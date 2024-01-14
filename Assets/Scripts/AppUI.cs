using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class AppUI : MonoBehaviour
{
    public static AppUI Instance;
    private void Awake()
    {
        Instance = this;
    }
    [SerializeField] private DataUsingSelectionPopup _dataUsingSelectionPopup;
    [SerializeField] private ErrorAndWarningsPopup _errorAndWarningsPopup;
    [SerializeField] private Loading _loading; 
    [SerializeField] private Button _backBtn;
    

    public void OpenPopup(string strInfo, WarningType type, System.Action actWhenClose)
    {
        _errorAndWarningsPopup.OpenPopup(strInfo, type, actWhenClose);
    }
    public void ClosePopup()
    {
        _errorAndWarningsPopup.ClosePopup();
    }

    public void OpenDataUsingSelectionPopup(Action onSelectExistedDataUsing, Action onSelectUpdateDataUsing,string fileName)
    {
        _dataUsingSelectionPopup.OpenDataUsingSelectionPopup(onSelectExistedDataUsing, onSelectUpdateDataUsing,fileName);
    }

    public void DoLoading()
    {
        _loading.StartLoading();
    }
    public void EndLoading()
    {
        _loading.EndLoading();
    }

    public void SetBackButtonStatus(Action act)
    {
        Debug.Log("btn");
        _backBtn.gameObject.SetActive(true);
        _backBtn.onClick.RemoveAllListeners();
        _backBtn.onClick.AddListener(() => { act.Invoke(); _backBtn.gameObject.SetActive(false); });
    }
}
