using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    private List<AsyncOperation> scenesToLoad = new List<AsyncOperation>();
    [SerializeField] private List<string> sceneNamesLoad = new List<string>();
    [SerializeField] private List<string> sceneNamesUnload = new List<string>();

    [SerializeField] private Vector2 playerPosition;
    [SerializeField] private VectorValue playerStorage;

    //[SerializeField] private GameObject fadeInPanel;
    //[SerializeField] private GameObject fadeOutPanel;
    //[SerializeField] private float fadeWaitTime = 3f;

    public void Awake()
    {
        /*if(fadeInPanel != null)
        {
            GameObject panel = Instantiate(fadeInPanel, Vector3.zero, Quaternion.identity) as GameObject;
            Destroy(panel, 1);

            Debug.Log("Fade in panel created.");
        }*/
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            playerStorage.initialValue = playerPosition;
            //SceneManager.LoadScene(sceneToLoad);
            
            foreach(string s in sceneNamesLoad)
            {
                scenesToLoad.Add(SceneManager.LoadSceneAsync(s, LoadSceneMode.Additive));
            }

            foreach(string s in sceneNamesUnload)
            {
                SceneManager.UnloadSceneAsync(s);
            }

            //StartCoroutine(FadeCo());

        }
    }

    /*public IEnumerator FadeCo()
    {
        if(fadeOutPanel != null)
        {
            GameObject panel = Instantiate(fadeOutPanel, Vector3.zero, Quaternion.identity) as GameObject;
            Debug.Log("Fade out panel created.");
        }
        yield return new WaitForSeconds(fadeWaitTime);
        
        foreach (string s in sceneNamesLoad)
        {
            scenesToLoad.Add(SceneManager.LoadSceneAsync(s, LoadSceneMode.Additive));
        }

        foreach (string s in sceneNamesUnload)
        {
            SceneManager.UnloadSceneAsync(s);
        }
    }*/
}
