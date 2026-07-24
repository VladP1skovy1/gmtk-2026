namespace LaunchBad.Utils
{
    [System.Serializable]
    public class DiscreteTimetable<T> : Timetable<T>
    {
        public override T GetValueAtTime(float time)
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
    }
}