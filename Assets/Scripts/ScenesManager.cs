using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ScenesManager : MonoBehaviour
{
    public static ScenesManager Instance;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        GoToScenarioChoosingScene();
    }

    public void GoToScenarioChoosingScene()
    {
        SceneManager.LoadScene(1);
    }

    public void GoToMarkerModeScene()
    {
        SceneManager.LoadScene(2);
    }

    public void GoToARModeScene()
    {
        SceneManager.LoadScene(3);
    }
}
