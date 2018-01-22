using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundRayCast : MonoBehaviour {

    private int currentGround;
    public bool inJunction;
    public GameObject Maze;
    Transform mazeTransform;
    void Start()
    {
        mazeTransform = Maze.GetComponent<Transform>();    
    }
    void Update () { 
        RaycastHit hit;
        Ray Ray_right = new Ray(transform.position, Vector3.right);   //the direction of the ray to the ground from the player
        Ray Ray_left = new Ray(transform.position, Vector3.left);
        Ray Ray_up = new Ray(transform.position, Vector3.up);
        Ray Ray_down = new Ray(transform.position, Vector3.down);
        Ray Ray_forward = new Ray(transform.position, Vector3.forward);
        Ray Ray_back = new Ray(transform.position, Vector3.back);

        //Debug.DrawRay(transform.position, Vector3.right);
        //Debug.DrawRay(transform.position, Vector3.left);
        //Debug.DrawRay(transform.position, Vector3.up);
        //Debug.DrawRay(transform.position, Vector3.down);
        //Debug.DrawRay(transform.position, Vector3.forward);
        //Debug.DrawRay(transform.position, Vector3.back);

        if (Physics.Raycast(Ray_right, out hit, 0.5f))
        {
            if (hit.collider.tag == "mazeBody")
            {   
                currentGround = 1;
                Debug.Log("On Ground");
            }
            if (hit.collider.tag == "mazeWalls")
            {
                Debug.Log("right collided !!");
            }
        }
        else if (Physics.Raycast(Ray_left, out hit, 0.5f))
        {
            if (hit.collider.tag == "mazeBody")
            {
                currentGround = 2;
                Debug.Log("On Ground");
            }
            if (hit.collider.tag == "mazeWalls")
            {
                Debug.Log("On Ground");
            }
        }
        else if (Physics.Raycast(Ray_up, out hit, 0.5f))
        {
            if (hit.collider.tag == "mazeBody")
            {
                currentGround = 3;
                Debug.Log("On Ground");
            }
            if (hit.collider.tag == "mazeWalls")
            {
                Debug.Log("On Ground");
            }
        }
        else if (Physics.Raycast(Ray_down, out hit, 0.5f))
        {
            if (hit.collider.tag == "mazeBody")
            {
                currentGround = 4;
                Debug.Log("On Ground");
            }
            if (hit.collider.tag == "mazeWalls")
            {
                Debug.Log("On Ground");
            }
        }
        else if (Physics.Raycast(Ray_forward, out hit, 0.5f))
        {
            if (hit.collider.tag == "mazeBody")
            {
                currentGround = 5;
                Debug.Log("On Ground");
            }
            if (hit.collider.tag == "mazeWalls")
            {
                Debug.Log("On Ground");
            }
        }
       else if (Physics.Raycast(Ray_back, out hit, 0.5f))
        {
            if (hit.collider.tag == "mazeBody")
            {
                currentGround = 6;
                Debug.Log("On Ground");
            }
            if (hit.collider.tag == "mazeWalls")
            {
                Debug.Log("On Ground");
            }
        }
        else //the condition for changing the ground
        {
            if (currentGround == 1) // move right on step and rotate the maze in counter this direction 
            {
                Debug.Log("1");
            }
            if (currentGround == 2) // move left 
            {
                Debug.Log("2");
            }
            if (currentGround == 3) // move up
            {
                Debug.Log("3");
            }
            if (currentGround == 4) // move down
            {
                Debug.Log("4");
            }
            if (currentGround == 5) // move forward 
            {
                Debug.Log("5");
            }
            if (currentGround == 6) // move back
            {
                Debug.Log("6");
            }

        }

    }
}
