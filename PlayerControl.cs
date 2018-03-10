﻿using UnityEngine;
public class PlayerControl : MonoBehaviour {

    Transform player;
    float speed;
    Vector3 destination;    //the next destination
    //the individual colliders for the player 

    public GameObject Maze;
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
    Vector3 currentPossition;
    float minMag = 1.0f;

    enum direction  //for the direcitons 
    {
        None,
        Right,
        Left,
        Forward,
        Back
    };

    direction currentDireciton;
    direction directionFlag;

    bool inJunction;

    int flag;
    int Flag = 1;
    void Start () {
        flag = 0;   // gets triggered at the edges when the maze is to be turned 
        speed = 0.1f;
        mazeRotate = Maze.GetComponent<MazeRotation>();
        groundray = GroundRay.GetComponent<GroundRayCast>();
        rightTrig = rightCollider.GetComponent<triggering>();
        leftTrig = leftCollider.GetComponent<triggering>();
        forwardTrig = forwardCollider.GetComponent<triggering>();
        backTrig = backCollider.GetComponent<triggering>();

        currentDireciton = direction.None;
        player = GetComponent<Transform>();
        currentPossition = player.localPosition;
        pastPosition = currentPossition;
        destination = player.localPosition;
        inJunction = true;
	}
	
	void FixedUpdate () {
        if(Flag == 1)
        {
            currentPossition = player.localPosition;
            Debug.Log(currentPossition);
            TrigCol();
            Debug.Log("in Junction : " + inJunction);
            if(!groundray.onGround && flag == 0)
            {
                changePlane();
            }
            else if(!inJunction)
            {
                
                Move();
                
            }
            else
            {
                player.localPosition = destination;
                directionFlag = direction.None;
                flag = 0;
                changeDirection();
            } 
        }
	}
    void TrigCol()  //manupulating the data of the triggered colliders RLFB
    {
        Vector2 travelled = new Vector2((currentPossition.x - pastPosition.x), (currentPossition.y - pastPosition.y));
        float mag = travelled.magnitude;
        if (rightTrig.trig)
        {
            if(currentDireciton == direction.Right)
            {
                inJunction = true;
            }
        }
        else
        {
            if (mag> minMag)
            {
                if (currentDireciton == direction.Forward || currentDireciton == direction.Back)
                {
                    inJunction = true;
                }
            }
        }
        if (leftTrig.trig)
        {
            
            if (currentDireciton == direction.Left)
            {
                inJunction = true;
            }
        }
        else
        {
            if (mag > minMag)
            {
                if (currentDireciton == direction.Forward || currentDireciton == direction.Back)
                {
                    inJunction = true;
                }
            }
        }
        if (forwardTrig.trig)
        {
            
            if (currentDireciton == direction.Forward)
            {
                inJunction = true;
            }
        }
        else
        {
            if (mag > minMag)
            {
                if (currentDireciton == direction.Right || currentDireciton == direction.Left)
                {
                    inJunction = true;
                }
            }
        }
        if (backTrig.trig)
        {
            if (currentDireciton == direction.Back)
            {
                inJunction = true;
            }
        }
        else
        {
            if (mag > minMag)
            {
                if (currentDireciton == direction.Right || currentDireciton == direction.Left)
                {
                    inJunction = true;
                }
            }
        }
    }
    void Move() //this funtion will controll the movement on the maze plane 
    {
        //code to move a single step on the plane 
        switch(currentDireciton)
        {
            case direction.Right:
                if(directionFlag != direction.Right)
                {
                    destination = player.localPosition + Vector3.right*2;
                    directionFlag = direction.Right;
                }
                break;
            case direction.Left :
                if( directionFlag != direction.Left)
                {
                    destination = player.localPosition + Vector3.left*2;
                    directionFlag = direction.Left;
                }
                break;
            case direction.Forward:
                if(directionFlag != direction.Forward)
                {
                    destination = player.localPosition + Vector3.forward*2;
                    directionFlag = direction.Forward;
                }
                break;
            case direction.Back:
                if( directionFlag != direction.Back)
                {
                    destination = player.localPosition + Vector3.back*2;
                    directionFlag = direction.Back;
                }
                break;
        }
         Debug.Log("currentPos: " + player.localPosition + "destination : " + destination);
        player.localPosition = Vector3.Lerp(player.localPosition, destination, 0.1f);
    }
    void changeDirection()  //used to change the direciton of the player
    {
       // Debug.Log("changing direciton");
        if(Input.GetAxis("Horizontal") > 0)
        {
            if (!rightTrig.trig)
            {
                inJunction = false;
                pastPosition = currentPossition;
                currentDireciton = direction.Right;
            }
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            if (!leftTrig.trig)
            {
                inJunction = false;
                pastPosition = currentPossition;
                currentDireciton = direction.Left;
            }
        }
        else if (Input.GetAxis("Vertical") > 0)
        {
            if (!forwardTrig.trig)
            {
                inJunction = false;
                pastPosition = currentPossition;
                currentDireciton = direction.Forward;
            }
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            if (!backTrig.trig)
            {
                inJunction = false;
                pastPosition = currentPossition;
                currentDireciton = direction.Back;
            }
        }
    }
    void changePlane()  //for changing the current face of the maze 
    {
        if(currentDireciton == direction.Right)
        {
            flag = 1;
            mazeRotate.rotateDirection = (int)direction.Right;
            transform.Rotate(90, 0, 0);
            mazeRotate.rotate = true;
        }
        if (currentDireciton == direction.Left)
        {
            flag = 1;
            mazeRotate.rotateDirection = (int)direction.Left;
            transform.Rotate(-90, 0, 0);
            mazeRotate.rotate = true;
        }
        if (currentDireciton == direction.Forward)
        {
            flag = 1;
            mazeRotate.rotateDirection = (int)direction.Forward;
            transform.Rotate(0, 0, 90);
            mazeRotate.rotate = true;
        }
        if (currentDireciton == direction.Back)
        {
            flag = 1;
            mazeRotate.rotateDirection = (int)direction.Back;
            transform.Rotate(0, 0, -90);
            mazeRotate.rotate = true;
        }
    }
}
