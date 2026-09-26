using Unity.VisualScripting;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;

public class PlayerMovement : MonoBehaviour
{
    public float plrSpeed = 1;
    public float speedIncrement = 0.1f;
    public float horizontal;

    public float camRot;
    public float rotIncrement = 0.1f;
    public CharacterController controller;

    static float larpValue = 0.1f;

    public GameObject cam;
    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        MovePlayer();
        larpValue = larpValue * Time.deltaTime;
        if(larpValue > 1f)
        {
            larpValue = 0.1f;
        }
    }
    void MovePlayer()
    {
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            Vector3 moveDirLeft = -horizontal * Vector2.left;
            controller.Move(moveDirLeft * plrSpeed * Time.deltaTime); 
            plrSpeed += speedIncrement;
            camRot += rotIncrement;
            cam.transform.rotation = Quaternion.Euler(0f, 0f, camRot);
        }
        //moves the player to the right using charactercontroller
        if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            Vector3 moveDirRight = horizontal * Vector2.right;
            controller.Move(moveDirRight * plrSpeed * Time.deltaTime);
            plrSpeed += speedIncrement;
            camRot -= rotIncrement;
            cam.transform.rotation = Quaternion.Euler(0f, 0f, camRot);
        }
        if(Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            plrSpeed = 1;
            cam.transform.rotation = Quaternion.Euler(0f, 0f, camRot);
            if(camRot > 0)
            {
                camRot -= rotIncrement;
                if(camRot == 0)
                {
                    return;
                }
            }
            if(camRot < 0)
            {
                camRot += rotIncrement;
                if(camRot == 0)
                {
                    return;
                }
            }
        }
    }
    void CameraShake()
    {
        
    }
}
