using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
public class ARObjectPooler : MonoBehaviour
{
    private ARTrackedImageManager _manager;
    private void Awake()
    {
        _manager = gameObject.GetComponent<ARTrackedImageManager>();
    }

    public void Init(Texture2D imageToAdd, GameObject obj)
    {
        var library = _manager.CreateRuntimeLibrary();

        if (library is MutableRuntimeReferenceImageLibrary mutableLibrary)
        {

            mutableLibrary.ScheduleAddImageWithValidationJob(imageToAdd, "target", 0.15f);
            _manager.referenceLibrary = mutableLibrary;
            _manager.trackedImagePrefab = obj;
            _manager.trackedImagePrefab.AddComponent<Lean.Touch.LeanPinchScale>();
            _manager.enabled = true;

            _manager.trackedImagesChanged += _manager_trackedImagesChanged;

        }
    }
    private void OnDisable()
    {
        _manager.trackedImagesChanged -= _manager_trackedImagesChanged;
    }

    private void _manager_trackedImagesChanged(ARTrackedImagesChangedEventArgs obj)
    {
        foreach (var item in obj.added)
        {
            _manager.trackedImagePrefab.transform.position = new Vector3(0, 0, 0);
            float targetImagesSizeX = item.size.x;
            float kkoef = _manager.trackedImagePrefab.transform.localScale.x / (targetImagesSizeX * GetBounds(_manager.trackedImagePrefab).size.x * 0.1f);
            _manager.trackedImagePrefab.transform.localScale /= kkoef;
        }
    }



    private Bounds GetBounds(GameObject obj)
    {
        Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();
        Bounds bounds = new Bounds(renderers[0].bounds.center, renderers[0].bounds.size);
        foreach (Renderer renderer in renderers)
        {
            bounds.Encapsulate(renderer.bounds);
        }
        return bounds;
    }
}
