using UnityEngine;

/// <summary>Includes this HUD in URP camera effects instead of drawing it after them.</summary>
[DisallowMultipleComponent]
[RequireComponent(typeof(Canvas))]
[ExecuteAlways]
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
        // Never attach a scene camera to the prefab asset or its isolated preview.
        if (hud == null || !gameObject.scene.IsValid() || !gameObject.scene.isLoaded) return;
        Camera camera = targetCamera != null ? targetCamera : Camera.main;
        if (camera == null) return;
        if (!Application.IsPlaying(gameObject) && camera.gameObject.scene != gameObject.scene) return;
        if (hud.renderMode != RenderMode.ScreenSpaceCamera)
            hud.renderMode = RenderMode.ScreenSpaceCamera;
        if (hud.worldCamera != camera)
            hud.worldCamera = camera;
        // Keep the HUD close to the near plane so scene geometry cannot cover it.
        float distance = Mathf.Lerp(camera.nearClipPlane, camera.farClipPlane, 0.00001f);
        if (!Mathf.Approximately(hud.planeDistance, distance))
            hud.planeDistance = distance;
    }

    private void OnDisable()
    {
        if (hud == null) return;
        hud.renderMode = previousMode;
        hud.worldCamera = previousCamera;
        hud.planeDistance = previousDistance;
    }
}
