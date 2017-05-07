using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    private int j = 0;
    private List<Vector3> Positions;
    private List<Quaternion> Rotations;
    private bool Restart = false;
    GameObject player;
    // Use this for initialization
    void Start()
    {
        GameObject player = GameObject.Find("Player");
        Positions = player.GetComponent<FirstPersonController>().Positions;
        Rotations = player.GetComponent<FirstPersonController>().Rotations;
        //GameObject player = GameObject.Find("Player");
        //ArrayList Positions = player.GetComponent<FirstPersonController>().Positions;
    }

    // Update is called once per frame
    void Update()
    {
        if (Restart){
            Positions = player.GetComponent<FirstPersonController>().Positions;
            Rotations = player.GetComponent<FirstPersonController>().Rotations;
        }
        transform.position = Positions[j];//new Vector3(Positions[j], Positions[j+1], Positions[j+2]);
        transform.rotation = Rotations[j];
        j += 1;
    }
}
