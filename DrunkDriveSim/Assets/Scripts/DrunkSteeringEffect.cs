using UnityEngine;
using UnityEngine.InputSystem;

public class DrunkSteeringEffect : MonoBehaviour
{
    [SerializeField] private Material drunkMaterial;

    [Header("Steering")]
    [SerializeField] private float leftOffset = 0f;
    [SerializeField] private float centerOffset = 0.5f;
    [SerializeField] private float rightOffset = 1f;

    [SerializeField] private float shiftSpeed = 2f;
    [SerializeField] private float returnSpeed = 1.2f;

    private float currentOffset = 0.5f;

    private static readonly int SteerOffsetID =
        Shader.PropertyToID("_SteerOffset");

    private void Start()
    {
        if (drunkMaterial != null)
            drunkMaterial.SetFloat(SteerOffsetID, currentOffset);
    }

    private void Update()
    {
        if (drunkMaterial == null || Keyboard.current == null)
            return;

        float targetOffset = centerOffset;

        bool right =
            Keyboard.current.dKey.isPressed ||
            Keyboard.current.rightArrowKey.isPressed;

        bool left =
            Keyboard.current.aKey.isPressed ||
            Keyboard.current.leftArrowKey.isPressed;

        if (left)
            targetOffset = leftOffset;
        else if (right)
            targetOffset = rightOffset;

        float speed = Mathf.Approximately(targetOffset, centerOffset)
            ? returnSpeed
            : shiftSpeed;

        currentOffset = Mathf.MoveTowards(
            currentOffset,
            targetOffset,
            speed * Time.deltaTime
        );

        drunkMaterial.SetFloat(SteerOffsetID, currentOffset);
    }
}