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

    void Start()
    {

    }

    void Update()
    {
        isCrouching = Input.GetKey(KeyCode.LeftControl);

        if(isCrouching)
        {
            speed = 1f;
        }else{speed = 3f;}

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
