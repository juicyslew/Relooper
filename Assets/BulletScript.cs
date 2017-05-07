﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {
    public GameObject owner;
    public float bulletDamage;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        GameObject collideEntity = collision.gameObject;
        if (collideEntity != owner)//check that bullet isnt colliding with owner
        {
            if (collideEntity.tag == "Character")
            {
                collideEntity.GetComponent<HealthMonitor>().health -= bulletDamage;
            }
            Destroy(gameObject);
        }
    }
}