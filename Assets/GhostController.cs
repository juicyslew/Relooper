using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    private int j = 0;
    private List<Vector3> Positions;
    private List<Quaternion> Rotations;
    private List<bool> Shots;
    private bool Restart = false;
    GameObject player;
    public GameObject BulletPrefab;
    public Transform BulletSpawn;
    public float bulletSpeed = 70.0f;
    Transform partrans;
    // Use this for initialization
    void Start()
    {
        partrans = gameObject.transform.parent.transform;
        GameObject player = GameObject.Find("Player");
        Positions = player.GetComponent<FirstPersonController>().Positions;
        Rotations = player.GetComponent<FirstPersonController>().Rotations;
        Shots = player.GetComponent<FirstPersonController>().Shots;
        //GameObject player = GameObject.Find("Player");
        //ArrayList Positions = player.GetComponent<FirstPersonController>().Positions;
    }

    // Update is called once per frame
    void Update()
    {
        if (Restart){
            Positions = player.GetComponent<FirstPersonController>().Positions;
            Rotations = player.GetComponent<FirstPersonController>().Rotations;
            Shots = player.GetComponent<FirstPersonController>().Shots;
        }
        partrans.position = Positions[j];//new Vector3(Positions[j], Positions[j+1], Positions[j+2]);
        partrans.rotation = Rotations[j];
        if (Shots[j])
        {
            Fire();
        }
        j += 1;
    }

    void Fire()
    {
        // Create the Bullet from the Bullet Prefab
        var bullet = Instantiate(BulletPrefab, BulletSpawn.position, BulletSpawn.rotation);

        // Set Bullet Owner
        bullet.GetComponent<BulletScript>().owner = gameObject;

        // Add velocity to the Bullet
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeed;

        // Destroy the Bullet after 2 seconds
        Destroy(bullet, 1.5f);
    }
}
