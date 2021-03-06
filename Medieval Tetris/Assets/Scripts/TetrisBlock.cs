﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisBlock : MonoBehaviour
{
    public static int width = 10;
    public static int height = 20;
    public static Transform[,] grid = new Transform[width, height];


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


}
