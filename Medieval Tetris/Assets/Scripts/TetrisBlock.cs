﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisBlock : MonoBehaviour
{
    public Vector3 rotationPoint;
	private float previousTime;

    public static int width = 10;
    public static int height = 20;

    public static Transform[,] grid = new Transform[width, height];

    [SerializeField] private float fallTime;
    [SerializeField] private float accelerationFactor;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveTetromino(new Vector3(-1, 0, 0));
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveTetromino(new Vector3(+1, 0, 0));
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            RotateTetromino();
        }

        if(Time.time-previousTime > (Input.GetKey(KeyCode.DownArrow) ? fallTime / accelerationFactor : fallTime)){
            transform.position += new Vector3(0,-1,0);
            if (!ValidMove())
            {
                StopTetrominoMovement();
            }
            previousTime = Time.time;
        }
    }

    public void StopTetrominoMovement()
    {
        transform.position -= new Vector3(0, -1, 0);
        AddToGrid();
        CheckForLines();
        this.enabled = false;
        FindObjectOfType<TetrominoSpawner>().SpawnTetromino(); //should inform game manager
    }

    public void MoveTetromino(Vector3 vectorMovement)
    {
        transform.position += vectorMovement;
        if (!ValidMove())
        {
            transform.position -= vectorMovement;
        }
    }
    public void RotateTetromino()
    {
        transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);
        if (!ValidMove()) transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -90);
    }

    public void AddToGrid()
    {
        foreach (Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);

            grid[roundedX, roundedY] = children;
        }
    }

    void CheckForLines()
    {
        for(int i=height-1; i >= 0; i--)
        {
            if (FullLineExists(i))
            {
                DeleteLine(i);
                DecreaseRow(i);
            }
        }
    }

    bool FullLineExists(int i)
    {
        for (int j =0; j<width; j++)
        {
            if(grid[j,i] == null)
            {
                return false;
            }
        }
        return true;
    }

    public void DeleteLine(int i)
    {
        for (int j=0; j<width; j++)
        {
            Destroy(grid[j, i].gameObject);
            grid[j, i] = null;

        }
    }

    public void DecreaseRow(int i)
    {
        for(int y=i; y<height; y++)
        {
            for (int j = 0; j < width; ++j)
            {
                if (grid[j, y] != null)
                {
                    grid[j, y].transform.position -= new Vector3(0, 1, 0);
                    grid[j, y - 1] = grid[j, y];
                    grid[j, y] = null;
                }
            }
        }
    }

    bool ValidMove()
    {
        foreach (Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);

            if (roundedX < 0 || roundedX >= width || roundedY < 0 || roundedY >= height)
            {
                return false;
            }

            if (grid[roundedX, roundedY] != null)
            {
                return false;
            }
        }

        return true;
    }
}
