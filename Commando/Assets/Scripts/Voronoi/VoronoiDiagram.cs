using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class VoronoiDiagram
{
    struct Seed
    {
        public int x;
        public int y;
    }

    public int size { get;}
    public List<VoronoiCell> cells;
    public byte[,] grid;
    Seed[] seeds;


    //max number of cells is 255
    public VoronoiDiagram(int width, int height, byte numberOfCells)
    {
        //size of Diagram has to be a square with powers of 2 dimensions for algorithm to work
        //adjusting size to fit those requirements
        


        if (width != height)
        {
            if (width > height)
                size = width;
            else
                size = height;
        }
        else
            size = width;

        size = Mathf.NextPowerOfTwo(size);

        //initiating grid with 0's
        grid = new byte[size, size];
        for(int i = 0; i < size; i++)
        {
            for(int j = 0; j < size; j++)
            {
                grid[i, j] = 0;
            }
        }

        //setting determined number of seeds
        setSeeds(numberOfCells);

        //TODO maybe this should be public and not invoked in constructor idk
        calculate();
    }

    void calculate()
    {
        int k = size;

        while (k/2 >= 1)
        {
            for(int x = 0; x < size; x++)
            {
                for(int y = 0; y < size; y++)
                {

                }
            }


            k /= 2;
        }
    }

    void setSeeds(byte numberOfCells)
    {
        seeds = new Seed[numberOfCells+1];

        int x, y;

        for(byte i = 1; i <= numberOfCells; i++)
        {

            do
            {
                x = Random.Range(0, size);
                y = Random.Range(0, size);
            } while (grid[x, y] != 0);

            grid[x, y] = i;
            seeds[i].x = x;
            seeds[i].y = y;
        }
    }

    void rescale()
    {
        //TODO rescale output matrix back to its original width x height size 
    }

    public VoronoiCell getCell(int i)
    {
        //TODO cells HAVE TO be rescaled to original texture size before this happens
        //no problems if there was no resizing and original texture was 2**n X 2**n , n= 1, 2, 3...
        return cells[i];
    }

}
