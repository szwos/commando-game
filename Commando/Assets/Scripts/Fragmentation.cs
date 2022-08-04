using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this class is used to take a sprite, and return an array of sprites as effect of the shprite defragmentation

public class Fragmentation
{
    Sprite baseSprite;
    Texture2D baseTexture;
    List<Sprite> sprites = null;

    public Fragmentation(Sprite sprite)
    {
        if (sprite.texture.isReadable == false)
            throw new System.FieldAccessException("Texture used by base Sprite is non readable");

        baseSprite = sprite;
        baseTexture = sprite.texture;   
    }

    public void fragmentate()
    {
        
    }

    public Sprite getChunkAt(int i)
    {
        if (sprites.Count == 0)
        {
            Debug.Log("Call Fragmentation.freagmentate() first before accessing Chunks");
            return null;
        }


        return sprites[i];
    }


}
