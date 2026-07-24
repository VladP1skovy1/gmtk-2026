using System.Collections.Generic;
using LaunchBad.Utils;
using UnityEngine;
using UnityEngine.Serialization;

namespace LaunchBad.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Rocket", menuName = "Scriptable Objects/Rocket")]
    public class Rocket : ScriptableObject
    {
        [field: Header("General Parameters")]
        [field: SerializeField] public float CountDownDuration { get; private set; }
        [field: SerializeField] public string RocketName { get; private set; }
        [SerializeField] private int rocketWeight;
        [field: SerializeField] public Vector2 TemperatureRange { get; private set; }
        [field: SerializeField] public Vector2 WindRange { get; private set; }
        [field: SerializeField] public string SpecialInstructions { get; private set; }
        
        [field: Header("Fuel Schedule")]
        [field: SerializeField] public List<FuelTank> FuelTanks { get; private set; }
        [field: Header("Weather Schedule")]
        [field: SerializeField] public ContinuousTimetable WindTimetable { get; private set; }
        [field: SerializeField] public ContinuousTimetable TemperatureTimetable { get; private set; }
        [field: SerializeField] public DiscreteTimetable<SkyStatus> SkyTimetable { get; private set; }
        [field: Header("Engine Schedule")]
        [field: SerializeField] public List<EngineSpriteMapping> EngineSpriteMappings { get; private set; }
        [field: SerializeField] public DiscreteTimetable<EngineStatus> EngineTimetable { get; private set; }
        [field: Header("Astronauts Schedule")]
        [field: SerializeField] public List<AstronautSpriteMapping> AstronautSpriteMappings { get; private set; }
        [field: SerializeField] public List<DiscreteTimetable<AstronautStatus>> AstronautsTimetables { get; private set; }
        [field: Header("Security Schedule")]
        [field: SerializeField] public List<LaunchPadStatusInfo> LaunchPadStatusInfos { get; private set; }
        [field: SerializeField] public DiscreteTimetable<LaunchPadStatus> LaunchPadTimetable { get; private set; }
        [field: Header("Assessment Parameters")]
        [field: SerializeField] public bool ShouldBeLaunched { get; private set; }
        [field: SerializeField] public string AssessmentText { get; private set; }
    }
}