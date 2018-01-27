using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeRotation : MonoBehaviour {

    public bool rotate;
    enum Direction  //for the direcitons 
    {
        None,
        Right,
        Left,
        Forward,
        Back
    };
    public int rotateDirection;
    int counter;
	void FixedUpdate () {
        if (rotate)
        {
            counter++;
            if (rotateDirection == (int)Direction.Right)
            {
                transform.Rotate(-2, 0, 0);
            }
            if (rotateDirection == (int)Direction.Left)
            {
                transform.Rotate(2, 0, 0);
            }
            if (rotateDirection == (int)Direction.Forward)
            {
                transform.Rotate(0, 0, -2);
            }
            if (rotateDirection == (int)Direction.Back)
            {
                transform.Rotate(0, 0, 2);
            }
            if (counter == 45)
                rotate = false;
        }
        else
            counter = 0;
    }
}
