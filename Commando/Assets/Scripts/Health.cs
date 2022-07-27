using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int health = 100;
    public GameObject deathEffect;
    public DialogueTrigger deathMessage; //TODO testing only


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage (int damage)
    {

        //TODO having multiple colliders on an entity causes it to take dmg multiple times
        //fix a) cooldown for taking damage (few miliseconds, like in minecraft)
        //fix b) stop dealing damage using colliders (kind of limiting approach)
        //fix c) disable colliders GetComponent<Collider>().enabled = false and enable them in next frame / after timeout

        //string msg = health.ToString() + " -> " + (health - damage).ToString();
        //Debug.Log(msg);

        health -= damage;


        if (health <= 0)
        {
            Die();
        }
    }

    void Die ()
    {
        //Instantiate(deathEffect, transform.position, Quaternion.identity);
        deathMessage.TriggerDialogue();
        Destroy(gameObject);
        
  

    }

}
