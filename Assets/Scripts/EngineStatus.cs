using UnityEngine;

namespace LaunchBad
{
    public enum EngineStatus
    {
        Off,
        Running,
        Sparking,
        Smoking,
    }
    
    [System.Serializable]
    public struct EngineSpriteMapping
    {
        public EngineStatus status;
        public Sprite sprite;
    }
}
