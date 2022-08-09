using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class VoronoiCell : IPixelMask
{
    public int width { get; set; }
    public int height { get; set; }

    public int offsetX { get; }
    public int offsetY { get; }

    public Vector2Int displacement { get; }

    public bool[,] Mask { get; set; }


    public VoronoiCell(int width, int height, int offsetX, int offsetY, Vector2Int displacement)
    {
        this.width = width;
        this.height = height;

        this.offsetX = offsetX;
        this.offsetY = offsetY;

        this.displacement = displacement;

        Mask = new bool[width, height];

        for(int i = 0; i < width; i++)
        {
            for(int j = 0; j < height; j++)
            {
                Mask[i, j] = false;
            }
        }



    }
}
