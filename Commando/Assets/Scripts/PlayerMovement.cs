using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller2D;

    public float runSpeed = 40f;
    float horizontalMove = 0f;
    bool isJumping = false;
    bool isCrouching = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        //TODO add autojump variable, just for sake of reusing thnis code
        if (Input.GetButton("Jump"))
        {
            isJumping = true;
        }

        if (Input.GetButtonDown("Crouch"))
        {
            isCrouching = true;
        } else if (Input.GetButtonUp("Crouch"))
        {
            isCrouching = false;
        }



    }

    // Update is called once per frame
    void FixedUpdate()
    {

        controller2D.Move(horizontalMove * Time.fixedDeltaTime, isCrouching, isJumping);
        isJumping = false;
     


    }
}
