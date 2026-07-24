using UnityEngine;

namespace LaunchBad.Utils
{
    [System.Serializable]
    public class ContinuousTimetable : Timetable<float>
    {
        public override float GetValueAtTime(float time)
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
                return Mathf.Lerp(a.Value, b.Value, t);
            }

            return entries[^1].Value;
        }
    }
}