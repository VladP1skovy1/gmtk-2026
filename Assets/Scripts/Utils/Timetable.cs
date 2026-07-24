using UnityEngine;

namespace LaunchBad.Utils
{
    [System.Serializable]
    public abstract class Timetable<T>
    {
        [SerializeField] private protected TimetableEntry<T>[] entries;
        public abstract T GetValueAtTime(float time);
        
        public int Length => entries.Length;
        
        public TimetableEntry<T> GetEntryAtIndex(int index)
        {
            return entries[index];
        }
    }
}
