using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour {

    public float movementspeed = 5.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float forwardspeed = Input.GetAxis("Vertical")*movementspeed;
        Vector3 velocity = new Vector3(0, 0, forwardspeed);
        CharacterController cc = GetComponent<CharacterController>();
        cc.SimpleMove(velocity);
	}
}
