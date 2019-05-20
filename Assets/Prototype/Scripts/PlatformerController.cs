// <copyright file="PlayerInputModule2D.cs" company="DIS Copenhagen">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Benno Lueders</author>
// <date>07/14/2017</date>

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script for general purpose 2D controls for any object that can move and jump when grounded.
/// </summary>
[RequireComponent (typeof(Rigidbody))]
public class PlatformerController : MonoBehaviour
{
	[HideInInspector] public bool IsGrounded { get { return grounded; } }

	[Tooltip ("Can this object move.")]
	public bool canMove = true;

	[Header ("Controls")]
	[Tooltip ("Base maximum horizontal movement speed.")]
	[SerializeField] public float speed = 5;
	[Tooltip ("Start velocity when jumping.")]
	[SerializeField] public float jumpVelocity = 15;
	[Tooltip ("Downwards acceleration.")]
	[SerializeField] public float gravity = 40;
	[Tooltip ("Time delay a jump is still performed, when grounding is gained after the jump button was pressed in the air.")]
	[SerializeField] float jumpingToleranceTimer = .1f;
	[Tooltip ("Time delay that a jump is still allowed when grounding is lost.")]
	[SerializeField] float groundingToleranceTimer = .1f;

	[Header ("Grounding")]
	[Tooltip ("Offset of the grounding raycasts (red lines)")]
	[SerializeField] Vector2 groundCheckOffset = new Vector2 (0, 0.1f);
	[Tooltip ("Width of the grounding raycasts.")]
	[SerializeField] float groundCheckWidth = 1;
	[Tooltip ("Distance of the grounding raycasts.")]
	[SerializeField] float groundCheckDepth = 0.2f;
	[Tooltip ("Number of the grounding Raycsts. Will be evenly spread over the width")]
	[SerializeField] int groundCheckRayCount = 3;
	[Tooltip ("Layers to be considered ground.")]
	[SerializeField] LayerMask groundLayers = 0;

	bool grounded = false;
	Rigidbody rb2d = null;
	SpriteRenderer sr = null;
	Animator anim = null;

	float lostGroundingTime = float.NegativeInfinity;
	float lastJumpTime = float.NegativeInfinity;
    float lastInputJump = float.NegativeInfinity;
    int facing = 1;


    Player player;
    PlayerActions actions;
    AudioHandler audioHandler;

	void Start ()
	{
        player = GetComponent<Player>();
        actions = player.Actions;
		canMove = true;
		rb2d = GetComponent<Rigidbody> ();
		anim = GetComponentInChildren<Animator> ();
		sr = GetComponent<SpriteRenderer> ();
        audioHandler = GetComponent<AudioHandler>();
	}



	/// <summary>
	/// Controls the basic update of the controller. This uses fixed update, since the movement is physics driven and has to be synched with the physics step.
	/// </summary>
	void FixedUpdate ()
	{
		UpdateGrounding ();

		Vector2 vel = rb2d.velocity;

		if (canMove) {
			vel.x = actions.Move.Value.x * speed;

			if (CheckJumpInput () && PermissionToJump ()) {
                vel = ApplyJump(vel);
            }
		}

		vel.y += -gravity * Time.deltaTime;
		rb2d.velocity = vel;

		UpdateAnimations ();
	}

	Vector2 ApplyJump (Vector2 vel)
	{
        audioHandler.PlayOneShotByName("Jump");
        vel.y = jumpVelocity;
		lastJumpTime = Time.time;
		grounded = false;
		return vel;
	}

	/// <summary>
	/// Updates grounded and lastGroundingTime. 
	/// </summary>
	void UpdateGrounding ()
	{
		Vector3 groudCheckCenter = new Vector3 (transform.position.x + groundCheckOffset.x, transform.position.y + groundCheckOffset.y);
		Vector3 groundCheckStart = groudCheckCenter + Vector3.left * groundCheckWidth * 0.5f;
		if (groundCheckRayCount > 1) {
			for (int i = 0; i < groundCheckRayCount; i++)
			{
				RaycastHit hit;
				Physics.Raycast (groundCheckStart, Vector3.down,out hit, groundCheckDepth, groundLayers);
				if (hit.collider != null) {
					grounded = true;
					return;
				}
				groundCheckStart += Vector3.right * (1.0f / (groundCheckRayCount - 1.0f)) * groundCheckWidth;
			}
		}
		if (grounded) {
			lostGroundingTime = Time.time;
		}
		grounded = false;
	}

	void UpdateAnimations ()
	{
		if (!canMove) {
			anim.SetBool ("grounded", false);
			return;
		}
		if (rb2d.velocity.x > 0 && facing == -1) {
			facing = 1;
			transform.rotation = Quaternion.Euler(0, 0, 0); 

			
			
		} else if (rb2d.velocity.x < 0 && facing == 1) {
			facing = -1;
			transform.rotation = Quaternion.Euler(0, 180, 0);


		}
		anim.SetBool ("grounded", grounded);
		anim.SetFloat ("speed", Mathf.Abs (rb2d.velocity.x));

			anim.SetFloat("velocity", rb2d.velocity.y);
	}

	/// <summary>
	/// Return true if the character can jump right now.
	/// </summary>
	/// <returns><c>true</c>, if to jump was permissioned, <c>false</c> otherwise.</returns>
	bool PermissionToJump ()
	{
		bool wasJustgrounded = Time.time < lostGroundingTime + groundingToleranceTimer;
		bool hasJustJumped = Time.time <= lastJumpTime + Time.deltaTime;
		return (grounded || wasJustgrounded) && !hasJustJumped;
	}

	/// <summary>
	/// Checks if the jump input is true right now.
	/// </summary>
	/// <returns><c>true</c>, if jump input detected, <c>false</c> otherwise.</returns>
	bool CheckJumpInput ()
	{
		if (actions.Jump) {
			lastInputJump = Time.time;
			return true;
		}
		if (Time.time < lastInputJump + jumpingToleranceTimer) {
			return true;
		}
		return false;
	}

	/// <summary>
	/// Pushback the object controlled by this instance with the specified force.
	/// </summary>
	/// <param name="force">Force to push the character back</param>
	public void Pushback (Vector3 force)
	{
		rb2d.velocity = force;
		lastJumpTime = Time.time;
		grounded = false;
		lostGroundingTime = Time.time;
	}

	/// <summary>
	/// Make the object controlled by this instance jump immediately. 
	/// </summary>
	/// <param name="strength">Strength.</param>
	public void ForceJump (float strength)
	{
		rb2d.velocity = new Vector3 (rb2d.velocity.x, strength, 0);
		lastJumpTime = Time.time;
		grounded = false;
		lostGroundingTime = Time.time;
	}

	/// <summary>
	/// Used to draw the red lines for the grounding raycast. Only active in the editor and when the instance is selected.
	/// </summary>
	void OnDrawGizmosSelected(){
		Vector3 groudCheckCenter = new Vector3 (transform.position.x + groundCheckOffset.x, transform.position.y + groundCheckOffset.y, 0);
		Vector3 groundCheckStart = groudCheckCenter + Vector3.left * groundCheckWidth * 0.5f;
		if (groundCheckRayCount > 1) {
			for (int i = 0; i < groundCheckRayCount; i++) {
				Debug.DrawLine (groundCheckStart, groundCheckStart + Vector3.down * groundCheckDepth, Color.red);
				groundCheckStart += Vector3.right * (1.0f / (groundCheckRayCount - 1.0f)) * groundCheckWidth;
			}
		}
	}
}
