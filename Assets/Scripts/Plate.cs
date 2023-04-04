using Player;
using UnityEngine;

public class Plate : MonoBehaviour
{
    [SerializeField] private GameObject UI;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerCharacter _))
        {
            Open();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out PlayerCharacter _))
        {
            Close();
        }
    }

    private void Open()
    {
        if (UI.activeInHierarchy) return;
        UI.SetActive(true);
    }

    public void Close()
    {
        if (!UI.activeInHierarchy) return;
        UI.SetActive(false);
    }
}