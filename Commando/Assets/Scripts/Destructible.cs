using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO sortingLayer
//TODO Layer (create a new one for enviromentEffects that cannot be interacted with, but will collide with ground, other enviroment props)
//TODO Bouncy material
//TODO smoothly disappearing after time
//TODO rescale collider to approximately fit fragment's shape
//TODO increased density of random seeds in place of collision that destroys gameobject

public class Destructible : MonoBehaviour, IHealth
{
    public int health = 100;
    public byte numberOfFragments;
    //public DestructionEffect destructionEffect , then destructionEffect.explode();
    public GameObject fragmentPrefab;

    
    

    public void TakeDamage(int damage)
    {
        //string msg = health.ToString() + " -> " + (health - damage).ToString();
        //Debug.Log(msg);

        health -= damage;


        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        //Instantiate(deathEffect, transform.position, Quaternion.identity);
        Explode();
        Destroy(gameObject);



    }
        
   
    void Explode()
    {
        Texture2D tex = gameObject.GetComponent<SpriteRenderer>().sprite.texture;
        //Pixels per unit
        float PPU = gameObject.GetComponent<SpriteRenderer>().sprite.pixelsPerUnit;
        Debug.Log(tex.isReadable);

        VoronoiDiagram diagram = new VoronoiDiagram(tex.width, tex.height, numberOfFragments, false);


        for (int i = 0; i < numberOfFragments; i++)
        {
            IPixelMask mask = diagram.getCell(i);

            Texture2D newTex = assignFragment(tex, mask, i);

            Sprite newSprite = Sprite.Create(newTex, new Rect(0.0f, 0.0f, newTex.width, newTex.height), new Vector2(0.5f, 0.5f), PPU);


            GameObject fragment = Instantiate(fragmentPrefab);

            
            
            fragment.transform.position = new Vector3(transform.position.x + mask.displacement.x/PPU, transform.position.y + mask.displacement.y/PPU, transform.position.z);

            fragment.GetComponent<SpriteRenderer>().sprite = newSprite;
        }

    }

    
    //TODO if i will need to set pivot to a specific point (e.g centre of mass) or get any other value from mask that will be assigned to sprite, i will need this function to return sprite instead and do that assignment here
    //TODO /\ yes i will need to and that attribute is position relative to source texture position, so i will even need to make this function operate on gameObject
    //TODO /\ not rly, bcs i have access to mask out of this scope so no problem
    //TODO change name, assignFragment doesn't really give much information
    Texture2D assignFragment(Texture2D srcTexture, IPixelMask mask, int cellNumber)
    {

        Texture2D newTex = new Texture2D(mask.width, mask.height);

        for (int i = 0; i < newTex.width; i++)
        {
            for (int j = 0; j < newTex.height; j++)
            {
                if (mask.Mask[i, j])
                {
                    newTex.SetPixel(i, j, srcTexture.GetPixel(i + mask.offsetX, j + mask.offsetY));
                } else
                {
                    newTex.SetPixel(i, j, Color.clear);
                }
            }
        }


        newTex.Apply();

        newTex.filterMode = FilterMode.Point;

        
       

        //fragmentGO.position = gameObject.position + offsetVector() * pixelDensityFactor

        return newTex;
    }



}
