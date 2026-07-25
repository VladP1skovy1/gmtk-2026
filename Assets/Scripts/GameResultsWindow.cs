using LaunchBad.Core;
using LaunchBad.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LaunchBad
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

        [Header("Messages")] [SerializeField] private string allGreenLightsMessage;
        [SerializeField] private string allRedLightsMessage;
        [SerializeField] private string notEnoughGreenLightsMessage;
        [SerializeField] private string assessmentMessage;

        private Window _window;


        private void Awake()
        {
            _window = GetComponent<Window>();
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
                    break;
                case EndGameStates.AllGreenLights:
                    background.sprite = allGreenLightsSprite;
                    resultsText.text = allGreenLightsMessage;
                    break;
                case EndGameStates.NotEnoughGreenLights:
                    background.sprite = notEnoughGreenLightsSprite;
                    resultsText.text = notEnoughGreenLightsMessage;
                    break;
            }

            assessmentText.text = assessmentMessage;

            _window.Show();
        }
    }
}