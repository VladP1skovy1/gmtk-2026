using UnityEngine;

namespace LaunchBad.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Rocket", menuName = "Scriptable Objects/Rocket")]
    public class Rocket : ScriptableObject
    {
        [SerializeField] private string rocketName;
        [SerializeField] private float countDownDuration;
        [SerializeField] private int rocketWeight;
    }
}