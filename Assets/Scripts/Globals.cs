using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public static class Globals
    {
        public static List<Color> StrongColors = new List<Color>();
        public static List<Color> PastelColors = new List<Color>();

        static Globals()
        {
            PastelColors.Add(new Color(1.0f, 0.5f, 0.5f));
            PastelColors.Add(new Color(1.0f, 0.75f, 0.5f));
            PastelColors.Add(new Color(1.0f, 1.0f, 0.5f));
            PastelColors.Add(new Color(0.75f, 1.0f, 0.5f));
            PastelColors.Add(new Color(0.5f, 1.0f, 0.5f));
            PastelColors.Add(new Color(0.5f, 1.0f, 0.75f));
            PastelColors.Add(new Color(0.5f, 1.0f, 1.0f));
            PastelColors.Add(new Color(0.5f, 0.75f, 1.0f));
            PastelColors.Add(new Color(0.5f, 0.5f, 1.0f));
            PastelColors.Add(new Color(0.75f, 0.5f, 1.0f));
            PastelColors.Add(new Color(1.0f, 0.5f, 1.0f));
            PastelColors.Add(new Color(1.0f, 0.5f, 0.75f));

            StrongColors.Add(FromRGB(255, 0, 51));
            StrongColors.Add(FromRGB(0, 255, 51));
            StrongColors.Add(FromRGB(0, 51, 255));
            StrongColors.Add(FromRGB(255, 255, 51));
            StrongColors.Add(FromRGB(255, 87, 34));
            StrongColors.Add(FromRGB(41, 182, 246));
            StrongColors.Add(FromRGB(74, 20, 140));
            StrongColors.Add(FromRGB(233, 30, 99));
            StrongColors.Add(FromRGB(255, 153, 0));
            StrongColors.Add(FromRGB(204, 0, 153));
        }

        public static Color GetRandomPastelColor()
        {
            return PastelColors[Random.Range(0, PastelColors.Count - 1)];
        }

        public static Color GetRandomStrongColor()
        {
            return StrongColors[Random.Range(0, StrongColors.Count - 1)];
        }

        private static Color FromRGB(int r, int g, int b)
        {
            return new Color(r / 255.0f, g / 255.0f, b / 255.0f);
        }
    }
}
