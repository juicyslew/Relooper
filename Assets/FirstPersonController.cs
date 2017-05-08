using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FirstPersonController : MonoBehaviour
{
    public float maxhealth = 100.0f;
    public float health = 100.0f;
    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    public float mousesensitivity = 5.0F;
    public float updownrange = 60.0f;
    public float bulletSpeed = 70.0f;
    public GameObject BulletPrefab;
    public Transform BulletSpawn;
    public float RoundInterval = 20.0f;
    public int GhostNum = 5;
    public float fireinterval = 20.0f;
    private int ghostCounter = 0;
    public float maxspray = 6.0f;
    public float minspray = 0.0f;
    public float sprayinterval = 1.0f;
    public float spraydecrease = .1f;
    private float Spray = 0.0f;
    private float negligableRot = .001f;
    private float RoundTime;
    public Vector3 SpawnLocation = new Vector3(4, 0, 0);
    public GameObject Ghost;
    private List<GameObject> ghosts = new List<GameObject>();
    float vertrot = 0.0F;
    private Vector3 moveDirection = Vector3.zero;
    private bool noGhost = false;
    private bool die = false;
    public GameObject DeadPlayerPrefab;
    private GameObject deadplay;
    private Renderer rend;

    //CharacterController controller;
    public List<Vector3> Positions = new List<Vector3>();
    public List<Quaternion> Rotations = new List<Quaternion>();
    public List<bool> Shots = new List<bool>();
    public List<Quaternion> ShotDir = new List<Quaternion>();
    public int cd = 0;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        health = maxhealth;
        RoundTime = RoundInterval;
        Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }

    void Update()
    {
        CharacterController controller = GetComponent<CharacterController>();
        Positions.Add(transform.position);
        Rotations.Add(Camera.main.transform.rotation);
        Shots.Add(false);
        //Positions.Add(transform.position.y);
        //Positions.Add(transform.position.z);
        float leftrightrot = Input.GetAxis("Mouse X") * mousesensitivity;
        transform.Rotate(0, leftrightrot, 0);

        //rotate Up and down the camera based on mouse position
        vertrot -= Input.GetAxis("Mouse Y") * mousesensitivity; //minus because updown rot is backwards
        vertrot = Mathf.Clamp(vertrot, -updownrange, updownrange);
        Camera.main.transform.rotation = Quaternion.Euler(vertrot, transform.rotation.eulerAngles.y, 0);
        if (controller.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            if (Input.GetButton("Jump"))
                moveDirection.y = jumpSpeed;
        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
        if (Time.time > RoundTime && noGhost == false)
        {
            Restart();
        }
        cd -= 1;
        if (Input.GetMouseButton(0) && cd <= 0)
        {
            Fire();
            cd = (int)fireinterval;
        }
        Spray -= spraydecrease;//(Spray-minspray)*spraydecrease + minspray;
        if (Spray < minspray+negligableRot){
            Spray = minspray;
        }
        if (health < 0 && die == false)
        {
            Die();
        }
        //Debug.Log(Spray);
    }

    void Restart()
    {
        die = false;
        RoundTime += RoundInterval;
        health = maxhealth;
        ghostCounter += 1;
        foreach (GameObject g in ghosts)
        {
            g.GetComponentInChildren<GhostController>().Restart();
        }
        GameObject newghost = Instantiate(Ghost, Vector3.zero, Quaternion.identity);
        ghosts.Add(newghost);
        transform.position = SpawnLocation;
        transform.rotation = Quaternion.identity;
        if (ghostCounter > GhostNum)
        {
            noGhost = true;
        }
        if (deadplay)
        {
            Destroy(deadplay);
        }
    }
    void Die()
    {
        GameObject deadplay = Instantiate(DeadPlayerPrefab, transform.position, transform.rotation);
        die = true;
    }

    void Fire()
    {
        if (Shots.Count > 0)
        {
            Shots[Shots.Count - 1] = true;

            // Create the Bullet from the Bullet Prefab
            var bullet = Instantiate(BulletPrefab, BulletSpawn.position, BulletSpawn.rotation);

            // Set Bullet Owner
            bullet.GetComponent<BulletScript>().owner = gameObject;

            var randomNumberX = Random.Range(-Spray, Spray);
            var randomNumberY = Random.Range(-Spray, Spray);
            var randomNumberZ = Random.Range(-Spray, Spray);

            bullet.transform.Rotate(randomNumberX, randomNumberY, randomNumberZ);

            // Add velocity to the Bullet
            ShotDir.Add(bullet.transform.rotation);
            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeed;

            // Destroy the Bullet after 2 seconds
            Destroy(bullet, 1f);
            Spray += sprayinterval;
            if (Spray > maxspray){
                Spray = maxspray;
            }
        }
    }
}