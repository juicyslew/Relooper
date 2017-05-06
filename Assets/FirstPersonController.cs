using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour {

    public float movementspeed = 5.0f;
    public float mousesensitivity = 5.0f;

    float vertrot = 0;
    public float updownrange = 60.0f;

	// Use this for initialization
	void Start () {
        Screen.lockCursor = true; // lock mouse to the screen (removes mouse from view and keeps it in screen center)
	}
	
	// Update is called once per frame
	void Update () {
        // Rotate Left and Right based on mouse position
        float leftrightrot = Input.GetAxis("Mouse X") * mousesensitivity;
        transform.Rotate(0, leftrightrot, 0);

        //rotate Up and down the camera based on mouse position
        vertrot -= Input.GetAxis("Mouse Y") * mousesensitivity; //minus because updown rot is backwards
        vertrot = Mathf.Clamp(vertrot, -updownrange, updownrange);
        Camera.main.transform.rotation = Quaternion.Euler(vertrot, transform.rotation.eulerAngles.y, 0);

        // Move Based On WASD or ARROWKEYS
        float sidespeed = Input.GetAxis("Horizontal") * movementspeed;
        float forwardspeed = Input.GetAxis("Vertical") * movementspeed;
        Vector3 velocity = new Vector3(sidespeed, 0, forwardspeed);

        velocity = transform.rotation * velocity;
        //Get Character Controller and execute Simple Movement Script
        CharacterController cc = GetComponent<CharacterController>();
        cc.Move(velocity*Time.deltaTime);
	}
}
