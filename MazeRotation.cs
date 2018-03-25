using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeRotation : MonoBehaviour {

    public bool rotate;
    public GameObject mazeBody;
    public GameObject mazeWall;
    enum Direction  //for the direcitons 
    {
        None,
        Right,
        Left,
        Forward,
        Back
    };
    public Vector3 rotateDirection;
    int counter;

	void FixedUpdate () 
    {
        if (rotate)
        {
            counter++;
            transform.Rotate(rotateDirection);
            /*if (rotateDirection == (int)Direction.Right)
            {
                transform.Rotate(0, 0, 2);
            }
            else if (rotateDirection == (int)Direction.Left)
            {
                transform.Rotate(0, 0, -2);
            }
            else if (rotateDirection == (int)Direction.Forward)
            {
                transform.Rotate(-2, 0, 0);
            }
            else if (rotateDirection == (int)Direction.Back)
            {
                transform.Rotate(2, 0, 0);
            }*/
            if (counter == 45)
            {
                rotate = false;
                counter = 0;

            }
        }
            
    }
}
