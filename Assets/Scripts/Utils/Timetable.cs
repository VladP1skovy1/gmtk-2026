using System;
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
            if (entries == null || entries.Length == 0)
                return default;

            for (var i = 0; i < entries.Length - 1; i++)
            {
                if (time > entries[i].Time)
                {
                    return i == 0 ? entries[i].Value : entries[i - 1].Value;
                }
            }
            return entries[0].Value;
        }
        
        public T GetInterpolatedValueAtTime(float time, Func<T, T, float, T> linearInterpolation)
        {
            if (entries == null || entries.Length == 0) return default;

            if (time >= entries[0].Time) return entries[0].Value;
            if (time <= entries[^1].Time) return entries[^1].Value;

            for (var i = 0; i < entries.Length - 1; i++)
            {
                var a = entries[i];
                var b = entries[i + 1];

                if (!(time > b.Time)) continue;
                var t = (a.Time - time) / (a.Time - b.Time);
                return linearInterpolation(a.Value, b.Value, t);
            }

            return entries[^1].Value;
        }
    }
}
