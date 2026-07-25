using System.Collections.Generic;
using UnityEngine;

namespace LaunchBad.Rules
{
    [System.Serializable]
    public struct RuleTab
    {
        [field: SerializeField] public string TabName { get; private set; }
        [field: SerializeField] public List<Rule> Rules { get; private set; }
    }
}
