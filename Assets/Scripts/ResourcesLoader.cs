using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
public class ResourcesLoader : MonoBehaviour
{
    public static ResourcesLoader Instance;
    private void Awake()
    {
        Instance = this;
    }
    private UnityWebRequest _currentUWR;

    public void TryToGetTargetImage(string url, Action<Texture2D> atSccess, Action<string> atFailure, Action<Action, Action,string> onDataExists)
    {
        StartCoroutine(TryToGetTargetImageCoroutine(url, atSccess, atFailure,onDataExists));
    }
    private IEnumerator TryToGetTargetImageCoroutine(string url, Action<Texture2D> atSccess, Action<string> atFailure,Action<Action,Action,string> onDataExists)
    {
        AppUI.Instance.DoLoading();
        if (LocalDataHandler.DataExists(LocalPathsAPI.TargetImageDirectory, url))
        {
            AppUI.Instance.EndLoading();
            Debug.Log("data exists");
            _currentUWR = null;
            onDataExists?.Invoke(
                () =>
                {
                    Debug.Log("use existed Image target");
                    atSccess?.Invoke(LocalDataHandler.GetTargetImage(LocalPathsAPI.TargetImageDirectory, url));
                },
                () =>
                {
                    Debug.Log("delete and update Image target");
                    LocalDataHandler.DeleteData(LocalPathsAPI.TargetImageDirectory, url);
                    StartCoroutine(TryToGetTargetImageCoroutine(url, atSccess, atFailure, onDataExists));
                }, LocalDataHandler.GetFileName(url));
        }
        else
        {
            while(Application.internetReachability == NetworkReachability.NotReachable)
            {
                AppUI.Instance.OpenPopup("At this stage, we will need an internet connection", WarningType.InternetRequired, null);
                yield return new WaitForEndOfFrame();
            }
            AppUI.Instance.ClosePopup();
            using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(url))
            {
                _currentUWR = uwr;
                _currentUWR.timeout = 15;

                yield return uwr.SendWebRequest();

                if (uwr.isNetworkError || uwr.isHttpError)
                {
                    _currentUWR = null;
                    atFailure?.Invoke("Error code- " + uwr.responseCode + " message- " + uwr.error);
                    AppUI.Instance.OpenPopup("Something went wrong, please try again.", WarningType.ErrorAtDownloadingProcess,() => StartCoroutine(TryToGetTargetImageCoroutine(url, atSccess, atFailure, onDataExists)));
                }
                else
                {
                    Texture2D targetImage = DownloadHandlerTexture.GetContent(uwr);
                    LocalDataHandler.SaveTargetImage(LocalPathsAPI.TargetImageDirectory, url, targetImage);
                    _currentUWR = null;
                    atSccess?.Invoke(targetImage);
                    AppUI.Instance.EndLoading();
                }
            }
        }
    }


    public void TryToGetModel(string url, Action<GameObject> atSccess, Action<string> atFailure, Action<Action, Action, string> onDataExists)
    {
       StartCoroutine(TryToGetModelCoroutine(url, atSccess, atFailure, onDataExists));
    }

    private IEnumerator TryToGetModelCoroutine(string url, Action<GameObject> atSccess, Action<string> atFailure, Action<Action, Action, string> onDataExists)
    {
        AppUI.Instance.DoLoading();
        if (LocalDataHandler.DataExists(LocalPathsAPI.ModelDirectory, url))
        {
            AppUI.Instance.EndLoading();
            Debug.Log("model data exists");
            _currentUWR = null;
            onDataExists?.Invoke(
                () =>
                {
                    Debug.Log("use existed model");
                    atSccess?.Invoke(LocalDataHandler.GetModel(LocalPathsAPI.ModelDirectory, url));
                },
                () =>
                {
                    Debug.Log("delete and update model");
                    LocalDataHandler.DeleteData(LocalPathsAPI.ModelDirectory, url);
                    StartCoroutine(TryToGetModelCoroutine(url, atSccess, atFailure, onDataExists));
                }, LocalDataHandler.GetFileName(url));
        }
        else
        {
            while (Application.internetReachability == NetworkReachability.NotReachable)
            {
                AppUI.Instance.OpenPopup("At this stage, we will need an internet connection", WarningType.InternetRequired, null);
                yield return new WaitForEndOfFrame();
            }
            AppUI.Instance.ClosePopup();
            yield return new WaitForEndOfFrame();
            using (UnityWebRequest uwr = UnityWebRequest.Get(url))
            {
                _currentUWR = uwr;
                _currentUWR.timeout = 15;

                yield return uwr.SendWebRequest();

                if (uwr.isNetworkError || uwr.isHttpError)
                {
                    _currentUWR = null;
                    atFailure?.Invoke("Error code- " + uwr.responseCode + " message- " + uwr.error);
                    AppUI.Instance.OpenPopup("Something went wrong, please try again.", WarningType.ErrorAtDownloadingProcess, () => StartCoroutine(TryToGetModelCoroutine(url, atSccess, atFailure, onDataExists)));
                }
                else
                {
                    LocalDataHandler.SaveModel(LocalPathsAPI.ModelDirectory, url, uwr.downloadHandler.data);
                    _currentUWR = null;
                     atSccess?.Invoke(LocalDataHandler.GetModel(LocalPathsAPI.ModelDirectory, url));
                    AppUI.Instance.EndLoading();
                }
            }
        }

    }
}
