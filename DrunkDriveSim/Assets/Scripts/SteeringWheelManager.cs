using UnityEngine;
using UnityEngine.InputSystem;

public class SteeringWheelManager : MonoBehaviour
{
    [Header("Steering Wheel Objects")]
    [SerializeField] private GameObject steeringWheel;
    [SerializeField] private GameObject steeringWheelLeft;
    [SerializeField] private GameObject steeringWheelRight;

    private enum SteeringState
    {
        Neutral,
        Left,
        Right
    }

    private SteeringState currentState = SteeringState.Neutral;

    private void Start()
    {
        SetState(SteeringState.Neutral);
    }

    private void Update()
    {
        Keyboard keyboard = Keyboard.current;

        if (keyboard == null)
            return;

        bool leftHeld =
            keyboard.aKey.isPressed ||
            keyboard.leftArrowKey.isPressed;

        bool rightHeld =
            keyboard.dKey.isPressed ||
            keyboard.rightArrowKey.isPressed;

        bool leftPressedThisFrame =
            keyboard.aKey.wasPressedThisFrame ||
            keyboard.leftArrowKey.wasPressedThisFrame;

        bool rightPressedThisFrame =
            keyboard.dKey.wasPressedThisFrame ||
            keyboard.rightArrowKey.wasPressedThisFrame;

        // Если игрок только что нажал влево —
        // левое положение получает приоритет.
        if (leftPressedThisFrame)
        {
            SetState(SteeringState.Left);
            return;
        }

        // Если игрок только что нажал вправо —
        // правое положение получает приоритет.
        if (rightPressedThisFrame)
        {
            SetState(SteeringState.Right);
            return;
        }

        // Проверяем текущее состояние после отпускания клавиши.

        if (currentState == SteeringState.Left)
        {
            // Левая клавиша всё ещё удерживается.
            if (leftHeld)
                return;

            // Левую отпустили, но правая всё ещё нажата.
            if (rightHeld)
            {
                SetState(SteeringState.Right);
                return;
            }

            // Ничего не нажато.
            SetState(SteeringState.Neutral);
            return;
        }

        if (currentState == SteeringState.Right)
        {
            // Правая клавиша всё ещё удерживается.
            if (rightHeld)
                return;

            // Правую отпустили, но левая всё ещё нажата.
            if (leftHeld)
            {
                SetState(SteeringState.Left);
                return;
            }

            // Ничего не нажато.
            SetState(SteeringState.Neutral);
            return;
        }

        // Мы были в Neutral, но одна из клавиш уже удерживается.
        if (leftHeld)
        {
            SetState(SteeringState.Left);
        }
        else if (rightHeld)
        {
            SetState(SteeringState.Right);
        }
        else
        {
            SetState(SteeringState.Neutral);
        }
    }

    private void SetState(SteeringState newState)
    {
        currentState = newState;

        steeringWheel.SetActive(newState == SteeringState.Neutral);
        steeringWheelLeft.SetActive(newState == SteeringState.Left);
        steeringWheelRight.SetActive(newState == SteeringState.Right);
    }
}