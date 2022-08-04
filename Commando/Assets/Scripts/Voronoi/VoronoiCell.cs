using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//TODO this whole class is pointless and everythin could be done by just returning an ID number from VoronoiDiagram
public class VoronoiCell : IPixelMask
{
    //TODO should IPixelMask. be there???
    public int width { get; }
    public int height { get; }

    bool[,] mask;


    public VoronoiCell(int width, int height)
    {
        this.width = width;
        this.height = height;

        mask = new bool[width, height];

        for(int i = 0; i < width; i++)
        {
            for(int j = 0; j < height; j++)
            {
                mask[i, j] = false;
            }
        }


    }

    public bool getMaskAt(int i, int j)
    {

        return mask[i, j];
    }
}
