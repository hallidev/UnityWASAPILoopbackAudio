using Assets.Scripts.Audio;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LoopbackAudio : MonoBehaviour
{
    #region Constants

    private const int EnergyAverageCount = 100;

    #endregion

    #region Private Member Variables

    private RealtimeAudio _realtimeAudio;
    private List<float> _energyAverages = new List<float>();

    #endregion

    #region Public Properties

    public int SpectrumSize;
    public ScalingStrategy ScalingStrategy;
    public float[] SpectrumData;
    public float[] PostScaledSpectrumData;
    public float PostScaledMax;
    public float PostScaledEnergy;
    public bool IsIdle;

    #endregion

    #region Startup / Shutdown

    public void Awake()
    {
        SpectrumData = new float[SpectrumSize];
        PostScaledSpectrumData = new float[SpectrumSize];

        // Used for post scaling
        float postScaleStep = 1.0f / SpectrumSize;

        // Setup loopback audio and start listening
        _realtimeAudio = new RealtimeAudio(SpectrumSize, ScalingStrategy,(spectrumData) =>
        {
            // Raw
            SpectrumData = spectrumData;

            // Post scaled for visualization
            float postScaledPoint = postScaleStep;
            float postScaledMax = 0.0f;

            float energyAverage = 0.0f;
            float totalPostScaledValue = 0.0f;

            bool isIdle = true;
            for (int i = 0; i < SpectrumSize; i++)
            {
                // Don't scale low band, it's too useful
                if (i == 0)
                {
                    PostScaledSpectrumData[i] = SpectrumData[i];
                }
                else
                {
                    PostScaledSpectrumData[i] = postScaledPoint * SpectrumData[i] * (RealtimeAudio.MaxAudioValue - (1.0f - postScaledPoint));
                }

                if (PostScaledSpectrumData[i] > postScaledMax)
                {
                    postScaledMax = PostScaledSpectrumData[i];
                }

                postScaledPoint += postScaleStep;
                totalPostScaledValue += PostScaledSpectrumData[i];

                if (spectrumData[i] > 0)
                {
                    isIdle = false;
                }
            }

            PostScaledMax = postScaledMax;

            energyAverage = totalPostScaledValue / SpectrumSize;
            _energyAverages.Add(energyAverage);

            // We only want to track EnergyAverageCount averages.
            // With a value of 1000, this will happen every couple seconds
            if (_energyAverages.Count == EnergyAverageCount)
            {
                _energyAverages.RemoveAt(0);
            }

            // Average the averages to get the energy.
            PostScaledEnergy = _energyAverages.Average();
            IsIdle = isIdle;
        });
        _realtimeAudio.StartListen();
    }

    public void Update()
    {
        
    }

    public void OnApplicationQuit()
    {
        _realtimeAudio.StopListen();
    }

    #endregion

    #region Public Methods

    public float GetSpectrumData(AudioVisualizationStrategy strategy, int index = 0)
    {
        float spectrumData = 0.0f;

        switch (strategy)
        {
            case AudioVisualizationStrategy.Raw:
                spectrumData = SpectrumData[index];
                break;
            case AudioVisualizationStrategy.PostScaled:
                spectrumData = PostScaledSpectrumData[index];
                break;
            case AudioVisualizationStrategy.PostScaledMax:
                spectrumData = PostScaledMax;
                break;
            case AudioVisualizationStrategy.PostScaledEnergy:
                spectrumData = PostScaledEnergy;
                break;
        }

        return spectrumData;
    }

    #endregion
}
