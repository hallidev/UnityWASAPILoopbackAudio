using Assets.Scripts.ReactiveEffects.Base;
using UnityEngine;

namespace Assets.Scripts.ReactiveEffects
{
    public class ObjectScaleReactiveEffect : VisualizationEffectBase
    {
        #region Private Member Variables

        private Vector3 _initialScale;

        #endregion

        #region Public Properties

        public Vector3 ScaleIntensity;

        #endregion

        #region Startup / Shutdown

        public override void Start()
        {
            base.Start();

            _initialScale = transform.localScale;
        }

        #endregion

        #region Render

        public void Update()
        {
            float audioData = GetAudioData();
            float xScaleAmount = audioData * ScaleIntensity.x;
            float yScaleAmount = audioData * ScaleIntensity.y;
            float zScaleAmount = audioData * ScaleIntensity.z;
            gameObject.transform.localScale = _initialScale + new Vector3(xScaleAmount, yScaleAmount, zScaleAmount);
        }

        #endregion
    }
}