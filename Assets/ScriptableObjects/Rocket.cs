using System.Collections.Generic;
using LaunchBad.Utils;
using UnityEngine;

namespace LaunchBad.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Rocket", menuName = "Scriptable Objects/Rocket")]
    public class Rocket : ScriptableObject
    {
        [field: SerializeField] public float CountDownDuration { get; private set; }
        [SerializeField] private string rocketName;
        [SerializeField] private int rocketWeight;

        [field: SerializeField] public List<FuelTank> FuelTanks { get; private set; }
        
        [field: SerializeField] public Timetable<float> WindTimetable { get; private set; }
        [field: SerializeField] public Timetable<float> TemperatureTimetable { get; private set; }
        [field: SerializeField] public Timetable<SkyStatus> SkyTimetable { get; private set; }

    }
}