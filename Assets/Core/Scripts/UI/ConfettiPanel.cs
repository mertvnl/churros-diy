using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfettiPanel : MonoBehaviour
{
    private ParticleSystem[] particles;

    private void Awake()
    {
        particles = GetComponentsInChildren<ParticleSystem>();
    }

    private void OnEnable()
    {
        GameManager.Instance.OnLevelCompleted.AddListener(PlayConfetti);
        LevelManager.Instance.OnLevelLoadingStarted.AddListener(StopConfetti);
        LevelManager.Instance.OnLevelLoaded.AddListener(StopConfetti);
    }

    private void OnDisable()
    {
        GameManager.Instance.OnLevelCompleted.RemoveListener(PlayConfetti);
        LevelManager.Instance.OnLevelLoadingStarted.RemoveListener(StopConfetti);
        LevelManager.Instance.OnLevelLoaded.RemoveListener(StopConfetti);
    }

    private void PlayConfetti(bool success)
    {
        if (!success) return;

        foreach (var p in particles)
        {
            p.Play();
        }
    }

    private void StopConfetti()
    {
        foreach (var p in particles)
        {
            p.Stop();
        }
    }
}
