using UnityEngine;

public class LevelVisualizer : MonoBehaviour
{
    [SerializeField]
    private LayerMask defaultLayerMask = new LayerMask();
    [SerializeField]
    private LayerMask invisibleLayerMask = new LayerMask();

    private Camera mainCamera = null;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    public void Show()
    {
        if (mainCamera == null)
        {
            Debug.LogError("No main Camera assigned.");
            return;
        }

        mainCamera.cullingMask = defaultLayerMask;
    }

    public void Hide()
    {
        if (mainCamera == null)
        {
            Debug.LogError("No main Camera assigned.");
            return;
        }

        mainCamera.cullingMask = invisibleLayerMask;
    }
}
