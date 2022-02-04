using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance = null;

    private bool turnLeft, turnRight, jump, isMoving;
    public float speed = 7.0f;
    private CharacterController controller;

    private float verticalVelocity = 0.0f;
    private float gravity = 12.0f;

    public float laneOffset = 1.5f;
    public Vector3 moveVector;
    private float lane1;
    private float lane2 = 0.0f;
    private float lane3;
    public float moveDistance;
    public float switchToLane;
    public float currentLane;

    private bool hasJumped = false;
    public float jumpHeight = 1.0f;

    internal Animator playerAnimator = null;
    private float animationSpeed = 1.0f;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        controller = GetComponent<CharacterController>();
        playerAnimator = GetComponent<Animator>();

        lane1 = -laneOffset;
        lane3 = laneOffset;
        switchToLane = lane2;
        currentLane = lane2;
        jump = false;
        isMoving = false;

        animationSpeed = 1.0f;
    }

    void Update()
    {
        if (GameController.instance.gameStarted == true)
        {
            playerAnimator.speed = animationSpeed;

            moveVector = Vector3.zero;

            if (controller.isGrounded)
            {
                animationSpeed = 1.0f;
                verticalVelocity = -1.0f;
                hasJumped = false;
            }
            else
            {
                verticalVelocity -= gravity * Time.deltaTime;
                animationSpeed = 0.1f;
            }

            if (jump && !hasJumped)
            {
                jump = false;
                hasJumped = true;
                verticalVelocity = +jumpHeight;
            }

            if (verticalVelocity > 0 + jumpHeight)
            {
                hasJumped = false;
            }

            //X
            float horizontalMovement = Input.GetAxisRaw("Horizontal");

            if (horizontalMovement < 0.0f)
            {
                MoveLeft();
            }
            else if (horizontalMovement > 0.0f)
            {
                MoveRight();
            }

            if (!hasJumped)
            {
                if (currentLane > switchToLane)
                {
                    if (gameObject.transform.position.x > switchToLane)
                    {
                        moveVector.x = moveDistance * speed;
                    }
                    else
                    {
                        currentLane = switchToLane;
                    }
                }
                else if (currentLane < switchToLane)
                {
                    if (gameObject.transform.position.x < switchToLane)
                    {
                        moveVector.x = moveDistance * speed;
                    }
                    else
                    {
                        currentLane = switchToLane;
                    }
                }
            }

            //Y
            moveVector.y = verticalVelocity;

            //Z
            moveVector.z = speed;

            controller.Move(moveVector * Time.deltaTime);
        } else
        {
            playerAnimator.CrossFade("Idle-GO", 0);
        }

    }

    public void MoveLeft()
    {
        if (hasJumped)
        {
            return;
        }

        isMoving = true;

        //Debug.Log("Moving Left");
        if (currentLane == lane1)
        {
            return;
        }

        if (currentLane == lane3)
        {
            switchToLane = lane2;
        } 
        else
        {
            switchToLane = lane1;
        }

        moveDistance = switchToLane - currentLane;
    }

    public void MoveRight()
    {
        if (hasJumped)
        {
            return;
        }

        isMoving = true;


        //Debug.Log("Moving Right");

        if (currentLane == lane3)
        {
            return;
        }

        if (currentLane == lane1)
        {
            switchToLane = lane2;
        }
        else
        {
            switchToLane = lane3;
        }

        moveDistance = switchToLane - currentLane;
    }

    public void JumpAction()
    {
        if (controller.isGrounded == false)
        {
            return;
        }

        jump = true;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "obstacle" || other.tag == "Obstacle" || other.tag == "Untagged")
        {
            GameController.instance.lives--;
            Debug.Log("OBSTACLE HIT. " + GameController.instance.lives + " left.");

        }
    }
}

