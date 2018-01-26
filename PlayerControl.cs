﻿using UnityEngine;
public class PlayerControl : MonoBehaviour {

    Rigidbody player;
    float speed;
    Vector3 destination;    //the next destination
    //the individual colliders for the player 
    public GameObject rightCollider;
    public GameObject leftCollider;
    public GameObject forwardCollider;
    public GameObject backCollider;

    private triggering rightTrig;
    private triggering leftTrig;
    private triggering forwardTrig;
    private triggering backTrig;

    enum direction  //for the direcitons 
    {
        None,
        Right,
        Left,
        Forward,
        Back
    };

    direction currentDireciton;
    bool inMotion;
    bool inJunction;
    bool atBoundary;
    void Start () {

        speed = 0.01f;
        rightTrig = rightCollider.GetComponent<triggering>();
        leftTrig = leftCollider.GetComponent<triggering>();
        forwardTrig = forwardCollider.GetComponent<triggering>();
        backTrig = backCollider.GetComponent<triggering>();

        currentDireciton = direction.None;
        player = GetComponent<Rigidbody>();
        inMotion = false;
        atBoundary = false;
        inJunction = true;
	}
	
	void Update () {
        Debug.Log(currentDireciton);
        TrigCol();
        if(atBoundary)
        {
            changePlane();
        }
        else if(inJunction)
        {
            changeDirection();
        }
        else
            Move();
	}
    void TrigCol()  //manupulating the data of the triggered colliders RLFB
    {
        Debug.Log("Trig");
        if (rightTrig.trig)
        {
            if(currentDireciton == direction.Right)
            {
                inJunction = true;
            }
        }
        else
        {
            if (currentDireciton == direction.Forward || currentDireciton == direction.Back)
            {
                inJunction = true;
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
            if (currentDireciton == direction.Forward || currentDireciton == direction.Back)
            {
                inJunction = true;
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
            if(currentDireciton == direction.Right || currentDireciton == direction.Left)
            {
                inJunction = true;
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
            if (currentDireciton == direction.Right || currentDireciton == direction.Left)
            {
                inJunction = true;
            }
        }
        Debug.Log(rightTrig.trig + " right");
        Debug.Log(leftTrig.trig + " left");
        Debug.Log(forwardTrig.trig + " forward");
        Debug.Log(backTrig.trig + " back");
    }
    void Move() //this funtion will controll the movement on the maze plane 
    {
        Debug.Log("moving");
        switch(currentDireciton)
        {
            case direction.Right:
                destination = Vector3.right;
                break;
            case direction.Left:
                destination = Vector3.left;
                break;
            case direction.Forward:
                destination = Vector3.forward;
                break;
            case direction.Back:
                destination = Vector3.back;
                break;

        }
        player.MovePosition(player.position + destination*speed);
        
    }
    void changeDirection()  //used to change the direciton of the player
    {
        Debug.Log("changing direciton");
        if(Input.GetAxis("Horizontal") > 0)
        {
            Debug.Log("Right");
            if (currentDireciton != direction.Right)
            {
                inJunction = false;
                currentDireciton = direction.Right;
            }
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            Debug.Log("Left");
            if (currentDireciton != direction.Left)
            {
                inJunction = false;
                currentDireciton = direction.Left;
            }
        }
        else if (Input.GetAxis("Vertical") > 0)
        {
            Debug.Log("Forward");
            if (currentDireciton != direction.Forward)
            {
                inJunction = false;
                currentDireciton = direction.Forward;
            }
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            Debug.Log("Back");
            if (currentDireciton != direction.Back)
            {
                inJunction = false;
                currentDireciton = direction.Back;
            }
        }
        else
        {
            currentDireciton = direction.None;
        }
    }
    void changePlane()  //for changing the current face of the maze 
    {
        Debug.Log("Changing plane");
    }
}