// <copyright file="PlayerInputModule2D.cs" company="DIS Copenhagen">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Benno Lueders</author>
// <date>07/14/2017</date>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Module for player controller input when using a PlatformController2D. Uses standart Horizontal and Vertical input axis as well as Jump button.
/// </summary>
[RequireComponent(typeof(PlatformerController))]
public class PlayerInput : MonoBehaviour
{
    PlatformerController controller;
    GunRotation gunPivot;
    ShootBehavior shootBehavior;
    string playerID;
    public string jumpButton;
    public string shootBinding;
    public GameObject gun;
    [Tooltip("Aim in the direction of the mouse's cursor instead of using controller input. Click to shoot.")]
    public bool useMouseAim;
    [Tooltip("Move the character with keys instead of using controller input.")]
    public bool useKeyboardMovement;

    void Start()
    {
        controller = GetComponentInParent<PlatformerController>();
        gunPivot = GetComponentInChildren<GunRotation>();
        shootBehavior = GetComponentInChildren<ShootBehavior>();
        playerID = GetComponentInParent<Player>().playerName;
    }

    void FixedUpdate()
    {
        Vector2 input = Vector2.zero;
        //Movement input
        if (useKeyboardMovement)
        {
            input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }
        else
        {
            input = new Vector2(Input.GetAxisRaw("L_XAxis_" + playerID), Input.GetAxisRaw("L_YAxis_" + playerID));
            if (input.magnitude > 1)
            {
                input.Normalize();
            }
        }
        controller.input = input;

        //Gun aiming
        Vector3 aimInput = Vector3.zero;
        if (useMouseAim)
        {
            //Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
            //Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Debug.Log(Input.mousePosition);
            //Vector3 gunPivotPosition = gunRotation.transform.position;
            //Vector3 targetPosition = new Vector3(mouseWorldPosition.x, mouseWorldPosition.y, gunPivotPosition.z);
            //Vector3 aimInput = targetPosition - gunPivotPosition;

            Vector2 mousePosition = Input.mousePosition;
            Vector3 gunPivotScreenPosition = Camera.main.WorldToScreenPoint(gunPivot.transform.position);
            aimInput = new Vector3(mousePosition.x, mousePosition.y, gunPivotScreenPosition.z) - gunPivotScreenPosition;

            Debug.DrawLine(gunPivot.transform.position, aimInput, Color.red);
            gunPivot.aimInput = aimInput;
        }
        else
        {
            float aimX = Input.GetAxis("R_XAxis_" + playerID);
            float aimY = -Input.GetAxis("R_YAxis_" + playerID);
            aimInput = new Vector3(aimX, aimY, 0);
            if (aimInput != Vector3.zero)
            {
                gunPivot.aimInput = aimInput;
            }
        }

        // Jump input
        if (useKeyboardMovement)
        {
            controller.inputJump = Input.GetButtonDown("Jump");
        }
        else
        {
            controller.inputJump = Input.GetButtonDown(jumpButton + "_" + playerID);
        }

        // Shoot input
        //NOTE TO SELF: put detection into shoot script, only handle interpretation.
        float shootInput = 0;
        if (useMouseAim)
        {
            shootInput = Input.GetButtonDown("Shoot") ? 1 : 0;
        }
        else
        {
            shootInput = Input.GetAxisRaw(shootBinding + "_" + playerID);
        }
        shootBehavior.shootInput = shootInput;
    }
}