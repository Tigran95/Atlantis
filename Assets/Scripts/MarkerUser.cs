using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MarkerUser : MonoBehaviour
{
    [SerializeField] private RawImage _markerImg;
    private void Start()
    {
        AppUI.Instance.SetBackButtonStatus(()=>ScenesManager.Instance.GoToScenarioChoosingScene());
        Debug.Log(Application.persistentDataPath);
        GetMarker();
    }
    private void GetMarker()
    {
       
        ResourcesLoader.Instance.TryToGetTargetImage
           (
           MobileAPI.GetTargetImageURL,
           (Texture2D txt) => CreteMarker(txt),
           (string info) => Debug.Log(info),
           (System.Action useExistedData, System.Action update, string fileName) => { AppUI.Instance.OpenDataUsingSelectionPopup(useExistedData, update, fileName); }
           );
    }
    private void CreteMarker(Texture2D texture)
    {
        _markerImg.gameObject.SetActive(true);
        _markerImg.texture = texture;
        _markerImg.SetNativeSize();
        _markerImg.rectTransform.sizeDelta *= Screen.width / texture.width;
    }
}
