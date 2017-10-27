using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public static class Globals
    {
        public static List<Color> Colors = new List<Color>();

        static Globals()
        {
            Colors.Add(new Color(1.0f, 0.5f, 0.5f));
            Colors.Add(new Color(1.0f, 0.75f, 0.5f));
            Colors.Add(new Color(1.0f, 1.0f, 0.5f));
            Colors.Add(new Color(0.75f, 1.0f, 0.5f));
            Colors.Add(new Color(0.5f, 1.0f, 0.5f));
            Colors.Add(new Color(0.5f, 1.0f, 0.75f));
            Colors.Add(new Color(0.5f, 1.0f, 1.0f));
            Colors.Add(new Color(0.5f, 0.75f, 1.0f));
            Colors.Add(new Color(0.5f, 0.5f, 1.0f));
            Colors.Add(new Color(0.75f, 0.5f, 1.0f));
            Colors.Add(new Color(1.0f, 0.5f, 1.0f));
            Colors.Add(new Color(1.0f, 0.5f, 0.75f));
        }

        public static Color GetRandomColor()
        {
            return Colors[Random.Range(0, Colors.Count - 1)];
        }

        public static class Tags
        {
            public const string LeftController = "LeftController";
            public const string RightController = "RightController";
        }
    }
}
