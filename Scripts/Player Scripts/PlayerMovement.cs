using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 3.5f;

    [SerializeField]
    private float minBound_X = -71f, maxBound_X = 71f, minBound_Y = -3.1f, maxBound_Y = -1f;

    private Vector3 tempPos;

    private float xAxis, yAxis;

    // Calling Player Animation  script func

    private PlayerAnimation playerAnimation;

    [SerializeField]
    private float shootWaitTime = 0.5f;

    private float waitBeforeShooting;

    [SerializeField]
    private float moveWaitTime = 0.3f;

    private float waitBeforeMoving;

    private bool   canMove  = true;

    //Calling Player Shooting Manager Script func

    private PlayerShootingManager playerShootingManager;

    // Player Died

    private bool playerDied;

    public Joystick joystick;

    private void Awake()
    {
        playerAnimation = GetComponent<PlayerAnimation>();

        playerShootingManager = GetComponent<PlayerShootingManager>();
    }


    // Update is called once per frame
    void Update()
    {
        if(playerDied)
            return;
        
        HandleMovement();
        HandleAnimation();
        HandleFacingDirection();

        HandleShooting();
        CheckIfCanMove();
    }

    void HandleMovement()
    {
        // xAxis = Input.GetAxisRaw("Horizontal");
        // yAxis = Input.GetAxisRaw("Vertical");
        // Joystick 
        xAxis = joystick.Horizontal;
        yAxis = joystick.Vertical;

        if (!canMove)
            return;


        tempPos = transform.position;

        tempPos.x += xAxis * moveSpeed * Time.deltaTime;
        tempPos.y += yAxis * moveSpeed * Time.deltaTime;

        if (tempPos.x < minBound_X)
            tempPos.x = minBound_X;

        if (tempPos.x > maxBound_X)
            tempPos.x = maxBound_X;

        if (tempPos.y < minBound_Y)
            tempPos.y = minBound_Y;

        if (tempPos.y > maxBound_Y)
            tempPos.y = maxBound_Y;

        transform.position = tempPos;
    }

    void HandleAnimation()
    {
        if (!canMove)
            return;

        
        if(Mathf.Abs(xAxis) > 0 || Mathf.Abs(yAxis) > 0)
            playerAnimation.PlayAnimation("Walk");
        else
            playerAnimation.PlayAnimation("Idle");

    }

    void HandleFacingDirection()
    {
        if (xAxis > 0)
            playerAnimation.SetFacingDirection(true);
        else if (xAxis < 0)
            playerAnimation.SetFacingDirection(false);
    }

    void StopMovement()
    {
        canMove = false;
        waitBeforeMoving = Time.time + moveWaitTime;
    }

    void Shoot()
    {
        waitBeforeMoving = Time.time + shootWaitTime;
        StopMovement();
        playerAnimation.PlayAnimation("Shoot");

        playerShootingManager.Shoot(transform.localScale.x);
    }

    void CheckIfCanMove()
    {
        if (Time.time > waitBeforeMoving)
            canMove = true;
    }

    void HandleShooting()
    {
        if(Input.GetKeyDown(KeyCode.K) || Input.GetButtonDown("Fire1"))
        { 
            if (Time.time > waitBeforeMoving)
                Shoot();
        }
    }

    public void  PlayerDied() 
    {
        playerDied = true;
        playerAnimation.PlayAnimation("Player");
        Invoke("DestroyPlayerAfterDelay", 1f);
    }

    void DestroyPlayerAfterDelay()
    {
        Destroy(gameObject);
    }
}
