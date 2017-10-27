using Assets.Scripts.Audio;
using UnityEngine;

public class LoopbackAudio : MonoBehaviour
{
    #region Private Member Variables

    private RealtimeAudio _realtimeAudio;

    #endregion

    #region Public Properties

    public int SpectrumSize;
    public float[] SpectrumData;
    public bool IsIdle;

    #endregion

    #region Startup / Shutdown

    public void Awake()
    {
        SpectrumData = new float[SpectrumSize];

        // Used for scaling
        float scalePart = 1.0f / (float)SpectrumSize;

        // Setup loopback audio and start listening
        _realtimeAudio = new RealtimeAudio(SpectrumSize, (spectrumData) =>
        {
            // Raw
            SpectrumData = spectrumData;

            bool isIdle = true;

            for (int i = 0; i < SpectrumSize; i++)
            {
                if(spectrumData[i] > 0)
                {
                    isIdle = false;
                }
            }

            IsIdle = isIdle;
        });
        _realtimeAudio.StartListen();
    }

    public void OnApplicationQuit()
    {
        _realtimeAudio.StopListen();
    }

    #endregion

    #region Public Methods

    public float GetSpectrumData(int index = 0)
    {
        return SpectrumData[index];
    }

    #endregion
}
