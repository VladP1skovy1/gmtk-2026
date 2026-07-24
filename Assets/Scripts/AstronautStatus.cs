using UnityEngine;

namespace LaunchBad
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