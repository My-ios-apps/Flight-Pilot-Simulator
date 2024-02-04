using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollReactSnap : MonoBehaviour {

	// Public variables
	public RectTransform  panel; //To hold the scrollpanal
	public Button[] btn;
	public RectTransform center; //Center to compare the distance for each button

	//Private variables
	private float[] distance; //All button's distance to the center
	private bool dragging= false; //will be true,while we drag the panal
	private int btnDistance; //will hold the distance between the buttons
	private int minButtonNum; //to hold the number of the button, with smallest distance to center

	// Use this for initialization
	void Start () 
	{
		int btnLenght = btn.Length;
		distance = new float[btnLenght]; 

		//Get distance between button
		btnDistance = (int)Mathf.Abs(btn[1].GetComponent<RectTransform>().anchoredPosition.x - btn[0].GetComponent<RectTransform>().anchoredPosition.x);
	}  
	
	// Update is called once per frame
	void Update ()
	{
		for(int i=0 ; i<btn.Length; i++)
		{
			distance [i] = Mathf.Abs (center.transform.position.x - btn [i].transform.position.x);
		}

		float minDistance = Mathf.Min (distance);
		for (int a=0; a<btn.Length; a++)
		{
			if(minDistance == distance[a])
			{
				minButtonNum = a;
			}
		}

		if(!dragging)
		{
			LerpToBttn (minButtonNum * -btnDistance);	
		}

	}

	void LerpToBttn(int position)
	{
 		float newX = Mathf.Lerp (panel.anchoredPosition.x, position, Time.deltaTime * 5f);
		Vector2 NewPosition = new Vector2 (newX, panel.anchoredPosition.y);

		panel.anchoredPosition = NewPosition;

	}

	public void StartDrag()
	{
		dragging = true;
	}

	public void EndDrag()
	{
		dragging = false;
	}
}
