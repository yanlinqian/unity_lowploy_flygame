using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public float speed = 100.0f;            // The speed that the player will move at.
	public float rotationspeed=100.0f;

	Vector3 movement;                   // The vector to store the direction of the player's movement.
	Vector3 default_movement=new Vector3(0.0f,0.0f,1.0f);				// The vector to store the default movement as it is airpiane.
	Vector3 sidelurch_vector=new Vector3(0f,0f,0f); //The vector to store the vector of side movement, later used for making sidelurch effect.
	//Animator anim;                      // Reference to the animator component.
	Rigidbody playerRigidbody;          // Reference to the player's rigidbody.
	int floorMask;                      // A layer mask so that a ray can be cast just at gameobjects on the floor layer.
	float camRayLength = 100f;          // The length of the ray from the camera into the scene.

	void Awake ()
	{
		// Create a layer mask for the floor layer.
		floorMask = LayerMask.GetMask ("Floor");

		// Set up references.
		//anim = GetComponent <Animator> ();
		playerRigidbody = GetComponent <Rigidbody> ();
	}


	void FixedUpdate ()
	{
		Move();
	// Store the input axes.
//	float h = Input.GetAxisRaw ("Horizontal");
//	float v = Input.GetAxisRaw ("Vertical");
//		
//	// Move the player around the scene.
//	default_movement=Move (h, v);
//
//	// Turn the player to face the direction he is heading for.
		Turning (transform.position+playerRigidbody.velocity);



	// Animate the	 player.
	//Animating (h, v);
	}

	void Move()
	{

		Quaternion AddRot = Quaternion.identity;
		float roll = 0;
		float pitch = 0;
		float yaw = 0;
		roll = Input.GetAxis("Roll") * (Time.fixedDeltaTime * rotationspeed);
		pitch = Input.GetAxis("Pitch") * (Time.fixedDeltaTime * rotationspeed);
		yaw = Input.GetAxis("Yaw") * (Time.fixedDeltaTime * rotationspeed);
		AddRot.eulerAngles = new Vector3(-pitch, yaw, -roll);
		playerRigidbody.rotation *= AddRot;
		Vector3 AddPos = Vector3.forward;
		AddPos = playerRigidbody.rotation * AddPos;
		//playerRigidbody.velocity = AddPos * (Time.fixedDeltaTime * speed);
		playerRigidbody.MovePosition(transform.position+AddPos * (Time.fixedDeltaTime * speed));
	}
		
	void Turning (Vector3 direction)
	{
		direction = direction.normalized;

		playerRigidbody.MoveRotation (Quaternion.Lerp(playerRigidbody.rotation,Quaternion.LookRotation(direction),Time.time * 0.2f));

//			// Create a ray from the mouse cursor on screen in the direction of the camera.
//			Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);
//
//			// Create a RaycastHit variable to store information about what was hit by the ray.
//			RaycastHit floorHit;
//
//			// Perform the raycast and if it hits something on the floor layer...
//			if(Physics.Raycast (camRay, out floorHit, camRayLength, floorMask))
//			{
//				// Create a vector from the player to the point on the floor the raycast from the mouse hit.
//				Vector3 playerToMouse = floorHit.point - transform.position;
//
//				// Ensure the vector is entirely along the floor plane.
//				playerToMouse.y = 0f;
//
//				// Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
//				Quaternion newRotation = Quaternion.LookRotation (playerToMouse);
//
//				// Set the player's rotation to this new rotation.
//				playerRigidbody.MoveRotation (newRotation);
//			}
	}

//		void Animating (float h, float v)
//		{
//			// Create a boolean that is true if either of the input axes is non-zero.
//			bool walking = h != 0f || v != 0f;
//
//			// Tell the animator whether or not the player is walking.
//			anim.SetBool ("IsWalking", walking);
//		}


}
