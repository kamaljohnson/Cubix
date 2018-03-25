﻿using UnityEngine;
public class PlayerControl : MonoBehaviour {

    Transform player;
    float speed;
    Vector3 destination;    //the next destination
    //the individual colliders for the player 

    float BoundaryLimits;
    public GameObject Maze;
    public GameObject mazeBody;
    private MazeRotation mazeRotate;

    public GameObject rightCollider;
    public GameObject leftCollider;
    public GameObject forwardCollider;
    public GameObject backCollider;

    public GameObject GroundRay;
    private GroundRayCast groundray;

    private triggering rightTrig;
    private triggering leftTrig;
    private triggering forwardTrig;
    private triggering backTrig;
    Vector3 pastPosition;
    Vector3 currentPosition;
    float minMag = 1.9999f;
    float mag;
    enum direction  //for the direcitons 
    {
        None,
        Right,
        Left,
        Forward,
        Back
    };

    direction currentDirection;
    direction directionFlag;
    Transform MazeLimits;
    bool inJunction;
    bool Moving;
    int flag;
    int FLAG = 1;
    Vector3 directionVector;
    
    Vector3 localForward;
    Vector3 localBack;
    Vector3 localRight;
    Vector3 localLeft;
    Vector3 localDown;
    void Start () {

        flag = 0;   // gets triggered at the edges when the maze is to be turned 
        speed = 0.1f;
        mazeRotate = Maze.GetComponent<MazeRotation>();
        groundray = GroundRay.GetComponent<GroundRayCast>();
        rightTrig = rightCollider.GetComponent<triggering>();
        leftTrig = leftCollider.GetComponent<triggering>();
        forwardTrig = forwardCollider.GetComponent<triggering>();
        backTrig = backCollider.GetComponent<triggering>();

        currentDirection = direction.None;
        player = GetComponent<Transform>();
        currentPosition = player.localPosition;
        pastPosition = currentPosition;
        destination = player.localPosition;
        inJunction = true;
        Moving = false;
        BoundaryLimits = mazeBody.transform.localScale.x + transform.localScale.x/2;
	}
	
	void FixedUpdate () 
    {
        //to getting the local vectors in the respected directions 
        localForward = transform.parent.InverseTransformDirection(transform.forward);
        localRight = transform.parent.InverseTransformDirection(transform.right);
        localLeft = localRight*-1;
        localBack = localForward*-1;
        localDown = transform.parent.InverseTransformDirection(transform.up) * -1;
        
        Debug.Log("Junction : " + inJunction);
        Debug.Log("Moving : " + Moving);
        Debug.Log("Direction : " + currentDirection);
        currentPosition = player.localPosition;
        if(flag == 0)
        {
            TrigCol();
        }
        if(Mathf.Abs(currentPosition.x) > BoundaryLimits || Mathf.Abs(currentPosition.y) > BoundaryLimits|| Mathf.Abs(currentPosition.z) > BoundaryLimits && flag == 0)
        {
            if(player.localPosition.x > 4.5f)
            {
                player.localPosition = new Vector3(4.5f, player.localPosition.y, player.localPosition.z);
            }
            else if(player.localPosition.x < -4.5f)
            {
                player.localPosition = new Vector3(-4.5f, player.localPosition.y, player.localPosition.z);

            }
            else if(player.localPosition.y > 4.5f)
            {
                player.localPosition = new Vector3(player.localPosition.x, 4.5f, player.localPosition.z);
            }
            else if(player.localPosition.y < -4.5f)
            {
                player.localPosition = new Vector3(player.localPosition.x, -4.5f, player.localPosition.z);
            }
            else if(player.localPosition.z > 4.5f)
            {
                player.localPosition = new Vector3(player.localPosition.x, player.localPosition.y, 4.5f);
            }
            else if(player.localPosition.z < -4.5f)
            {
                player.localPosition = new Vector3(player.localPosition.x, player.localPosition.y, -4.5f);
            }
            changePlane();
        }
        else if(!inJunction)
        {

            if(player.localPosition == destination && flag == 0)
            {
                directionFlag = direction.None;
            }
            else if(flag == 1)
            {
                currentDirection = directionFlag;
                directionFlag = direction.None;
                flag = 0;
            }
            Move();
        }
        else if(inJunction)
        {
            Moving = false;
            player.localPosition = destination;
            directionFlag = direction.None;
            changeDirection();
        }
	}

