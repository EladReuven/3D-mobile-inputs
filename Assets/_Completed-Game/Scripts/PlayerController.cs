﻿using UnityEngine;

// Include the namespace required to use Unity UI
using UnityEngine.UI;

using System.Collections;

public class PlayerController : MonoBehaviour {

	//Elad Added
	Gyroscope myGyro;
	float moveHor;
	float moveVer;
	Vector3 offset;
	bool firstAngleFound = false;
	public Button startButton;
	bool started = false;


	// Create public variables for player speed, and for the Text UI game objects
	public float speed;
	public Text countText;
	public Text winText;

	// Create private references to the rigidbody component on the player, and the count of pick up objects picked up so far
	private Rigidbody rb;
	private int count;

	// At the start of the game..
	void Start ()
	{
		myGyro = Input.gyro;
		myGyro.enabled = true;
		Screen.orientation = ScreenOrientation.LandscapeLeft;
		// Assign the Rigidbody component to our private rb variable
		rb = GetComponent<Rigidbody>();

		// Set the count to zero 
		count = 0;

		// Run the SetCountText function to update the UI (see below)
		SetCountText ();

		// Set the text property of our Win Text UI to an empty string, making the 'You Win' (game over message) blank
		winText.text = "";
		startButton.gameObject.SetActive(true);
	}

	void FixedUpdate ()
    {

		if (started)
        {
			Move();
        }
        else
        {
			rb.velocity = Vector3.zero;
		}
	}


    // When this game object intersects a collider with 'is trigger' checked, 
    // store a reference to that collider in a variable named 'other'..
    void OnTriggerEnter(Collider other) 
	{
		// ..and if the game object we intersect has the tag 'Pick Up' assigned to it..
		if (other.gameObject.CompareTag ("Pick Up"))
		{
			// Make the other game object (the pick up) inactive, to make it disappear
			other.gameObject.SetActive (false);

			// Add one to the score variable 'count'
			count = count + 1;

			// Run the 'SetCountText()' function (see below)
			SetCountText ();
		}
	}

	// Create a standalone function that can update the 'countText' UI and check if the required amount to win has been achieved
	void SetCountText()
	{
		// Update the text field of our 'countText' variable
		countText.text = "Count: " + count.ToString ();

		// Check if our 'count' is equal to or exceeded 12
		if (count >= 12) 
		{
			// Set the text value of our 'winText'
			winText.text = "You Win!";
		}
	}
    private void Move()
    {
        moveHor = Input.acceleration.x;
        moveVer = Input.acceleration.y;

        Vector3 movement = new Vector3(moveHor, 0, moveVer);

        if (!firstAngleFound && movement != Vector3.zero)
        {
            Debug.Log("Angle Found");
            firstAngleFound = true;
            offset = movement;
			rb.velocity = Vector3.zero;
		}

		Vector3 offsetMovement = movement - offset;

        rb.AddForce(offsetMovement * speed * Time.deltaTime, ForceMode.VelocityChange);
    }

	public void StartGame()
    {
		started = true;
		startButton.gameObject.SetActive(false);

	}
}