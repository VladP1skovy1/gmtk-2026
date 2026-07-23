using UnityEngine;

namespace LaunchBad.Utils
{
    [System.Serializable]
    public class TimetableEntry<T>
    {
        [field: SerializeField] public float Time { get; private set; }
        [field: SerializeField] public T Value { get; private set; }
    }
}