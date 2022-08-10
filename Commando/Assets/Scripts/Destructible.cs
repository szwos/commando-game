using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO sortingLayer
//TODO rescale collider to approximately fit fragment's shape
//TODO increased density of random seeds in place of collision that destroys gameobject

public class Destructible : MonoBehaviour, IHealth
{
    public int health = 100;
    public byte numberOfFragments;
    public float explosionSpeed = 2;

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
        //Debug.Log(tex.isReadable); //TODO catch exception if is not readable

        VoronoiDiagram diagram = new VoronoiDiagram(tex.width, tex.height, numberOfFragments, false);

        for (int i = 0; i < numberOfFragments; i++)
        {
            //get mask
            IPixelMask mask = diagram.getCell(i);

            //create texture
            Texture2D newTex = assignFragment(tex, mask);

            //create sprite
            Sprite newSprite = Sprite.Create(newTex, new Rect(0.0f, 0.0f, newTex.width, newTex.height), new Vector2(0.5f, 0.5f), PPU);

            //instantiate
            GameObject fragment = Instantiate(fragmentPrefab);


            //assign sprite
            fragment.GetComponent<SpriteRenderer>().sprite = newSprite;

            //adjust position
            fragment.transform.position = new Vector3(transform.position.x + mask.displacement.x/PPU, transform.position.y + mask.displacement.y/PPU, transform.position.z);

            //set in motion
            Rigidbody2D fragmentRB = fragment.GetComponent<Rigidbody2D>();
            float angle = Vector2.SignedAngle(Vector2.right, fragment.transform.position - transform.position);
            fragmentRB.velocity = new Vector2(Mathf.Cos(angle*Mathf.Deg2Rad) * explosionSpeed, Mathf.Sin(angle*Mathf.Deg2Rad) * explosionSpeed);


        }
    }

    
    Texture2D assignFragment(Texture2D srcTexture, IPixelMask mask)
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
