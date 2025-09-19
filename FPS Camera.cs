using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CharacterController))]

public class NewBehaviourScript : MonoBehaviour
{
    //variables
    public Camera playerCamera;
    public float walk = 6f;
    public float run = 1f;
    public float jump = 7f;
    public float gravity = 10f;

    public float lookSpeed = 2f;
    public  float lookXlimit = 24f;

    Vector3 direction = Vector3.zero;
    float rotation = 0;

    public bool canMove = true;

    CharacterController characterController;


    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;  //locks the camera in the middle of the screen
       
    }

    
    void Update()
    {
      Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        bool isRunning = Input.GetKey(KeyCode.LeftShift); //sets so that leftshift is used to run
        float curspeedX = canMove ? (isRunning ? run : walk) * Input.GetAxis("Vertical") : 0;
        float curspeedY = canMove ? (isRunning ? run : walk) * Input.GetAxis("Horizontal") : 0;
        float MoveDirectionY = direction.y;
        direction = (forward * curspeedX) + (right * curspeedY);


        //jumping

        if (Input.GetButton("Jump") && canMove && characterController.isGrounded) //checks if the player is standing on surface
        {
            direction.y = jump; //moves the player up 
        }
        else
        {
            direction.y = MoveDirectionY;
        }
        if (!characterController.isGrounded) //checks if the player is not standing on a surface
        {
            direction.y -= gravity * Time.deltaTime; //prevents the player from jumping
        }


        characterController.Move(direction * Time.deltaTime);

        if (canMove)
        {
            rotation += -Input.GetAxis("Mouse Y") * lookSpeed; //determines the sensitivity of the mouse in game
            rotation = Mathf.Clamp(rotation, -lookXlimit, lookXlimit); //adds a limit to how far the player can turn the camera 
            playerCamera.transform.localRotation = Quaternion.Euler(rotation, 0, 0);
            transform.rotation *= Quaternion.Euler(0 , Input.GetAxis("Mouse X") *lookSpeed, 0);
        }
   
      
  
    }
}
