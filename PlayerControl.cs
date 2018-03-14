using UnityEngine;
public class PlayerControl : MonoBehaviour {

    Transform player;
    float speed;
    Vector3 destination;    //the next destination
    //the individual colliders for the player 

    float BoundaryLimits;
    public GameObject Maze;
    public GameObject mazeBody;
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
    Vector3 currentPosition;
    float minMag = 1.0f;
    float mag;
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
    Transform MazeLimits;
    bool inJunction;
    bool Moving;
    int flag;
    int FLAG = 1;
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
        currentPosition = player.localPosition;
        pastPosition = currentPosition;
        destination = player.localPosition;
        inJunction = true;
        Moving = false;
        BoundaryLimits = mazeBody.transform.localScale.x + transform.localScale.x/2;
	}
	
	void FixedUpdate () 
    {

        Debug.Log("FLAG : " + flag);
        currentPosition = player.localPosition;
        if(flag == 0)
        {
            TrigCol();
        }
        if(Mathf.Abs(currentPosition.x) > BoundaryLimits || Mathf.Abs(currentPosition.y) > BoundaryLimits|| Mathf.Abs(currentPosition.z) > BoundaryLimits && flag == 0)
        {
            if(player.localPosition.x > 4.5f)
            {
                player.localPosition = new Vector3(4.5f, player.localPosition.y, player.localPosition.z);
            }
            else if(player.localPosition.x < -4.5f)
            {
                player.localPosition = new Vector3(-4.5f, player.localPosition.y, player.localPosition.z);
            }
            else if(player.localPosition.y > 4.5f)
            {
                player.localPosition = new Vector3(player.localPosition.x, 4.5f, player.localPosition.z);
            }
            else if(player.localPosition.y < -4.5f)
            {
                player.localPosition = new Vector3(player.localPosition.x, -4.5f, player.localPosition.z);
            }
            else if(player.localPosition.z > 4.5f)
            {
                player.localPosition = new Vector3(player.localPosition.x, player.localPosition.y, 4.5f);
            }
            else if(player.localPosition.z < -4.5f)
            {
                player.localPosition = new Vector3(player.localPosition.x, player.localPosition.y, -4.5f);
            }
            changePlane();
        }
        else if(!inJunction)
        {

            if(player.localPosition == destination && flag == 0)
            {
                directionFlag = direction.None;
            }
            else if(flag == 1)
            {
                currentDireciton = directionFlag;
            }
            Debug.Log("Direction flag : " + directionFlag);
            Move();
        }
        else if(inJunction)
        {
            Moving = false;
            player.localPosition = destination;
            directionFlag = direction.None;
            flag = 0;
            changeDirection();
        }
	}

    void TrigCol()  //manupulating the data of the triggered colliders RLFB
    {
        Vector2 travelled = new Vector2((currentPosition.x - pastPosition.x), (currentPosition.y - pastPosition.y));
        float mag = travelled.magnitude;
        if (rightTrig.trig)
        {
            if(currentDireciton == direction.Right)
            {
                inJunction = true;
                Moving = false;
            }
        }
        else
        {
            if (mag> minMag)
            {
                if (currentDireciton == direction.Forward || currentDireciton == direction.Back)
                {
                    inJunction = true;
                    Moving = false;
                }
            }
        }
        if (leftTrig.trig)
        {
            
            if (currentDireciton == direction.Left)
            {
                inJunction = true;
                Moving = false;
            }
        }
        else
        {
            if (mag > minMag)
            {
                if (currentDireciton == direction.Forward || currentDireciton == direction.Back)
                {
                    inJunction = true;
                    Moving = false;
                }
            }
        }
        if (forwardTrig.trig)
        {
            
            if (currentDireciton == direction.Forward)
            {
                inJunction = true;
                Moving = false;
            }
        }
        else
        {
            if (mag > minMag)
            {
                if (currentDireciton == direction.Right || currentDireciton == direction.Left)
                {
                    inJunction = true;
                    Moving = false;
                }
            }
        }
        if (backTrig.trig)
        {
            if (currentDireciton == direction.Back)
            {
                inJunction = true;
                Moving = false;
            }
        }
        else
        {
            if (mag > minMag)
            {
                if (currentDireciton == direction.Right || currentDireciton == direction.Left)
                {
                    inJunction = true;
                    Moving = false;
                }
            }
        }
    }
    void Move() //this funtion will controll the movement on the maze plane 
    {
        //code to move a single step on the plane 
        Debug.Log("moving. . .");
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
                    Debug.Log("destination : " + destination);
                    directionFlag = direction.Back;
                }
                break;
        }
        if(player.localPosition == destination)
        {
            directionFlag = direction.None;
        }
        Debug.Log("current location : " + player.transform.localPosition);
        Moving = true;
        player.localPosition = Vector3.MoveTowards(player.localPosition, destination, 0.1f);
    }
    void changeDirection()  //used to change the direciton of the player
    {
        if(Input.GetAxis("Horizontal") > 0 && !Moving)
        {
            if (!rightTrig.trig)
            {
                Moving = true;
                inJunction = false;
                pastPosition = currentPosition;
                currentDireciton = direction.Right;
            }
        }
        else if (Input.GetAxis("Horizontal") < 0 && !Moving)
        {
            if (!leftTrig.trig)
            {
                Moving = true;
                inJunction = false;
                pastPosition = currentPosition;
                currentDireciton = direction.Left;
            }
        }
        else if (Input.GetAxis("Vertical") > 0 && !Moving)
        {
            if (!forwardTrig.trig)
            {
                Moving = true;
                inJunction = false;
                pastPosition = currentPosition;
                currentDireciton = direction.Forward;
            }
        }
        else if (Input.GetAxis("Vertical") < 0 && !Moving)
        {
            if (!backTrig.trig)
            {
                Moving = true;
                inJunction = false;
                pastPosition = currentPosition;
                currentDireciton = direction.Back;
            }
        }
    }
    void changePlane()  //for changing the current face of the maze 
    {
        destination = player.localPosition + Vector3.down*1.5f;
        player.localPosition = destination;
        mazeRotate.rotate = true;
        if(currentDireciton == direction.Right)
        {
            mazeRotate.rotateDirection = (int)direction.Right;
            transform.Rotate(0, 0, -90);
            
        }
        else if (currentDireciton == direction.Left)
        {
            mazeRotate.rotateDirection = (int)direction.Left;
            transform.Rotate(0, 0, 90);
        }
        else if (currentDireciton == direction.Forward)
        {
            mazeRotate.rotateDirection = (int)direction.Forward;
            transform.Rotate(90, 0, 0);
        }
        else if (currentDireciton == direction.Back)
        {
            mazeRotate.rotateDirection = (int)direction.Back;
            transform.Rotate(-90, 0, 0);
        }
        flag = 1;
    }
}
