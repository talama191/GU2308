using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(this);
        // SceneManager.UnloadScene

        SceneManager.sceneLoaded += OnLoadSceneDone;
        SceneManager.sceneUnloaded += OnUnloadSceneDone;

        // SceneManager.sceneLoaded -= OnLoadSceneDone;


    }

    public void OnLoadSceneDone(Scene scene, LoadSceneMode loadSceneMode)
    {

    }

    public void OnUnloadSceneDone(Scene scene)
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadSceneAsync("Buoi 9 B", LoadSceneMode.Single);
        }
    }

}
