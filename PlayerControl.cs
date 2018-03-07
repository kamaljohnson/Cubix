using UnityEngine;
public class PlayerControl : MonoBehaviour {

    Rigidbody player;
    float speed;
    Vector3 destination;    //the next destination
    //the individual colliders for the player 

    public GameObject Maze;
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
    Vector3 currentPossition;
    float minMag = 0.000006f;

    enum direction  //for the direcitons 
    {
        None,
        Right,
        Left,
        Forward,
        Back
    };

    direction currentDireciton;
    bool inJunction;

    int flag;
    int Flag = 1;
    void Start () {
        flag = 0;   // gets triggered at the edges when the maze is to be turned 
        speed = 0.035f;
        currentPossition = transform.position;
        pastPosition = transform.position;
        mazeRotate = Maze.GetComponent<MazeRotation>();
        groundray = GroundRay.GetComponent<GroundRayCast>();
        rightTrig = rightCollider.GetComponent<triggering>();
        leftTrig = leftCollider.GetComponent<triggering>();
        forwardTrig = forwardCollider.GetComponent<triggering>();
        backTrig = backCollider.GetComponent<triggering>();

        currentDireciton = direction.None;
        player = GetComponent<Rigidbody>();
        inJunction = true;
	}
	
	void FixedUpdate () {
        if(Flag == 1)
        {
            currentPossition = transform.position;
            TrigCol();
            Debug.Log("in Junction : " + inJunction);
            if(!groundray.onGround && flag == 0)
            {
                Debug.Log("here1");
                changePlane();
            }
            else if(inJunction)
            {
                Debug.Log("here2");
                flag = 0;
                changeDirection();
            }
            else 
                Move();
        }
	}
    void TrigCol()  //manupulating the data of the triggered colliders RLFB
    {
        Vector2 travelled = new Vector2((currentPossition.x - pastPosition.x), (currentPossition.y - pastPosition.y));
        float mag = travelled.magnitude;
        if (rightTrig.trig)
        {
            Debug.Log("Hitting Right");
            if(currentDireciton == direction.Right)
            {
                inJunction = true;
            }
        }
        else
        {
            if (mag> minMag)
            {
                if (currentDireciton == direction.Forward || currentDireciton == direction.Back)
                {
                    inJunction = true;
                }
            }
        }
        if (leftTrig.trig)
        {
            
            Debug.Log("Hitting Left");
            if (currentDireciton == direction.Left)
            {
                inJunction = true;
            }
        }
        else
        {
            if (mag > minMag)
            {
                if (currentDireciton == direction.Forward || currentDireciton == direction.Back)
                {
                    inJunction = true;
                }
            }
        }
        if (forwardTrig.trig)
        {
            
            Debug.Log("Hitting Up");
            if (currentDireciton == direction.Forward)
            {
                inJunction = true;
            }
        }
        else
        {
            if (mag > minMag)
            {
                if (currentDireciton == direction.Right || currentDireciton == direction.Left)
                {
                    inJunction = true;
                }
            }
        }
        if (backTrig.trig)
        {
            Debug.Log("Hitting Back");
            if (currentDireciton == direction.Back)
            {
                inJunction = true;
            }
        }
        else
        {
            if (mag > minMag)
            {
                if (currentDireciton == direction.Right || currentDireciton == direction.Left)
                {
                    inJunction = true;
                }
            }
        }
    }
    void Move() //this funtion will controll the movement on the maze plane 
    {
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
        float steps = 0.1f;
        //player.transform.position = Vector3.MoveTowards(player.transform.position, player.transform.position+destination *2, 2);
        player.MovePosition(player.position + destination*4);
        Flag = 0;
    }
    void changeDirection()  //used to change the direciton of the player
    {
       // Debug.Log("changing direciton");
        if(Input.GetAxis("Horizontal") > 0)
        {
            Debug.Log("Right");
            if (!rightTrig.trig)
            {
                inJunction = false;
                pastPosition = currentPossition;
                currentDireciton = direction.Right;
            }
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            Debug.Log("Left");
            if (!leftTrig.trig)
            {
                inJunction = false;
                pastPosition = currentPossition;
                currentDireciton = direction.Left;
            }
        }
        else if (Input.GetAxis("Vertical") > 0)
        {
            Debug.Log("Up");
            if (!forwardTrig.trig)
            {
                inJunction = false;
                pastPosition = currentPossition;
                currentDireciton = direction.Forward;
            }
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            Debug.Log("Down");
            if (!backTrig.trig)
            {
                inJunction = false;
                pastPosition = currentPossition;
                currentDireciton = direction.Back;
            }
        }
    }
    void changePlane()  //for changing the current face of the maze 
    {
        if(currentDireciton == direction.Right)
        {
            flag = 1;
            mazeRotate.rotateDirection = (int)direction.Right;
            transform.Rotate(90, 0, 0);
            mazeRotate.rotate = true;
        }
        if (currentDireciton == direction.Left)
        {
            flag = 1;
            mazeRotate.rotateDirection = (int)direction.Left;
            transform.Rotate(-90, 0, 0);
            mazeRotate.rotate = true;
        }
        if (currentDireciton == direction.Forward)
        {
            flag = 1;
            mazeRotate.rotateDirection = (int)direction.Forward;
            transform.Rotate(0, 0, 90);
            mazeRotate.rotate = true;
        }
        if (currentDireciton == direction.Back)
        {
            flag = 1;
            mazeRotate.rotateDirection = (int)direction.Back;
            transform.Rotate(0, 0, -90);
            mazeRotate.rotate = true;
        }
    }
}
