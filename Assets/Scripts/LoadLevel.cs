using System.Collections;
using Player;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    [SerializeField] private string sceneName;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerCharacter _))
        {
            StartCoroutine(LoadMap_c());
        }
    }

    private IEnumerator LoadMap_c()
    {
        yield return Resources.UnloadUnusedAssets();
        SceneManager.LoadScene(sceneName);
    }
}