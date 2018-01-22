using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //private GroundRayCast rayCastData;

    bool canMoveRight;  // if the player could move in this direciton 
    bool canMoveLeft;
    bool canMoveForward;
    bool canMoveBack;

    bool inMotion;      //if the player is in motion 
    bool inJunction;    //if the player is at a juntion 

    public bool Right; //direction of the player movement
    public bool Left;
    public bool Forward;
    public bool Back;

    float speed;    //speed of the player 
    int currentDireciton;

    RaycastHit hit; //raycast data 

    enum Direction { F = 0, B, R, L };

    Ray Ray_right;
    Ray Ray_left;
    Ray Ray_up;
    Ray Ray_down;
    Ray Ray_forward;
    Ray Ray_back;

    float range;

    void Start()
    {

        range = 0.5f;
        speed = 0.1f;
        canMoveRight = true;
        canMoveLeft = true;
        canMoveForward = true;
        canMoveBack = true;
        inMotion = false;
    }
	void Update () {

        Debug.DrawRay(transform.position, Vector3.right * range);
        Debug.DrawRay(transform.position, Vector3.left * range);
        Debug.DrawRay(transform.position, Vector3.up * range);
        Debug.DrawRay(transform.position, Vector3.down * range);
        Debug.DrawRay(transform.position, Vector3.forward * range);
        Debug.DrawRay(transform.position, Vector3.back * range);

        //the ray casting is done here  
        //for detection of ground and the maze walls 
        Ray_right = new Ray(transform.position, Vector3.back);   
        Ray_left = new Ray(transform.position, Vector3.forward);
        Ray_up = new Ray(transform.position, Vector3.up);
        Ray_down = new Ray(transform.position, Vector3.down);
        Ray_forward = new Ray(transform.position, Vector3.right);
        Ray_back = new Ray(transform.position, Vector3.left);

        if (!inMotion)  //if the player is not in motion then the user can input the direction of player movement 
        {
            //waiting for the swip data
            if (Input.GetAxis("Horizontal") > 0)
            {
                Right = true;
                Left = false;
                Forward = false;
                Back = false;
                Debug.Log("Right");
            }
            if (Input.GetAxis("Horizontal") < 0)
            {
                Left = true;
                Right = false;
                Forward = false;
                Back = false;
                Debug.Log("Left");
            }
            if (Input.GetAxis("Vertical") > 0)
            {
                Forward = true;
                Right = false;
                Left = false;
                Back = false;
                Debug.Log("Up");
            }
            if (Input.GetAxis("Vertical") < 0)
            {
                Back = true;
                Right = false;
                Left = false;
                Forward = false;
                Debug.Log("Down");
            }
        }
        else
        {
            Right = false;
            Left = false;
            Forward = false;
            Back = false;
        }
        if (Right && canMoveRight)
        {
            Debug.Log("Moving Right");
            transform.position += transform.right * speed;
            inMotion = true;
            currentDireciton = (int)Direction.B;
        }
        if (Left && canMoveLeft)
        {
            Debug.Log("Moving Left");
            transform.position -= transform.right * speed;
            inMotion = true;
            currentDireciton = (int)Direction.F;
        }
        if (Forward && canMoveForward)
        {

            Debug.Log("Moving Forward");
            transform.position += transform.forward * speed;
            inMotion = true;
            currentDireciton = (int)Direction.R;
        }
        if (Back && canMoveBack)
        { 
            Debug.Log("Moving Back");
            transform.position = Vector3.MoveTowards(transform.position, transform.position - transform.forward, speed);
            inMotion = true;
            currentDireciton = (int)Direction.L;
        }
        inJunction = getJunctionData();
        if (inJunction)
        {
            //code for stopping the motion of the player
            if (!Physics.Raycast(Ray_right, out hit, 0.5f))
            {
                canMoveRight = true;
            }
            else
                canMoveRight = false;
            if (!Physics.Raycast(Ray_left, out hit, 0.5f))
            {
                canMoveLeft = true;
            }
            else
                canMoveLeft = false;
            if (!Physics.Raycast(Ray_forward, out hit, 0.5f))
            {
                canMoveForward = true;
            }
            else
                canMoveForward = false;
            if (!Physics.Raycast(Ray_back, out hit, 0.5f))
            {
                canMoveBack = true;
            }
            else
                canMoveBack = false;
            inMotion = false;
        }
    }
    bool getJunctionData()
    {
        int direction = -1;
        if (Physics.Raycast(Ray_right, out hit, range))
        {
            if (hit.collider.tag == "mazeWalls")
            {
                direction = (int)Direction.R;
                Debug.Log("right collided !!");
            }
        }
        if (Physics.Raycast(Ray_left, out hit, range))
        {
            if (hit.collider.tag == "mazeWalls")
            {
                direction = (int)Direction.L;
                Debug.Log("left collided !!");
            }
        }
        if (Physics.Raycast(Ray_forward, out hit, range))
        {
            if (hit.collider.tag == "mazeWalls")
            {
                direction = (int)Direction.F;
                Debug.Log("forward collided !!");
            }
        }
        if (Physics.Raycast(Ray_back, out hit, range))
        {
            if (hit.collider.tag == "mazeWalls")
            {
                direction = (int)Direction.B;
                Debug.Log("back collided !!");
            }

        }
        Debug.Log("current direction : " + currentDireciton + " direction :" + direction);
        if ((direction == (int)Direction.R && currentDireciton == (int)Direction.R))
        {
            Debug.Log("Cant move Right");
            canMoveRight = false;
            inMotion = false;
        }
        if ((direction == (int)Direction.L && currentDireciton == (int)Direction.L))
        {
            Debug.Log("Cant move Left");
            canMoveLeft = false;
            inMotion = false;
        }
        if ((direction == (int)Direction.F && currentDireciton == (int)Direction.F))
        {
            Debug.Log("Cant move Forward");
            canMoveForward = false;
            inMotion = false;
        }
        if ((direction == (int)Direction.B && currentDireciton == (int)Direction.B))
        {
            Debug.Log("Cant move Back");
            canMoveBack = false;
            inMotion = false;
        }

        if ((currentDireciton == (int)Direction.R || currentDireciton == (int)Direction.L) && direction != (int)Direction.F)
        {
            return (true);
        }
        else if ((currentDireciton == (int)Direction.R || currentDireciton == (int)Direction.L) && direction != (int)Direction.B)
        {
            return (true);
        }
        else if ((currentDireciton == (int)Direction.F || currentDireciton == (int)Direction.B) && direction != (int)Direction.R)
        {
            return (true);
        }
        else if ((currentDireciton == (int)Direction.F || currentDireciton == (int)Direction.B) && direction != (int)Direction.L)
        {
            return (true);
        }
        else
            return (false);
    }
}

