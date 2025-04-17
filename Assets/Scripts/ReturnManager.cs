using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnManager : MonoBehaviour
{
    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        int buildIndex = currentScene.buildIndex;
        Debug.Log("Current Scene Build Index: " + buildIndex);

        int totalSceneCount = SceneManager.sceneCountInBuildSettings;
        Debug.Log("Total Scenes in Build Settings: " + totalSceneCount);
    }

    public void LoadScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }
}
