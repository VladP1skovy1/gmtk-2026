using LaunchBad.Core;
using LaunchBad.ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Window = LaunchBad.UI.Window;

namespace LaunchBad.Windows
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
        
        [Header("Backgrounds")]
        [SerializeField] private Sprite positiveBackground;
        [SerializeField] private Sprite negativeBackground;
        
        private Window _window;
        private Image _image;


        private void Awake()
        {
            _window = GetComponent<Window>();
            _image = GetComponent<Image>();
        }

        private void OnEnable()
        {
            GameManager.OnChoiceMade += HandleChoiceMade;
            GameManager.OnRocketChanged += HandleRocketChanged;
        }

        private void OnDisable()
        {
            GameManager.OnChoiceMade -= HandleChoiceMade;
            GameManager.OnRocketChanged -= HandleRocketChanged;
        }

        private void HandleRocketChanged(Rocket rocket)
        {
            _window.Hide();
        }

        private void HandleChoiceMade(Rocket rocket, bool wasLaunched)
        {
            (resultsText.text, assessmentText.text, _image.sprite) = (wasLaunched, rocket.ShouldBeLaunched) switch
            {
                (true, true) => (tpMessage, "", positiveBackground),
                (false, false) => (tnMessage, rocket.AssessmentText, positiveBackground),
                (true, false) => (fpMessage, rocket.AssessmentText, negativeBackground),
                (false, true) => (fnMessage, fnAssessmentMessage, negativeBackground),
            };
            _window.Show();
        }
    }
}