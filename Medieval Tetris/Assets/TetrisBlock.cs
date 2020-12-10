﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisBlock : MonoBehaviour
{
    public Vector3 rotationPoint;
	private float previousTime;
	[SerializeField] private float fallTime;
    [SerializeField] private float accelerationFactor;

    public static int heigth = 20;
    public static int width = 10;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-1, 0, 0);
            if (!ValidMove())
            {
                transform.position -= new Vector3(-1, 0, 0);
            }
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += new Vector3(+1, 0, 0);
            if (!ValidMove())
            {
                transform.position -= new Vector3(+1, 0, 0);
            }
        }else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0,0,1), 90);
            if(!ValidMove()) transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -90);

        }

        if(Time.time-previousTime > (Input.GetKey(KeyCode.DownArrow) ? fallTime / accelerationFactor : fallTime)){
            transform.position += new Vector3(0,-1,0);
            if (!ValidMove())
            {
                transform.position -= new Vector3(0, -1, 0);
            }
            previousTime = Time.time;
        }
    }


    bool ValidMove()
    {
        foreach (Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);

            if (roundedX < 0 || roundedX >= width || roundedY < 0 || roundedY >= heigth)
            {
                return false;
            }
        }

        return true;
    }
}
