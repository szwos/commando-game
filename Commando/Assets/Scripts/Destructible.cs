using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour, IHealth
{
    public int health = 100;
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
        
    Texture2D generateTextureUsingMask(Texture2D sourceTexture, IPixelMask mask) {
        Texture2D newTex = new Texture2D(mask.width, mask.height);

        for(int i = 0; i < mask.width; i++)
        {
            for(int j = 0; j < mask.height; j++)
            {
                if (mask.getMaskAt(i, j))
                    newTex.SetPixel(i, j, sourceTexture.GetPixel(i, j));
            }
        }

        //make texture size lesser so that it fits in the smallest possible rectangle
        //adjustTextureSize()


        return newTex;
    }


    void Explode()
    {
        Texture2D tex = gameObject.GetComponent<SpriteRenderer>().sprite.texture;
        Debug.Log(tex.isReadable);

        Texture2D newTex = new Texture2D(32, 64);
        Texture2D newTex2 = new Texture2D(32, 64);
        
        for (int i = 0; i < tex.width; i++)
        {
            for(int j = 0; j < tex.height; j++)
            {
                if (i < 32)
                    newTex.SetPixel(i, j, tex.GetPixel(i, j));
                else
                    newTex2.SetPixel(i - 32, j - 32, tex.GetPixel(i, j));

            }
        }


        newTex.Apply();
        newTex2.Apply();
        
        
        Sprite anotherSprite = Sprite.Create(newTex, new Rect(0.0f, 0.0f, newTex.width, newTex.height), new Vector2(0.5f, 0.5f), 100.0f);
        Sprite anotherSprite2 = Sprite.Create(newTex2, new Rect(0.0f, 0.0f, newTex2.width, newTex2.height), new Vector2(0.5f, 0.5f), 100.0f);


        GameObject fragment = Instantiate(fragmentPrefab);
        GameObject fragment2 = Instantiate(fragmentPrefab);

        fragment.transform.position = transform.position;
        fragment2.transform.position = transform.position;

        fragment.GetComponent<SpriteRenderer>().sprite = anotherSprite;
        fragment2.GetComponent<SpriteRenderer>().sprite = anotherSprite2;


    }
}
