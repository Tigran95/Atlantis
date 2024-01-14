using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChooseScenario : MonoBehaviour
{
    [SerializeField] private Button _useMarkerModeBtn;
    [SerializeField] private Button _useARModeBtn;


    private void Start()
    {
        _useMarkerModeBtn.onClick.AddListener(() => UseMarkerMode());
        _useARModeBtn.onClick.AddListener(() => UseARMode());
    }

    private void UseMarkerMode()
    {
        ScenesManager.Instance.GoToMarkerModeScene();
    }

    private void UseARMode()
    {
        ScenesManager.Instance.GoToARModeScene();
    }
}
