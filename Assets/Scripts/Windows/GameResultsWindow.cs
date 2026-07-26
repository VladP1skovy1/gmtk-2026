using LaunchBad.Core;
using LaunchBad.UI;
using LaunchBad.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LaunchBad.Windows
{
    public class GameResultsWindow : MonoBehaviour
    {
        [Header("References")] [SerializeField]
        private TextMeshProUGUI resultsText;

        [SerializeField] private TextMeshProUGUI assessmentText;

        [Header("Sprites")] [SerializeField] private Image background;
        [SerializeField] private Sprite allGreenLightsSprite;
        [SerializeField] private Sprite allRedLightsSprite;
        [SerializeField] private Sprite notEnoughGreenLightsSprite;
        [SerializeField] private Sprite successBackgroundSprite;
        [SerializeField] private Sprite failureBackgroundSprite;

        [Header("Messages")] [SerializeField] private string allGreenLightsMessage;
        [SerializeField] private string allRedLightsMessage;
        [SerializeField] private string notEnoughGreenLightsMessage;
        [SerializeField] private string allGreenAssessmentMessage;
        [SerializeField] private string allRedAssessmentMessage;
        [SerializeField] private string notEnoughGreenAssessmentMessage;

        private Window _window;
        private Image _image;


        private void Awake()
        {
            _window = GetComponent<Window>();
            _image = GetComponent<Image>();
        }

        private void OnEnable()
        {
            GameManager.OnGameFinished += HandleGameFinished;
        }

        private void OnDisable()
        {
            GameManager.OnGameFinished -= HandleGameFinished;
        }

        private void HandleGameFinished(EndGameStates endState)
        {
            switch (endState)
            {
                case EndGameStates.AllRedLights:
                    background.sprite = allRedLightsSprite;
                    resultsText.text = allRedLightsMessage;
                    _image.sprite = failureBackgroundSprite;
                    assessmentText.text = allRedAssessmentMessage;
                    break;
                case EndGameStates.AllGreenLights:
                    background.sprite = allGreenLightsSprite;
                    resultsText.text = allGreenLightsMessage;
                    _image.sprite = successBackgroundSprite;
                    assessmentText.text = allGreenAssessmentMessage;
                    break;
                case EndGameStates.NotEnoughGreenLights:
                    background.sprite = notEnoughGreenLightsSprite;
                    resultsText.text = notEnoughGreenLightsMessage;
                    _image.sprite = failureBackgroundSprite;
                    assessmentText.text = notEnoughGreenAssessmentMessage;
                    break;
            }

            _window.Show();
        }
    }
}