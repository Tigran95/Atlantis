using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using DG.Tweening;
public class DataUsingSelectionPopup : MonoBehaviour
{
    [SerializeField] private Image _localBlocker;
    [SerializeField] private Button _useExistedDataBtn;
    [SerializeField] private Button _updateDataBtn;
    [SerializeField] private Text _infoText;
    [SerializeField] private RectTransform _rect;
    [SerializeField] private float _tweenSpeed;


    public void OpenDataUsingSelectionPopup(Action onSelectExistedDataUsing, Action onSelectUpdateDataUsing,string fileName)
    {
        _infoText.text = "";
        _infoText.text = "A file named <color=green>" + fileName + "</color> found in the local storage. Do you want to use it?";
        _useExistedDataBtn.onClick.RemoveAllListeners();
        _useExistedDataBtn.onClick.AddListener(() => { onSelectExistedDataUsing?.Invoke(); CloseDataUsingSelectionPopup(null); });

        _updateDataBtn.onClick.RemoveAllListeners();
        _updateDataBtn.onClick.AddListener(() => { onSelectUpdateDataUsing?.Invoke();CloseDataUsingSelectionPopup(()=>Debug.Log("Update")); });
        var seq = DOTween.Sequence();
        _localBlocker.raycastTarget = true;
        seq.Append(_localBlocker.DOColor(new Color(_localBlocker.color.r, _localBlocker.color.g, _localBlocker.color.b, 0.3f), 0.2f)).OnComplete(() =>
          {
              var seq2 = DOTween.Sequence();
              seq2.Append(_rect.DOScale(1.0f, _tweenSpeed));
          });
        
    }
    private void CloseDataUsingSelectionPopup(Action OnPopupClose)
    {
        var seq = DOTween.Sequence();

        seq.Append(_rect.DOScale(0.0f, _tweenSpeed)).OnComplete(() =>
        {
            var seq2 = DOTween.Sequence();
            seq2.Append(_localBlocker.DOColor(new Color(_localBlocker.color.r, _localBlocker.color.g, _localBlocker.color.b, 0.0f), 0.2f)).OnComplete(() =>
            {
                _localBlocker.raycastTarget = false;
                OnPopupClose?.Invoke();
            });

           
        });
    }
}
