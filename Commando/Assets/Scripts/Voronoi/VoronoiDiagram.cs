using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class VoronoiDiagram
{
    
    public int size { get;}
    private VoronoiCell[] cells;
    private byte[,] grid;
    public byte[,] finalGrid { get; }
    Vector2Int[] seeds;
    private bool manhattanDistance;

    //max number of cells is 255
    public VoronoiDiagram(int width, int height, byte numberOfCells, bool isManhattan)//TODO make width and height class fields to avoid passing it to every function
    {
        //size of Diagram has to be a square with powers of 2 dimensions for algorithm to work
        //adjusting size to fit those requirements

        manhattanDistance = isManhattan;


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

        finalGrid = new byte[width, height];

        //setting determined number of seeds
        setSeeds(numberOfCells);
        
        calculate();

        rescale(width, height);

        assignCells(numberOfCells, width, height);
    }

    void calculate()
    {
        int k = size/2;

        while (k >= 1)
        {
            for(int x = 0; x < size; x++)
            {
                for(int y = 0; y < size; y++)
                {
                    List<Vector2Int> neighbours = getNeighbours(x, y, k);

                    foreach(Vector2Int q in neighbours)
                    {
                        if (grid[x, y] == 0 && grid[q.x, q.y] > 0)
                        {
                            grid[x, y] = grid[q.x, q.y];
                        }
                        if(grid[x, y] > 0 && grid[q.x, q.y] > 0)
                        {
                            Vector2Int p = new Vector2Int(x, y);
                            Vector2Int sp = seeds[grid[x, y]];
                            Vector2Int sq = seeds[grid[q.x, q.y]];
                            
                            if(distance(p, sp) > distance(p, sq))
                            {
                                grid[x, y] = grid[q.x, q.y];
                            }   
                        }
                    }
                }
            }


            k /= 2;
        }
    }

    float distance(Vector2Int P1, Vector2Int P2)
    {
        if (manhattanDistance)
        {
            return (Mathf.Abs(P2.x - P1.x) + Mathf.Abs(P2.y - P1.y));
        } else
        {
            return Mathf.Sqrt((P2.x - P1.x) * (P2.x - P1.x) + (P2.y - P1.y) * (P2.y - P1.y));
        }
    }

    //TODO maybe this should be an array, check performance
    //for array to work, if (x + i >= 0 && x + i < size && y + j >= 0 && y + j < size) should be
    //checked after array is returned and array size will always be 8 (for 8 neighbours, bcs you ignore x + 0, y + 0)
    //Vector2Int[] getNeighbours(int x, int y, int k)
    List<Vector2Int> getNeighbours(int x, int y, int k)
    {
        List<Vector2Int> neighbours = new List<Vector2Int>();

        for(int i = -k; i <= k; i += k)
        {
            for(int j = -k; j <= k; j += k)
            {
                if (x + i >= 0 && x + i < size && y + j >= 0 && y + j < size)
                {
                    neighbours.Add(new Vector2Int(x + i, y + j));
                }
            }
        }



        return neighbours;
    }

    void setSeeds(byte numberOfCells)
    {
        seeds = new Vector2Int[numberOfCells+1];

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

    

    void rescale(int width, int height)
    {

        if (width != size || height != size)
        {
            float xFactor = size / width;
            float yFactor = size / height;

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    finalGrid[x, y] = grid[Mathf.FloorToInt(x * xFactor), Mathf.FloorToInt(y * yFactor)];
                }
            }
        }else
        {
            for(int x = 0; x < width; x++)
            {
                for(int y = 0; y < height; y++)
                {
                    finalGrid[x, y] = grid[x, y];
                }
            }
        }
    }

    struct CellDimensions
    {
        public int minX;
        public int minY;
        public int maxX;
        public int maxY;
    }

    void assignCells(int numberOfCells, int width, int height)
    {
        cells = new VoronoiCell[numberOfCells];
        
        for(int i = 0; i < numberOfCells; i++)
        {
            CellDimensions dim = findDimensions(i + 1, width, height);

            // MAX - MIN = LENGTH                                                                   
            Vector2Int displacement = seeds[i + 1] - new Vector2Int(width / 2, height / 2);
            cells[i] = new VoronoiCell(dim.maxX - dim.minX, dim.maxY - dim.minY, dim.minX, dim.minY, displacement);

            for(int x = 0; x < dim.maxX - dim.minX; x++)
            {
                for(int y = 0; y < dim.maxY - dim.minY; y++)
                {
                    if(finalGrid[x + dim.minX, y + dim.minY] == i + 1)
                    {
                        cells[i].Mask[x, y] = true;
                    }
                }
            }
        }

    }
    CellDimensions findDimensions(int cellNumber, int width, int height)
    {
        CellDimensions dim = new CellDimensions();
        dim.minX = width;
        dim.minY = height;
        dim.maxX = 0;
        dim.maxY = 0;

        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                if(finalGrid[x, y] == cellNumber)
                {
                    if (dim.minX > x)
                        dim.minX = x;
                    if (dim.minY > y)
                        dim.minY = y;
                    if (dim.maxX < x)
                        dim.maxX = x;
                    if (dim.maxY < y)
                        dim.maxY = y;
                }
            }
        }

        return dim;
    }


    public VoronoiCell getCell(int i)
    {
        return cells[i];
    }

}
