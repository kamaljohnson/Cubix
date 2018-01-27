using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundRayCast : MonoBehaviour {
    public bool onGround;
    void Update () { 
        RaycastHit hit;
        Ray Ray_down = new Ray(transform.position, Vector3.down);

      
        if (Physics.Raycast(Ray_down, out hit, 0.5f))
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
