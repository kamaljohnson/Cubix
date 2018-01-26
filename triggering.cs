using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggering : MonoBehaviour {

    public bool trig;
    void OntriggerEnter(Collider other)
    {
        Debug.Log("Triggered");
        trig = true;
    }
    void OntriggerExit(Collider other)
    {
        Debug.Log("hello");
        trig = false;
    }

}
