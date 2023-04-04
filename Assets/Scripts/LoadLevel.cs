using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    [SerializeField] private string sceneName;

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(LoadMap_c());
    }

    private IEnumerator LoadMap_c()
    {
        yield return Resources.UnloadUnusedAssets();
        SceneManager.LoadScene(sceneName);
    }
}