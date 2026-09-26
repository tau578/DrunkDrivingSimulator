using UnityEngine;

/// <summary>Includes this HUD in URP camera effects instead of drawing it after them.</summary>
[DisallowMultipleComponent]
[RequireComponent(typeof(Canvas))]
public sealed class PostProcessedCanvas : MonoBehaviour
{
    [Tooltip("Defaults to the camera tagged MainCamera.")]
    [SerializeField] private Camera targetCamera;

    private Canvas hud;
    private RenderMode previousMode;
    private Camera previousCamera;
    private float previousDistance;

    private void OnEnable()
    {
        hud = GetComponent<Canvas>();
        previousMode = hud.renderMode;
        previousCamera = hud.worldCamera;
        previousDistance = hud.planeDistance;
        BindCamera();
    }

    private void LateUpdate()
    {
        // Also handles cameras spawned after the HUD or replaced during gameplay.
        BindCamera();
    }

    private void BindCamera()
    {
        Camera camera = targetCamera != null ? targetCamera : Camera.main;
        if (camera == null) return;
        hud.renderMode = RenderMode.ScreenSpaceCamera;
        hud.worldCamera = camera;
        // Keep the HUD close to the near plane so scene geometry cannot cover it.
        hud.planeDistance = Mathf.Lerp(camera.nearClipPlane, camera.farClipPlane, 0.00001f);
    }

    private void OnDisable()
    {
        if (hud == null) return;
        hud.renderMode = previousMode;
        hud.worldCamera = previousCamera;
        hud.planeDistance = previousDistance;
    }
}
