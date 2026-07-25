using UnityEngine;

namespace LaunchBad.Rules
{
    [System.Serializable]
    public struct Rule
    {
        [field: SerializeField] public string Text { get; private set; }
        [field: SerializeField] public int LaunchNumber { get; private set; }
    }
}