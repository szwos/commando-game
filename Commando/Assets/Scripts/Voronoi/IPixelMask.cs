using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPixelMask
{
    public int width { get; }
    public int height { get; }
    public int offsetX { get; }
    public int offsetY { get; }
    
    //how much mask's center is displaced from original Texture center
    public Vector2Int displacement { get; }

    public bool[,] Mask { get;}

}
