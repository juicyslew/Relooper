using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    public float maxhealth = 100.0f;
    public float health = 100.0f;
    private bool alive = true;
    private int j = 0;
    private int k = 0;
    private List<Vector3> Positions;
    private List<Quaternion> Rotations;
    private List<bool> Shots;
    private List<Quaternion> ShotDir;
    
    GameObject player;
    public GameObject BulletPrefab;
    public Transform BulletSpawn;
    public float bulletSpeed = 70.0f;
    public Material[] materials;
    private Renderer rend;
    private GameObject deadplay;
    private bool die = false;
    public bool restart = false;
    Transform partrans;
    public GameObject DeadPlayerPrefab;
    // Use this for initialization
    void Start()
    {
        health = maxhealth;
        rend = GetComponent<Renderer>();
        partrans = gameObject.transform.parent.transform;
        GameObject player = GameObject.Find("Player");
        Positions = player.GetComponent<FirstPersonController>().Positions;
        Rotations = player.GetComponent<FirstPersonController>().Rotations;
        Shots = player.GetComponent<FirstPersonController>().Shots;
        ShotDir = player.GetComponent<FirstPersonController>().ShotDir;
        //GameObject player = GameObject.Find("Player");
        //ArrayList Positions = player.GetComponent<FirstPersonController>().Positions;
    }

    public void Restart()
    {   
        health = maxhealth;
        rend.sharedMaterial = materials[0];

        if (die)
        {
            Destroy(deadplay);
        }
        
        restart = false;
        die = false;
    }
    
    void Die()
    {
        deadplay = Instantiate(DeadPlayerPrefab, transform.position, transform.rotation);
        rend.sharedMaterial = materials[1];
        die = true;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Restart){
            Positions = player.GetComponent<FirstPersonController>().Positions;
            Rotations = player.GetComponent<FirstPersonController>().Rotations;
            Shots = player.GetComponent<FirstPersonController>().Shots;
            ShotDir = player.GetComponent<FirstPersonController>().ShotDir;
        }*/
        if (restart)
        {
            Restart();
        }
        partrans.position = Positions[j];//new Vector3(Positions[j], Positions[j+1], Positions[j+2]);
        partrans.rotation = Rotations[j];
        if (Shots[j])
        {
            Fire();
        }
        j += 1;
        if (health < 0 && die == false)
        {
            Die();
        }
    }

    void Fire()
    {
        // Create the Bullet from the Bullet Prefab
        var bullet = Instantiate(BulletPrefab, BulletSpawn.position, BulletSpawn.rotation);

        // Set Bullet Owner
        bullet.GetComponent<BulletScript>().owner = gameObject;

        bullet.transform.rotation = ShotDir[k];

        // Add velocity to the Bullet
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeed;

        // Destroy the Bullet after 2 seconds
        Destroy(bullet, 1.5f);
        k += 1;
    }
}
