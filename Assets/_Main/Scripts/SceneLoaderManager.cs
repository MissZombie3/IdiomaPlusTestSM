using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderManager : MonoBehaviour
{
    public static SceneLoaderManager instance;
    private string pendingScene;

    public float LoadPct
    {
        get;
        private set;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    
    public void LoadScene(string sceneName)
    {
        LoadPct = 0;
        this.pendingScene = sceneName;
        SceneManager.LoadScene("LoadingScene");
    }

    public void LoadPendingScene()
    {
        StartCoroutine(LoadPendingSceneAsync());
    }

    private void OnDestroy()
    {
        if (instance == this)
            instance = null;
    }

    private IEnumerator LoadPendingSceneAsync()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(pendingScene);

        while (!asyncLoad.isDone)
        {
            LoadPct = asyncLoad.progress;
            yield return null;
        }
    }
}
