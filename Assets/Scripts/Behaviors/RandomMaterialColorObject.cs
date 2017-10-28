using UnityEngine;

namespace Assets.Scripts.Behaviors
{
    public class RandomMaterialColorObject : MonoBehaviour
    {
        #region Startup / Shutdown

        public void Awake()
        {
            Renderer rend = GetComponent<Renderer>();
            Color color = Globals.GetRandomStrongColor();
            rend.material.SetColor("_Color", color);
            rend.material.SetColor("_EmissionColor", color);
        }

        #endregion
    }
}
