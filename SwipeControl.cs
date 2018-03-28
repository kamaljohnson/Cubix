using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeControl : MonoBehaviour {

	// Use this for initialization
	Vector2 StartTouch;
	Vector2 SwipeDelta;
	bool Touch;
	public bool SwipeRight;
	public bool SwipeLeft;
	public bool SwipeForward;
	public bool SwipeBack;

	void Reset()
	{
		Touch = false;

	}
	void Start () {
		Touch = false;
		SwipeDelta = Vector2.zero;

		SwipeRight = false;
		SwipeLeft = false;
		SwipeForward = false;
		SwipeBack = false;
	}
	
	// Update is called once per frame
	private void Update () {
		if(Input.GetMouseButtonDown(0) && !Touch)
		{	
			Debug.Log("Here");
			Touch = true;
			StartTouch = Input.mousePosition;	
		}
		else if(Input.GetMouseButtonUp(0))
		{	
			Reset();
		}
		if(Input.touches.Length > 0)	//touched
		{
			if(Input.touches[0].phase == TouchPhase.Began)
			{
				Touch = true;
				StartTouch = Input.touches[0].position;
			}
			else if(Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
			{
				Reset();
			}
		}
		SwipeDelta = Vector2.zero;
		if(Touch)
		{
			if(Input.touches.Length > 0)
			{
				SwipeDelta = Input.touches[0].position - StartTouch; 
			}
			if(Input.GetMouseButton(0))
			{
				SwipeDelta = (Vector2)Input.mousePosition - StartTouch;

				Debug.Log("mousePosition : " + (Vector2)Input.mousePosition);
			}
		}
		Debug.Log("Magnitude : " + SwipeDelta.magnitude);
		if(SwipeDelta.magnitude > 100)
		{
			float x = SwipeDelta.x;
			float y = SwipeDelta.y;

			Debug.Log("X : " + x);
			Debug.Log("Y : " + y);
			if(x > 0 && y < 0)
			{
				SwipeRight = true;
			}
			else
			{
				SwipeRight = false;
			}
			if(x < 0 && y > 0)
			{
				SwipeLeft = true;
			}
			else
			{
				SwipeLeft = false;
			}
			if(x > 0 && y > 0)
			{
				SwipeForward = true;
			}
			else
			{
				SwipeForward = false;
			}
			if(x < 0 && y < 0)
			{
				SwipeBack = true;
			}
			else
			{
				SwipeBack = false;
			}
			Reset();
		}
	}
	
}
