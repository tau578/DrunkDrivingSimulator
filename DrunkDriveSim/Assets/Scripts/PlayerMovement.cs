using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float plrSpeed = 1;
    public float speedIncrement = 0.1f;
    public float horizontal;
    public CharacterController controller;
    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        MovePlayer();
    }
    void MovePlayer()
    {
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            Vector3 moveDirLeft = -horizontal * Vector2.left;
            controller.Move(moveDirLeft * plrSpeed * Time.deltaTime); 
            plrSpeed += speedIncrement;
        }
        //moves the player to the right using charactercontroller
        if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            Vector3 moveDirRight = horizontal * Vector2.right;
            controller.Move(moveDirRight * plrSpeed * Time.deltaTime);
            plrSpeed += speedIncrement;
        }
        if(Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            plrSpeed = 1;
        }
    }
}
