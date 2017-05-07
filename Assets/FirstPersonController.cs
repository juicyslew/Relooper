using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FirstPersonController : MonoBehaviour
{
    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    public float mousesensitivity = 5.0F;
    public float updownrange = 60.0f;
    public float bulletSpeed = 50.0f;
    public GameObject BulletPrefab;
    public Transform BulletSpawn;
    public float RoundInterval = 20.0f;
    public int GhostNum = 5;
    private int ghostCounter = 0;
    private float RoundTime;
    public Vector3 SpawnLocation = new Vector3(4, 0, 0);
    public Transform Ghost;
    float vertrot = 0.0F;
    private Vector3 moveDirection = Vector3.zero;
    private bool noGhost = false;

    //CharacterController controller;
    public List<Vector3> Positions = new List<Vector3>();
    public List<Quaternion> Rotations = new List<Quaternion>();
    public int cd = 0;

    private void Start()
    {
        RoundTime = RoundInterval;
        Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }

    void Update()
    {
        CharacterController controller = GetComponent<CharacterController>();
        Positions.Add(transform.position);
        Rotations.Add(transform.rotation);
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
            ghostCounter += 1;
            Instantiate(Ghost, Vector3.zero, Quaternion.identity);
            transform.position = SpawnLocation;
            transform.rotation = Quaternion.identity;
            RoundTime += RoundInterval;
            if (ghostCounter > GhostNum)
            {
                noGhost = true;
            }
        }
        cd -= 1;
        if (Input.GetMouseButton(0) && cd <= 0)
        {
            Fire();
            cd = 20;
        }
    }

    void Fire()
    {
        // Create the Bullet from the Bullet Prefab
        var bullet = Instantiate(BulletPrefab, BulletSpawn.position, BulletSpawn.rotation);

        // Add velocity to the Bullet
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward*bulletSpeed;

        // Destroy the Bullet after 2 seconds
        Destroy(bullet, 1.5f);
    }
}