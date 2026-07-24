namespace LaunchBad.Utils
{
    [System.Serializable]
    public class DiscreteTimetable<T> : Timetable<T>
    {
        public override T GetValueAtTime(float time)
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