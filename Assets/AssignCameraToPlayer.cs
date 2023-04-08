using Cinemachine;
using Player;
using UnityEngine;
using Zenject;

public class AssignCameraToPlayer : MonoBehaviour
{
    private CinemachineVirtualCamera _virtualCamera;
    private PlayerCharacter _playerCharacter;
    
    [Inject]
    private void Construct(PlayerCharacter playerCharacter)
    {
        _playerCharacter = playerCharacter;
    }

    private void Awake()
    {
        _virtualCamera = GetComponent<CinemachineVirtualCamera>();
        _virtualCamera.Follow = _playerCharacter.transform;
        _virtualCamera.LookAt = _playerCharacter.transform;
    }
}
