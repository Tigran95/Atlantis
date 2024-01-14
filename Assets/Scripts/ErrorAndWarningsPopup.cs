using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public enum WarningType
{
    InternetRequired,InternetOptional,ErrorAtDownloadingProcess
}
public class ErrorAndWarningsPopup : MonoBehaviour
{
    public static ErrorAndWarningsPopup Instance;

    private void Awake()
    {
        Instance = this;
        StartCoroutine(CheckInternetConnection());
    }


    [SerializeField] private Image _screenBlocker;
    [SerializeField] private Text _infoText;
    [SerializeField] private RectTransform _popupRect;
    [SerializeField] private Button _actBtn;
    [SerializeField] private float _tweenSpeed;
    private bool _opened;

    public void OpenPopup(string strInfo,WarningType type,System.Action actWhenClose)
    {
        
        if (_opened) { return; }
        _opened = true;
        _infoText.text = strInfo;
        switch (type)
        {
            case WarningType.InternetRequired:
                _actBtn.gameObject.SetActive(false);
                break;
            case WarningType.InternetOptional:
                _actBtn.gameObject.SetActive(true);
                _actBtn.onClick.RemoveAllListeners();
                _actBtn.onClick.AddListener(() => ClosePopup());
                break;
            case WarningType.ErrorAtDownloadingProcess:
                _actBtn.onClick.RemoveAllListeners();
                _actBtn.onClick.AddListener(() => { ClosePopup(); actWhenClose?.Invoke(); });
                break;
            default:
                break;
        }

        var seq = DOTween.Sequence();
        _screenBlocker.raycastTarget = true;
        seq.Append(_screenBlocker.DOColor(new Color(_screenBlocker.color.r, _screenBlocker.color.g, _screenBlocker.color.b, 0.3f), 0.2f)).OnComplete(() =>
        {
            var seq2 = DOTween.Sequence();
            seq2.Append(_popupRect.DOScale(1.0f, _tweenSpeed));
        });
    }

    public void ClosePopup()
    {
        _opened = false;
        var seq = DOTween.Sequence();

        seq.Append(_popupRect.DOScale(0.0f, _tweenSpeed)).OnComplete(() =>
        {
            var seq2 = DOTween.Sequence();
            seq2.Append(_screenBlocker.DOColor(new Color(_screenBlocker.color.r, _screenBlocker.color.g, _screenBlocker.color.b, 0.0f), 0.2f)).OnComplete(() =>
            {
                _screenBlocker.raycastTarget = false;
            });
        });
    }


    private IEnumerator CheckInternetConnection()
    {
        if(Application.internetReachability == NetworkReachability.NotReachable)
        {
            OpenPopup("Without an Internet connection, the application will not work fully", WarningType.InternetOptional, null);
        }
        yield return new WaitForSeconds(15.0f);
        StartCoroutine(CheckInternetConnection());
    }
}
