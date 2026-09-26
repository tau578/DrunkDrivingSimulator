using UnityEngine;

public class DrunkTitleSway : MonoBehaviour
{
    [Header("Rotation")]
    [SerializeField] private float maxRotation = 4f;
    [SerializeField] private float rotationSpeed = 1.2f;

    [Header("Position Sway")]
    [SerializeField] private float horizontalAmount = 8f;
    [SerializeField] private float verticalAmount = 5f;
    [SerializeField] private float moveSpeed = 0.8f;

    [Header("Extra Drunkiness")]
    [SerializeField] private float secondaryRotationAmount = 1.5f;
    [SerializeField] private float secondarySpeed = 0.45f;

    private RectTransform rectTransform;
    private Vector2 startPosition;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        startPosition = rectTransform.anchoredPosition;
    }

    private void Update()
    {
        float time = Time.unscaledTime;

        // Основное покачивание
        float mainRotation =
            Mathf.Sin(time * rotationSpeed) * maxRotation;

        // Дополнительная медленная фаза,
        // чтобы движение было менее механическим
        float secondaryRotation =
            Mathf.Sin(time * secondarySpeed + 1.7f)
            * secondaryRotationAmount;

        float finalRotation =
            mainRotation + secondaryRotation;

        rectTransform.localRotation =
            Quaternion.Euler(0f, 0f, finalRotation);

        // Независимое плавание по X и Y
        float x =
            Mathf.Sin(time * moveSpeed + 0.4f)
            * horizontalAmount;

        float y =
            Mathf.Sin(time * moveSpeed * 0.73f + 2.1f)
            * verticalAmount;

        rectTransform.anchoredPosition =
            startPosition + new Vector2(x, y);
    }
}