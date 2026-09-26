using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class SteeringWheelUI : MonoBehaviour
{
    [Header("Steering Wheel Sprite")]
    [SerializeField] private Image steeringWheelImage;

    [Header("Rotation Angles")]
    [SerializeField] private float rightAngle = -45f;
    [SerializeField] private float leftAngle = 45f;

    [Header("Rotation Speed")]
    [SerializeField] private float rotationSpeed = 180f;

    private float currentAngle = 0f;

    private void Update()
    {
        if (steeringWheelImage == null)
            return;

        float targetAngle = 0f;

        Keyboard keyboard = Keyboard.current;

        if (keyboard == null)
            return;

        if (keyboard.dKey.isPressed || keyboard.rightArrowKey.isPressed)
        {
            targetAngle = rightAngle;
        }
        else if (keyboard.aKey.isPressed || keyboard.leftArrowKey.isPressed)
        {
            targetAngle = leftAngle;
        }

        currentAngle = Mathf.MoveTowards(
            currentAngle,
            targetAngle,
            rotationSpeed * Time.deltaTime
        );

        steeringWheelImage.rectTransform.localRotation =
            Quaternion.Euler(0f, 0f, currentAngle);
    }
}