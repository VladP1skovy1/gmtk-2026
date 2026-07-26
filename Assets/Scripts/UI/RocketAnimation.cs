using System;
using System.Collections;
using LaunchBad.Core;
using LaunchBad.ScriptableObjects;
using UnityEngine;

namespace LaunchBad.UI
{
    public class RocketAnimation : MonoBehaviour
    {
        [SerializeField] private Transform rocketTransform;
        [SerializeField] private float moveDistance = 5f;
        [SerializeField] private float duration = 1f;
        [SerializeField] private AnimationCurve movementCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
        
        [SerializeField] private ParticleSystem fireParticles;
        [SerializeField] private ParticleSystem smokeParticles;
        
        [SerializeField] private ParticleSystem explosionFireParticles;
        [SerializeField] private ParticleSystem explosionSparkleParticles;
        [SerializeField] private ParticleSystem explosionSmokeParticles;
        
        [SerializeField] private float sparkleToFireDelay = 0.1f;
        [SerializeField] private float fireToSmokeDelay = 0.15f;

        private Vector3 _startPosition;

        private void Awake()
        {
            _startPosition = rocketTransform.position;
        }

        private void HandleRocketAnimation(bool isLaunched, Action onComplete)
        {
            StartCoroutine(PlayAnimation(isLaunched, onComplete));
        }

        private IEnumerator PlayAnimation(bool isLaunched, Action onComplete)
        {
            if (isLaunched)
            {   
                fireParticles.Play();
                smokeParticles.Play();
                
                yield return MoveUp();
                
                fireParticles.Stop();
                smokeParticles.Stop();
            } 
            else
            {
                yield return PlayExplosion();
            }

            onComplete?.Invoke();
        }

        private IEnumerator PlayExplosion()
        {
            explosionSparkleParticles.Play();
            yield return new WaitForSeconds(sparkleToFireDelay);

            explosionFireParticles.Play();
            yield return new WaitForSeconds(fireToSmokeDelay);

            explosionSmokeParticles.Play();
            float totalDuration = fireToSmokeDelay + sparkleToFireDelay
                                                   + Mathf.Max(explosionSparkleParticles.main.duration,
                                                       explosionFireParticles.main.duration,
                                                       explosionSmokeParticles.main.duration);

            yield return new WaitForSeconds(totalDuration);
        }

        private IEnumerator MoveUp()
        {
            Vector3 endPosition = _startPosition + Vector3.up * moveDistance;
            float elapsed = 0f;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float t = movementCurve.Evaluate(elapsed / duration);
                rocketTransform.position = Vector3.LerpUnclamped(_startPosition, endPosition, t);
                yield return null;
            }

            rocketTransform.position = endPosition;
        }
        
        private void HandleRocketChange(Rocket obj)
        {
            rocketTransform.position = _startPosition;
        }

        private void OnEnable()
        {
            GameManager.OnRocketAnimation += HandleRocketAnimation;
            GameManager.OnRocketChanged += HandleRocketChange;
        }

        

        private void OnDisable()
        {
            GameManager.OnRocketAnimation -= HandleRocketAnimation;
            GameManager.OnRocketChanged -= HandleRocketChange;
        }
    }
}