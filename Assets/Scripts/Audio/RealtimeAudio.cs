using System;
using CSCore;
using CSCore.SoundIn;
using CSCore.DSP;
using CSCore.Streams;

namespace Assets.Scripts.Audio
{
    public class RealtimeAudio
    {
        #region Constants

        private const FftSize CFftSize = FftSize.Fft4096;
        public const int MaxAudioValue = 10;

        #endregion

        #region Private Member Variables

        private WasapiLoopbackCapture _loopbackCapture;
        private SoundInSource _soundInSource;
        private IWaveSource _realtimeSource;
        private BasicSpectrumProvider _basicSpectrumProvider;
        private LineSpectrum _lineSpectrum;
        private SingleBlockNotificationStream _singleBlockNotificationStream;
        private int _spectrumSize;
        private ScalingStrategy _scalingStrategy;
        private Action<float[]> _receiveAudio;

        #endregion

        #region Constructor

        public RealtimeAudio(int spectrumSize, ScalingStrategy scalingStrategy, Action<float[]> receiveAudio)
        {
            _spectrumSize = spectrumSize;
            _scalingStrategy = scalingStrategy;
            _receiveAudio = receiveAudio;
        }

        #endregion

        #region Public Properties

        public BasicSpectrumProvider BasicSpectrumProvider { get { return _basicSpectrumProvider; } }

        #endregion

        #region Public Methods

        public void StartListen()
        {
            _loopbackCapture = new WasapiLoopbackCapture();
            _loopbackCapture.Initialize();

            _soundInSource = new SoundInSource(_loopbackCapture);

            _basicSpectrumProvider = new BasicSpectrumProvider(_soundInSource.WaveFormat.Channels, _soundInSource.WaveFormat.SampleRate, CFftSize);

            _lineSpectrum = new LineSpectrum(CFftSize)
            {
                SpectrumProvider = _basicSpectrumProvider,
                BarCount = _spectrumSize,
                UseAverage = true,
                IsXLogScale = true,
                ScalingStrategy = _scalingStrategy
            };

            _loopbackCapture.Start();

            _singleBlockNotificationStream = new SingleBlockNotificationStream(_soundInSource.ToSampleSource());
            _realtimeSource = _singleBlockNotificationStream.ToWaveSource();

            byte[] buffer = new byte[_realtimeSource.WaveFormat.BytesPerSecond / 2];

            _soundInSource.DataAvailable += (s, ea) =>
            {
                while (_realtimeSource.Read(buffer, 0, buffer.Length) > 0)
                {
                    float[] spectrumData = _lineSpectrum.GetSpectrumData(MaxAudioValue);

                    if (spectrumData != null &&  _receiveAudio != null)
                    {
                        _receiveAudio(spectrumData);
                    }
                }
            };

            _singleBlockNotificationStream.SingleBlockRead += singleBlockNotificationStream_SingleBlockRead;
        }

        public void StopListen()
        {
            _singleBlockNotificationStream.SingleBlockRead -= singleBlockNotificationStream_SingleBlockRead;

            _soundInSource.Dispose();
            _realtimeSource.Dispose();
            _receiveAudio = null;
            _loopbackCapture.Stop();
            _loopbackCapture.Dispose();
        }

        #endregion

        #region Private Methods

        private void singleBlockNotificationStream_SingleBlockRead(object sender, SingleBlockReadEventArgs e)
        {
            _basicSpectrumProvider.Add(e.Left, e.Right);
        }

        #endregion
    }
}
