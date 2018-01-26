using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggering : MonoBehaviour {

    public bool trig;
    void Start()
    {
        Debug.Log("the trigger class is activated ");
        trig = false;
    }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered");
        trig = true;
    }
    void OnTriggerExit(Collider other)
    {
        Debug.Log("hello");
        trig = false;
    }

}
