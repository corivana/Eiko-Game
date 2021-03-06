﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterController))]
public class PlayerMovementManager : MonoBehaviour
{
    public static PlayerMovementManager instance;

    //***THE CODE FOR ROTATING THE PLAYER IS IN THE CAMERA CONTROLLER SCRIPT***

    //Movement Variables
    [SerializeField] private CharacterController controller;
    [SerializeField] private float gravityScale = 1f;
    public float moveSpeed;
    public float jumpForce;
    private Vector3 moveDirection;
    public Animator anim;


    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
    }

    void PlayerMovement()
    {
        //Move up, down, left, right with WASD
        float yStore = moveDirection.y;
        moveDirection = (transform.forward * Input.GetAxis("Vertical")) + (transform.right * Input.GetAxis("Horizontal"));
        moveDirection = moveDirection.normalized * moveSpeed;
        moveDirection.y = yStore; //Keep the same y value

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S)) //|| Input.GetKeyDown(KeyCode.S)) //|| (Input.GetKeyDown(KeyCode.W) && Input.GetKeyDown(KeyCode.A)) || (Input.GetKeyDown(KeyCode.W) && Input.GetKeyDown(KeyCode.D)))
        {
            anim.SetInteger(HashIDs.forwardCond_Int, 1);
        }

        else if (Input.GetKeyDown(KeyCode.A) && !(Input.GetKey(KeyCode.W))) // && !(Input.GetKey(KeyCode.W)))
        {
            anim.SetInteger(HashIDs.leftCond_Int, 1);
        }
        else if (Input.GetKeyDown(KeyCode.D) && !(Input.GetKey(KeyCode.W))) // && !(Input.GetKey(KeyCode.W)))
        {
            anim.SetInteger(HashIDs.rightCond_Int, 1);
        }
        else if(!(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)))
        {
            anim.SetInteger(HashIDs.forwardCond_Int, 0);
            anim.SetInteger(HashIDs.backwardCond_Int, 0);
            anim.SetInteger(HashIDs.leftCond_Int, 0);
            anim.SetInteger(HashIDs.rightCond_Int, 0);
        }


        //check if the player is on the ground before jumping
        if (controller.isGrounded)
        {
            moveDirection.y = 0f;
            //Jumping
            if (Input.GetButtonDown("Jump"))
            {
                moveDirection.y = jumpForce;
            }

        }


        //Apply Gravity to Y axis
        moveDirection.y += (Physics.gravity.y * gravityScale * Time.deltaTime);

        //Input movement direction into CharacterController
        controller.Move(moveDirection * Time.deltaTime);
    }
}
