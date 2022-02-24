using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Move Parameters")]
    public CharacterController controller;
    public float speed = 3f;
    
    [Header("Crouch Parameters")]
    public Transform playerCamera;
    private float crouchHeight = 1f;
    private float standingHeight = 2f;
    private float timeToCrouch = 0.25f;
    private bool isCrouching;
    private Vector3 RayOrigin;

    void Start()
    {

    }

    void Update()
    {
        isCrouching = Input.GetKey(KeyCode.LeftControl);

        if(isCrouching)
        {
            speed = 1f;
        }else
            {
                speed = 3f;

                if(Input.GetKey(KeyCode.LeftShift))
                {
                    print("running");
                    speed = 6f;
                }
            }
        if(controller.height == 1f)
        {
            speed = 1f;
        }

        Move();
      /*if(canCrouch)
        {
            Crouch();
        }*/
    }

    void FixedUpdate()
    {
        var desiredHeight = isCrouching ? crouchHeight : standingHeight;

        if(controller.height != desiredHeight)
        {
            AdjustHeight(desiredHeight);

            var camPos = playerCamera.transform.position;
            camPos.y = controller.height;

            playerCamera.transform.position = camPos;
        }

        RayOrigin = new Vector3(transform.position.x,1,transform.position.z);
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if(Physics.Raycast(RayOrigin, transform.TransformDirection(Vector3.up), out hit, 1))
        {
            Debug.DrawRay(RayOrigin, transform.TransformDirection(Vector3.up) * hit.distance, Color.yellow);
            Debug.Log("Did Hit");

            controller.height = 1f;
            controller.center = new Vector3(0,0.5f,0);
            var camPos = playerCamera.transform.position;
            camPos.y = controller.height;
            playerCamera.transform.position = camPos;
        }
        else
        {
            Debug.DrawRay(RayOrigin, transform.TransformDirection(Vector3.up) * 1, Color.white);
            Debug.Log("Did not Hit");
        }

    }

    public void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);
    }
/*
    public void Crouch()
    {
        isCrouching = Input.GetKeyDown(KeyCode.LeftControl);
    }
*/
    private void AdjustHeight(float height)
    {
        float center = height / 2;
        controller.height = Mathf.Lerp(controller.height, height, timeToCrouch);
        controller.center = Vector3.Lerp(controller.center, new Vector3(0,center,0),timeToCrouch);
    }
}


//transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
