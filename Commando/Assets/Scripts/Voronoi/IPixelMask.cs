using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPixelMask
{
    public int width { get; }
    public int height { get; }


    public bool getMaskAt(int i, int j);
}
