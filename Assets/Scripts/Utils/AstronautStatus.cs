using UnityEngine;

namespace LaunchBad.Utils
{
    public enum AstronautStatus
    {
        Sick,
        Healthy,
        Depressed,
    }
    
    [System.Serializable]
    public struct AstronautSpriteMapping
    {
        public AstronautStatus status;
        public Sprite sprite;
    }
}