﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour {

    Camera cam;
    Transform player;
    [SerializeField] float maxCameraLength = 40f;
    [SerializeField] float minCameraLength = -200f;
    [SerializeField] float maxCameraHeight = 24.4f;
    [SerializeField] float minCameraHeight = -50f;
    [SerializeField] float offsetY = 2.5f;
    [SerializeField] float movementSpeed = 0.2f;

    [SerializeField]
    float horizontalDeadZoneCenter = 0.4f;
    float hdzc;

    [SerializeField]
    float horizontalDeadZoneLength = 0.15f;
    [SerializeField]
    float verticalDeadZoneCenter = 0.25f;
    [SerializeField]
    float verticalDeadZoneLength = 0f;

    // Used to determine when the player starts moving
    private CustomPlatformerCharacter2D playerMov;
    private Rigidbody2D playerRigidbody;

    //private Vector3 initialPos;
    private Vector3 velocity = Vector3.zero;

    private bool trackPlayerFall = false;


    void Start ()
    {
        hdzc = horizontalDeadZoneCenter;

        cam = GetComponent<Camera>();
        
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        playerMov = player.gameObject.GetComponent<CustomPlatformerCharacter2D>();
        playerRigidbody = player.gameObject.GetComponent<Rigidbody2D>();
        
        Vector3 newPos = new Vector3(player.position.x, player.position.y + offsetY, -10f);
        gameObject.transform.position = newPos;
    }
	
    // LateUpdate fires after all other update functions
	void LateUpdate ()
    {
        //gameObject.transform.position = newPos;

        Vector3 playerPos = cam.WorldToViewportPoint(player.position);


        // Adjust the camera position if the player has moved out of the "neutral position" horizontally or vertically.
        if(playerPos.x > (horizontalDeadZoneCenter + horizontalDeadZoneLength / 2.0f) || 
            playerPos.x < (horizontalDeadZoneCenter - horizontalDeadZoneLength / 2.0f) ||
            playerPos.y > (verticalDeadZoneCenter + verticalDeadZoneLength / 2.0f) || 
            playerPos.y < (verticalDeadZoneCenter - verticalDeadZoneLength / 2.0f))
        {
            // Adjusts camera to display more of the level in the player's direction, SMW style
            if (playerPos.x > (horizontalDeadZoneCenter + horizontalDeadZoneLength / 2.0f))
                horizontalDeadZoneCenter = hdzc;
            if (playerPos.x < (horizontalDeadZoneCenter - horizontalDeadZoneLength / 2.0f))
                horizontalDeadZoneCenter = 1 - hdzc;

            float offset = (0.5f - horizontalDeadZoneCenter) / 0.1f;

            Vector3 newPos = new Vector3(player.position.x + (2.0f * offset), transform.position.y, -10f);
            float speed = movementSpeed;

            // Track player y position if the player is not jumping.
            if (playerMov.GetIsGrounded())
            {
                newPos.y = player.position.y + offsetY;
            }
            else if(!playerMov.GetIsGrounded() && playerPos.y < (verticalDeadZoneCenter - verticalDeadZoneLength / 2.0f))
            {
                newPos.y = player.position.y + offsetY;

                if(playerRigidbody.velocity.y < 0.0f)
                {
                    transform.position = newPos.y < minCameraHeight ?
                        new Vector3(transform.position.x, minCameraHeight, transform.position.z) : 
                        new Vector3(transform.position.x, newPos.y, transform.position.z);
                }
            }


            // Clamp camera height
            if (newPos.y > maxCameraHeight)
                newPos.y = maxCameraHeight;
            else if (newPos.y < minCameraHeight)
                newPos.y = minCameraHeight;

            // Clap camera x dimension
            if (newPos.x > maxCameraLength)
                newPos.x = maxCameraLength;
            else if (newPos.x < minCameraLength)
                newPos.x = minCameraLength;

            transform.position = Vector3.SmoothDamp(transform.position, newPos, ref velocity, speed);

            //Debug.Log("Adjust");
        }

    }
}