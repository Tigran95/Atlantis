using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class Loading : MonoBehaviour
{
    public static Loading Instance;
    private void Awake()
    {
        Instance = this;
    }

    [SerializeField] private Image _loadingImg;
    [SerializeField] private Image _blockerImg;
    private bool _doLoading;
    public void StartLoading()
    {
        _doLoading = true;
        _blockerImg.raycastTarget = true;
        var seq = DOTween.Sequence();
        seq.Join(_blockerImg.DOColor(new Color(_blockerImg.color.r, _blockerImg.color.g, _blockerImg.color.b,0.3f), 0.3f).SetEase(Ease.Linear));
        seq.Join(_loadingImg.DOColor(new Color(_loadingImg.color.r, _loadingImg.color.g, _loadingImg.color.b,1.0f), 0.3f)).SetEase(Ease.Linear);
        StartCoroutine(DoLoading()); 
    }
    private IEnumerator DoLoading()
    {
        if (!_doLoading) { yield break; }
        yield return new WaitForEndOfFrame();
        _loadingImg.rectTransform.DORotate(_loadingImg.rectTransform.eulerAngles + new Vector3(0, 0, 90), 0.5f).SetEase(Ease.Linear).OnComplete(() =>
             {
                 StartCoroutine(DoLoading());
             });
    }

    public void EndLoading()
    {
        _doLoading = false;
        var seq = DOTween.Sequence();
        seq.Join(_blockerImg.DOColor(new Color(_blockerImg.color.r, _blockerImg.color.g, _blockerImg.color.b, 0.0f), 0.3f).SetEase(Ease.Linear));
        seq.Join(_loadingImg.DOColor(new Color(_loadingImg.color.r, _loadingImg.color.g, _loadingImg.color.b, 0.0f), 0.3f)).SetEase(Ease.Linear);
        seq.OnComplete(() =>
        {
            _blockerImg.raycastTarget = false;
        });
    }
}
