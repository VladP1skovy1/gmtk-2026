using System;
using System.Collections;
using System.Collections.Generic;
using LaunchBad.Core;
using LaunchBad.ScriptableObjects;
using LaunchBad.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace LaunchBad
{
    public class SecurityManager : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer launchPadImage;
        [SerializeField] private Button securityButton;

        public static event Action<LaunchPadStatusInfo> OnSecurityBreach;

        private DiscreteTimetable<LaunchPadStatus> _launchPadTimetable;
        private List<LaunchPadStatusInfo> _launchPadStatusInfos;
        private int _currentEventIndex;

        private void OnEnable()
        {
            GameManager.OnRocketChanged += OnRocketChanged;
            CountDownManager.OnCountDown += OnCountDown;
            securityButton.onClick.AddListener(OnSecurityButtonPressed);
        }

        private void OnDisable()
        {
            GameManager.OnRocketChanged -= OnRocketChanged;
            CountDownManager.OnCountDown -= OnCountDown;
            securityButton.onClick.RemoveListener(OnSecurityButtonPressed);
        }

        private void OnSecurityButtonPressed()
        {
            StopAllCoroutines();
            launchPadImage.sprite = _launchPadStatusInfos.Find(info => info.status == LaunchPadStatus.Clear).sprite;
        }

        private void OnCountDown(float time)
        {
            if (_launchPadTimetable == null || _launchPadStatusInfos == null) return;
            if (_currentEventIndex >= _launchPadTimetable.Length) return;

            var nextEvent = _launchPadTimetable.GetEntryAtIndex(_currentEventIndex);
            if (time >= nextEvent.Time) return;

            var status = nextEvent.Value;
            var statusInfo = _launchPadStatusInfos.Find(info => info.status == status);
            launchPadImage.sprite = statusInfo.sprite;
            if (statusInfo.status != LaunchPadStatus.Clear)
            {
                StartCoroutine(FinishGameAfterDelay(statusInfo.reactionTime, statusInfo));
            }
            _currentEventIndex++;
        }

        private IEnumerator FinishGameAfterDelay(float reactionTime, LaunchPadStatusInfo statusInfo)
        {
            yield return new WaitForSeconds(reactionTime);
            OnSecurityBreach?.Invoke(statusInfo);
        }

        private void OnRocketChanged(Rocket rocket)
        {
            _launchPadTimetable = rocket.LaunchPadTimetable;
            _launchPadStatusInfos = rocket.LaunchPadStatusInfos;
            _currentEventIndex = 0;
        }
    }
}