using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class LoadSceneOnEnable : MonoBehaviour
{
    private List<AsyncOperation> scenesToLoad = new List<AsyncOperation>();
    [SerializeField] private List<string> sceneNames = new List<string>();

    private void OnEnable()
    {
        scenesToLoad.Add(SceneManager.LoadSceneAsync(sceneNames[0]));

        for(int i = 1; i < sceneNames.Count; i++)
        {
            scenesToLoad.Add(SceneManager.LoadSceneAsync(sceneNames[i], LoadSceneMode.Additive));
        }

    }
}
