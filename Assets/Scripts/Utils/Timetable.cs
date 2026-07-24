using JetBrains.Annotations;
using UnityEngine;

namespace LaunchBad.Utils
{
    [System.Serializable]
    public class Timetable<T>
    {
        [SerializeField] private TimetableEntry<T>[] entries;
        
        [CanBeNull]
        public T GetValueAtTime(float time)
        {
            if (entries == null || entries.Length == 0) return default;
            
            for (var i = entries.Length - 1; i >= 0; i--)
            {
                if (entries[i].Time >= time) 
                {
                    return entries[i].Value;
                }
            }
            return entries[0].Value;
        }
    }
}
