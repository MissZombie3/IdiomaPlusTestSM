using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadPendingScene : MonoBehaviour
{
    private void Start()
    {
        SceneLoaderManager.instance.LoadPendingScene();
    }

    private void Update()
    {
        Debug.Log(SceneLoaderManager.instance.LoadPct);
    }
}