    void TrigCol()  //manupulating the data of the triggered colliders RLFB
    {
        Vector2 travelled = new Vector2((currentPosition.x - pastPosition.x), (currentPosition.y - pastPosition.y));
        float mag = travelled.magnitude;
        if (rightTrig.trig)
        {
            if(currentDirection == direction.Right)
            {
                inJunction = true;
                Moving = false;
            }
        }
        else
        {
            if (mag >= minMag)
            {
                if (currentDirection == direction.Forward || currentDirection == direction.Back)
                {
                    inJunction = true;
                    Moving = false;
                }
            }
        }
        if (leftTrig.trig)
        {
            if (currentDirection == direction.Left)
            {
                inJunction = true;
                Moving = false;
            }
        }
        else
        {
            if (mag >= minMag)
            {
                if (currentDirection == direction.Forward || currentDirection == direction.Back)
                {
                    inJunction = true;
                    Moving = false;
                }
            }
        }
        if (forwardTrig.trig)
        {
            
            if (currentDirection == direction.Forward)
            {
                inJunction = true;
                Moving = false;
            }
        }
        else
        {
            if (mag >= minMag)
            {
                if (currentDirection == direction.Right || currentDirection == direction.Left)
                {
                    inJunction = true;
                    Moving = false;
                }
            }
        }
        if (backTrig.trig)
        {
            if (currentDirection == direction.Back)
            {
                inJunction = true;
                Moving = false;
            }
        }
        else
        {
            if (mag >= minMag)
            {
                if (currentDirection == direction.Right || currentDirection == direction.Left)
                {
                    inJunction = true;
                    Moving = false;
                }
            }
        }
    }
    void Move() //this funtion will controll the movement on the maze plane 
    {
        //code to move a single step on the plane 
        Debug.Log("Moving to : " + currentDirection);
        switch(currentDirection)    
        {
            case direction.Right:
                if(directionFlag != direction.Right)
                {
                    //directionVector = new Vector3(2, 0 , 0);
                    destination = player.localPosition + localRight * 2;
                    directionFlag = direction.Right;
                }
                break;
            case direction.Left :
                if( directionFlag != direction.Left)
                {
                    
                    //directionVector = new Vector3(-2, 0 , 0);
                    destination = player.localPosition + localLeft*2;
                    directionFlag = direction.Left;
                }
                break;
            case direction.Forward:
                if(directionFlag != direction.Forward)
                {
                    //directionVector = new Vector3(0, 0 , 2);
                    destination = player.localPosition + localForward;
                    directionFlag = direction.Forward;
                }
                break;
            case direction.Back:
                if( directionFlag != direction.Back)
                {
                    //directionVector = new Vector3(0, 0 , -2);
                    destination = player.localPosition + localBack*2;
                    directionFlag = direction.Back;
                }
                break;
        }
        if(player.localPosition == destination)
        {
            directionFlag = direction.None;
        }
        Moving = true;
        player.localPosition = Vector3.MoveTowards(player.localPosition, destination, 0.1f);
        Debug.Log("Destination" + destination);
    }
    void changeDirection()  //used to change the direciton of the player
    {
        if(Input.GetAxis("Horizontal") > 0 && !Moving)
        {
            if (!rightTrig.trig)
            {
                Moving = true;
                inJunction = false;
                pastPosition = currentPosition;
                currentDirection = direction.Right;
            }
        }
        else if (Input.GetAxis("Horizontal") < 0 && !Moving)
        {
            if (!leftTrig.trig)
            {
                Moving = true;
                inJunction = false;
                pastPosition = currentPosition;
                currentDirection = direction.Left;
            }
        }
        else if (Input.GetAxis("Vertical") > 0 && !Moving)
        {
            if (!forwardTrig.trig)
            {
                Moving = true;
                inJunction = false;
                pastPosition = currentPosition;
                currentDirection = direction.Forward;
            }
        }
        else if (Input.GetAxis("Vertical") < 0 && !Moving)
        {
            if (!backTrig.trig)
            {
                Moving = true;
                inJunction = false;
                pastPosition = currentPosition;
                currentDirection = direction.Back;
            }
        }
    }
    void changePlane()  //for changing the current face of the maze 
    {
        direction temp = directionFlag;
        destination = player.localPosition + localDown*1.5f;
        player.localPosition = destination;
        directionFlag = temp;
        mazeRotate.rotate = true;

        if(currentDirection == direction.Right)
        {
            mazeRotate.rotateDirection = localForward * 2;
            transform.Rotate(localForward * -90);
        }
        else if (currentDirection == direction.Left)
        {
            mazeRotate.rotateDirection = localBack * 2;
            transform.Rotate(localBack * -90);
        }
        else if (currentDirection == direction.Forward)
        {
            mazeRotate.rotateDirection = localLeft * 2;
            transform.Rotate(localLeft * -90);
        }
        else if (currentDirection == direction.Back)
        {
            mazeRotate.rotateDirection = localRight * 2;
            transform.Rotate(localRight * -90);
        }
        flag = 1;
    }
}
