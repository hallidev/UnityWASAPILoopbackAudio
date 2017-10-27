namespace Assets.Scripts.Audio
{
    internal interface ISpectrumProvider
    {
        bool GetFftData(float[] fftBuffer, object context);
        int GetFftBandIndex(float frequency);
    }
}