using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthMonitor : MonoBehaviour {
    public float maxhealth = 100.0f;
    public float health = 100.0f;
    private void Start()
    {
        health = maxhealth;
    }
    void Update()
    {
        
    }
}
