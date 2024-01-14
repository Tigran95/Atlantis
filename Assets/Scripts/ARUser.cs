using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ARUser : MonoBehaviour
{

    private Texture2D _tex;
    private GameObject _obj;
    private void Start()
    {
        AppUI.Instance.SetBackButtonStatus(() => ScenesManager.Instance.GoToScenarioChoosingScene());
        Debug.Log(Application.persistentDataPath);
        GetMarker(()=>GetModel(()=>AddReferences()));
    }

    private void GetMarker(Action OnComplite)
    {
        ResourcesLoader.Instance.TryToGetTargetImage
           (
           MobileAPI.GetTargetImageURL,
           (Texture2D tex) => { _tex = tex; OnComplite?.Invoke(); },
           (string info) => Debug.Log(info),
           (System.Action useExistedData, System.Action update, string fileName) => AppUI.Instance.OpenDataUsingSelectionPopup(useExistedData, update, fileName)
           );
    }

    private void GetModel(Action OnComplite)
    {
        ResourcesLoader.Instance.TryToGetModel
           (
           MobileAPI.GetTargetModelURL,
           (GameObject obj) => { _obj = obj; OnComplite?.Invoke(); },
           (string info) => Debug.Log(info),
           (System.Action useExistedData, System.Action update, string fileName) => AppUI.Instance.OpenDataUsingSelectionPopup(useExistedData, update, fileName)
           );
    }

    private void AddReferences()
    {
        ARObjectPooler pooler = gameObject.AddComponent<ARObjectPooler>();
        pooler.Init(_tex,_obj);
    }
}
