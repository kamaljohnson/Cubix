using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggering : MonoBehaviour {

    public bool trig;
    bool stay;
    void Start()
    {
        Debug.Log("the trigger class is activated ");
        trig = false;
    }
    void Update()
    {

    }
    void OnTriggerStay(Collider other)
    {
        Debug.Log("collided with wall");
        trig = true;
    }
    void OnTriggerExit(Collider other)
    {
        trig = false;
    }

}
