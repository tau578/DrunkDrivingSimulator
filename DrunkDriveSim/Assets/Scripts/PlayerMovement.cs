using Unity.VisualScripting;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;
using Unity.Mathematics;

public class PlayerMovement : MonoBehaviour
{
    public float plrSpeed = 1;
    public float speedIncrement = 0.1f;
    public float horizontal;

    public float camRot;
    public float rotIncrement = 0.1f;
    public float rotSpeed = 0.1f;
    public CharacterController controller;
    public CameraShake camShakeScript;

    public float larpValue = 0.1f;

    public GameObject cam;
    
    void Start()
    {
        camShakeScript = camShakeScript.GetComponent<CameraShake>();
    }
    void Update()
    {
        //camShakeScript.ConstantShake();
        horizontal = Input.GetAxisRaw("Horizontal");
        MovePlayer();
        larpValue = larpValue * Time.deltaTime;
        if(larpValue > 1f)
        {
            larpValue = 0.1f;
        }
        if(Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            plrSpeed = 1;
            larpValue += 0.1f;
            cam.transform.rotation = Quaternion.Euler(0f, 0f, Mathf.LerpAngle(camRot, 0f, Time.deltaTime));
        }
        camRot = Mathf.Clamp(camRot, -20f, 20f);
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Border")
        {
            camShakeScript.StartShake(0.2f, 1f);
        }
    }
    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Border" && Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            camShakeScript.StartShake(0.2f, 1f);
        }
    }
    /*void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Border")
        {
            camShakeScript.ConstantShake();
        }
    }*/
    void MovePlayer()
    {
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            Vector3 moveDirLeft = -horizontal * Vector2.left;
            controller.Move(moveDirLeft * plrSpeed * Time.deltaTime); 
            plrSpeed += speedIncrement;
            rotSpeed += 0.02f;
            camRot = camRot + rotIncrement * rotSpeed;
            cam.transform.rotation = Quaternion.Euler(0f, 0f, camRot);
        }
        //moves the player to the right using charactercontroller
        if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            Vector3 moveDirRight = horizontal * Vector2.right;
            controller.Move(moveDirRight * plrSpeed * Time.deltaTime);
            plrSpeed += speedIncrement;
            rotSpeed += 0.02f;
            camRot = camRot - rotIncrement * rotSpeed;
            cam.transform.rotation = Quaternion.Euler(0f, 0f, camRot);
        }
        if(Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            plrSpeed = 1;
            larpValue += 0.1f;
            cam.transform.rotation = Quaternion.Euler(0f, 0f, Mathf.LerpAngle(camRot, 0f, larpValue));
            rotSpeed = 0.1f;
            /*if(camRot > 0)
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
            }*/
        }
    }
}
