using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class RigidBodyMovement : MonoBehaviour{
    private Vector3 PlayerMovmentInput;
    private Vector2 PlayerMouseInput;
    private float xRot;
    //References what the Dev inputs in the Inspector.
    [SerializeField] private LayerMask FloorMask;
    [SerializeField] private Transform FeetTransform;
    [SerializeField] private Transform PlayerCamera;
    [SerializeField] private Rigidbody PlayerBody;
 
    [SerializeField] private float Speed;
    [SerializeField] private float Sensitivity;
    [SerializeField] private float Jumpforce;
 
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
 
    private void Update()
    {
        //gets players input every frame
        PlayerMovmentInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        PlayerMouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        //calls the methods that move the player
        MovePlayer();
        MovePlayerCamera();
    }
 
    private void MovePlayer()
    {
        //moves the player
        Vector3 MoveVector = transform.TransformDirection(PlayerMovmentInput) * Speed;
        PlayerBody.velocity = new Vector3(MoveVector.x, PlayerBody.velocity.y, MoveVector.z);
        //checks if the player told the character to jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //makes sure player is on the ground before jumping
            if(Physics.CheckSphere(FeetTransform.position, 0.1f, FloorMask))
            {
                PlayerBody.AddForce(Vector3.up * Jumpforce, ForceMode.Impulse);
            }
        }
    }
 
    private void MovePlayerCamera()
    {
        //handles moving the camera
        xRot -= PlayerMouseInput.y * Sensitivity;
        //makes sure the character cant look past straight up or straight down
        xRot = Mathf.Clamp(xRot, -90f, 90f);
 
        //rotates the character
        transform.Rotate(0f, PlayerMouseInput.x * Sensitivity, 0f);
        PlayerCamera.transform.localRotation = Quaternion.Euler (xRot, 0f, 0f);
    }
}
 
