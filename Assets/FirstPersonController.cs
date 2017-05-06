using UnityEngine;
using System.Collections;

public class FirstPersonController : MonoBehaviour
{
    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    public float mousesensitivity = 5.0F;
    public float updownrange = 60.0f;
    float vertrot = 0.0F;
    private Vector3 moveDirection = Vector3.zero;

    private void Start()
    {
        Screen.lockCursor = true;
    } 

    void Update()
    {
        float leftrightrot = Input.GetAxis("Mouse X") * mousesensitivity;
        transform.Rotate(0, leftrightrot, 0);

        //rotate Up and down the camera based on mouse position
        vertrot -= Input.GetAxis("Mouse Y") * mousesensitivity; //minus because updown rot is backwards
        vertrot = Mathf.Clamp(vertrot, -updownrange, updownrange);
        Camera.main.transform.rotation = Quaternion.Euler(vertrot, transform.rotation.eulerAngles.y, 0);
        CharacterController controller = GetComponent<CharacterController>();
        if (controller.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection *= speed;
            if (Input.GetButton("Jump"))
                moveDirection.y = jumpSpeed;
        }
        moveDirection.y -= gravity * Time.deltaTime;
        moveDirection = transform.TransformDirection(moveDirection);
        controller.Move(moveDirection * Time.deltaTime);
    }
}
