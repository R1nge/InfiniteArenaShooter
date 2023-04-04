using Player;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator _animator;
    private static readonly int IsOpen = Animator.StringToHash("IsOpen");

    private void Awake() => _animator = GetComponent<Animator>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerCharacter _))
        {
            _animator.SetBool(IsOpen, true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out PlayerCharacter _))
        {
            _animator.SetBool(IsOpen, false);
        }
    }
}