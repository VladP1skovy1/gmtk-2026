using UnityEngine;

namespace LaunchBad.Utils
{
    [System.Serializable]
    public class Timetable<T>
    {
        [SerializeField] private TimetableEntry<T>[] entries;
        
        public T GetValueAtTime(float time)
        {
            if (entries == null || entries.Length == 0)
                return default;

            for (var i = 0; i < entries.Length - 1; i++)
            {
                if (time > entries[i].Time)
                {
                    return i == 0 ? entries[i].Value : entries[i - 1].Value;
                }
            }

            return entries[^1].Value;
        }
    }
}
