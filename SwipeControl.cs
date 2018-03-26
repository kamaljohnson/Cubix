using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeControl : MonoBehaviour {

	// Use this for initialization
	Vector2 StartTouch;
	Vector2 SwipeDelta;
	bool Touch;
	enum Direction
	{
		None,
		Right,
		Left,
		Forward,
		Back
	};
	Direction SwipeDirection;
	void Reset()
	{
		Touch = false;
	}
	void Start () {
		Touch = false;
		SwipeDelta = Vector2.zero;
		SwipeDirection = Direction.None;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.touches.Length > 0)	//touched
		{
			#region  MouseInput
			if(Input.GetMouseButtonDown(0))
			{
				Touch = true;
				StartTouch = Input.mousePosition;
			}
			else if(Input.GetMouseButtonUp(0))
			{
				Reset();
			}
			#endregion
			
			#region MobileInput
			if(Input.touches[0].phase == TouchPhase.Began)
			{
				Touch = true;
				StartTouch = Input.touches[0].position;
			}
			else if(Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
			{
				Reset();
			}
			#endregion
		}

		if(Touch)
		{
			if(Input.touches.Length > 0)
			{
				SwipeDelta = Input.touches[0].position - StartTouch;
			}
			else if(Input.GetMouseButtonDown(0))
			{
				SwipeDelta = (Vector2)Input.mousePosition - StartTouch;
			}
		}

		if(SwipeDelta.magnitude > 120)
		{
			float x = SwipeDelta.x;
			float y = SwipeDelta.y;
			
			if(x > 0 && y > 0)
			{
				SwipeDirection = Direction.Forward;
			}
			if(x < 0 && y < 0)
			{
				SwipeDirection = Direction.Back;
			}
			if(x > 0 && y < 0)
			{
				SwipeDirection = Direction.Right;
			}
			if(x < 0 && y > 0)
			{
				SwipeDirection = Direction.Left;
			}
			Reset();
		}
	}
	public Direction SwipeInput()
	{	
		return((int)SwipeDirection);
	}
}
