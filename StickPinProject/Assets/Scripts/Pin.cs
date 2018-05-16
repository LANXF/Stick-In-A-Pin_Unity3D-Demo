using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour {

    public float speed = 5;
    private bool isFly = false;
    private bool isReach = false;
    private Transform startPoint;
    private Transform circle;

    private Vector3 targetCirclePosition;

	// Use this for initialization
	void Start () {
        startPoint = GameObject.Find("StartPoint").transform;
        circle = GameObject.Find("Circle").transform;
        targetCirclePosition = circle.position;
        targetCirclePosition.y -= 2.75f;
	}
	
	// Update is called once per frame
	void Update () {
		if(isFly == false)
        {
            if(isReach == false)
            {
               transform.position = Vector3.MoveTowards(transform.position, startPoint.position, speed * Time.deltaTime);
                if(Vector3.Distance(transform.position,startPoint.position) < .5f)
                {
                    isReach = true;
                }
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position,targetCirclePosition, 100 * Time.deltaTime);
            if(Vector3.Distance(transform.position,targetCirclePosition) < .05f)
            {
                transform.position = targetCirclePosition;
                transform.parent = circle;
                isFly = false;
            }
        }
	}

    public void startFly()
    {
        isFly = true;
        isReach = true;
    }
}
