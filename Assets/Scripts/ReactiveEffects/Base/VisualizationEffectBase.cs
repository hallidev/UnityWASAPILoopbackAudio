using Assets.Scripts.Audio;
using UnityEngine;

namespace Assets.Scripts.ReactiveEffects.Base
{
    public abstract class VisualizationEffectBase : MonoBehaviour
    {
        #region Private Member Variables

        private LoopbackAudio _loopbackAudio;

        #endregion

        #region Protected Properties

        protected LoopbackAudio LoopbackAudio { get { return _loopbackAudio; } }

        #endregion

        #region Public Properties

        public AudioVisualizationStrategy AudioVisualizationStrategy;
        public int AudioSampleIndex;
        public float PrimaryScaleFactor;

        #endregion

        #region Startup / Shutdown

        public virtual void Start()
        {
            // References and setup
            _loopbackAudio = FindObjectOfType<LoopbackAudio>();
        }

        #endregion

        #region Protected Methods

        protected float GetAudioData()
        {
            // Get audio data
            return _loopbackAudio.GetSpectrumData(AudioVisualizationStrategy, AudioSampleIndex);
        }

        #endregion
    }
}
