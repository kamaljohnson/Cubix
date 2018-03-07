using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundRayCast : MonoBehaviour {
    public bool onGround;
    void Start()
    {
        onGround = true;    
    }
    void FixedUpdate () { 
        RaycastHit hit;
        Ray Ray_down = new Ray(transform.position, Vector3.down);
        Debug.DrawRay(transform.position, Vector3.down, Color.red);
        if (Physics.Raycast(Ray_down, out hit, 2.0f))
        {
            if (hit.collider.tag == "mazeBody")
            {
                Debug.Log("On Ground");
                onGround = true;
            }
        }
        else
        {
            Debug.Log("Out of Ground");
            onGround = false;
        }
        
        }

}
