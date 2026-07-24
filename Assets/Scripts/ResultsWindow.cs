using LaunchBad.Core;
using LaunchBad.ScriptableObjects;
using TMPro;
using UnityEngine;
using Window = LaunchBad.UI.Window;

namespace LaunchBad
{
    public class ResultsWindow : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private TextMeshProUGUI resultsText;

        [SerializeField] private TextMeshProUGUI assessmentText;

        [Header("Messages")]
        [SerializeField] private string tpMessage;

        [SerializeField] private string tnMessage;
        [SerializeField] private string fpMessage;
        [SerializeField] private string fnMessage;
        [SerializeField] private string fnAssessmentMessage;
        
        private Window _window;


        private void Awake()
        {
            _window = GetComponent<Window>();
        }

        private void OnEnable()
        {
            GameManager.OnChoiceMade += HandleChoiceMade;
        }

        private void OnDisable()
        {
            GameManager.OnChoiceMade -= HandleChoiceMade;
        }

        private void HandleChoiceMade(Rocket rocket, bool wasLaunched)
        {
            (resultsText.text, assessmentText.text) = (wasLaunched, rocket.ShouldBeLaunched) switch
            {
                (true, true) => (tpMessage, ""),
                (false, false) => (tnMessage, rocket.AssessmentText),
                (true, false) => (fpMessage, rocket.AssessmentText),
                (false, true) => (fnMessage, fnAssessmentMessage)
            };
            _window.Show();
        }
    }
}