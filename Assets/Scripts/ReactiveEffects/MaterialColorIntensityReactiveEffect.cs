using Assets.Scripts.ReactiveEffects.Base;
using UnityEngine;

namespace Assets.Scripts.ReactiveEffects
{
    public class MaterialColorIntensityReactiveEffect : VisualizationEffectBase
    {
        #region Private Member Variables

        private Renderer _renderer;
        private Color _initialColor;
        private Color _initialEmissionColor;

        #endregion

        #region Public Properties

        public float MinIntensity;
        public float IntensityScale;
        public float MinEmissionIntensity;
        public float EmissionIntensityScale;

        #endregion

        #region Startup / Shutdown

        public override void Start()
        {
            base.Start();

            _renderer = GetComponent<Renderer>();
            _initialColor = _renderer.material.GetColor("_Color");
            _initialEmissionColor = _renderer.material.GetColor("_EmissionColor");
        }

        #endregion

        #region Render

        public void Update()
        {
            float audioData = GetAudioData();
            float scaledAmount = Mathf.Clamp(MinIntensity + (audioData * IntensityScale), 0.0f, 1.0f);
            float scaledEmissionAmount = Mathf.Clamp(MinEmissionIntensity + (audioData * EmissionIntensityScale), 0.0f, 1.0f);
            Color scaledColor = _initialColor * scaledAmount;
            Color scaledEmissionColor = _initialEmissionColor * scaledEmissionAmount;

            _renderer.material.SetColor("_Color", scaledColor);
            _renderer.material.SetColor("_EmissionColor", scaledEmissionColor);
        }

        #endregion

        #region Public Methods

        public void UpdateColor(Color color)
        {
            _initialColor = color;
            _initialEmissionColor = color;
        }

        #endregion
    }
}