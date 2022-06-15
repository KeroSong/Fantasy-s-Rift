using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SDD.Events;

public class PlayerAnimation : MonoBehaviour
{
    /*public CharacterController controller;*/
    public Transform cam;

    public float speed = 6f;
    public float sprintSpeed = 10f;
    public float turnSmoothTime = 0.1f;
    public float gravity = -9.81f;
    float turnSmoothVelocity;

    public Transform groudCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    Animator playerAnimator;

    private void Awake()
    {
        playerAnimator = GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groudCheck.position,groundDistance,groundMask);
        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if(direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg +cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f,angle,0f);

            if (Input.GetKey(KeyCode.LeftShift))
            {
                playerAnimator.SetFloat("Sprint", direction.magnitude);
            }
            else
            {
                playerAnimator.SetFloat("Sprint", 0);
            }

            playerAnimator.SetBool("Run", true);

        }
        else
        {
            playerAnimator.SetBool("Run",false);
        }

        /*velocity.y +=  gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);*/

    }
}
