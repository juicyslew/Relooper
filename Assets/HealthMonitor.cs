using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthMonitor : MonoBehaviour {
    [HideInInspector]
    public float health;
    private void Start()
    {
        
    }
    void Update()
    {
        Debug.Log(health);

    }
}
