using UnityEngine;

namespace LaunchBad
{
    public enum LaunchPadStatus
    {
        Clear, 
        People,
        Birds,
    }
    
    [System.Serializable]
    public struct LaunchPadStatusInfo
    {
        public LaunchPadStatus status;
        public Sprite sprite;
        public float reactionTime;
        public string message;
    }
}
