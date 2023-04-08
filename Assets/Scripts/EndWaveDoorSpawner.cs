using UnityEngine;
using Zenject;

public class EndWaveDoorSpawner : MonoBehaviour
{
    [SerializeField] private GameObject homeDoor, continueDoor;
    private WaveSpawner _waveSpawner;
        
    [Inject]
    private void Construct(WaveSpawner waveSpawner)
    {
        _waveSpawner = waveSpawner;
    }

    private void Awake()
    {
        _waveSpawner.OnWaveClearedEvent += OnWaveCleared;
    }

    private void OnWaveCleared()
    {
        homeDoor.SetActive(true);
        continueDoor.SetActive(true);
    }

    private void OnDestroy()
    {
        _waveSpawner.OnWaveClearedEvent -= OnWaveCleared;
    }
}