using System;
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

        [field: SerializeField] public ContinuousTimetable WindTimetable { get; private set; }
        [field: SerializeField] public ContinuousTimetable TemperatureTimetable { get; private set; }
        [field: SerializeField] public DiscreteTimetable<SkyStatus> SkyTimetable { get; private set; }
    }
}