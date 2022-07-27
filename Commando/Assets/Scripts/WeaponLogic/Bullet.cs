using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody2D rb;
    public List<string> targetedTags;

    public float timeout = 60;
    public float speed = 20f;
    public int damage = 20;
    public bool DestroyOnImpact = true;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;

    }

    // Update is called once per frame
    void Update()
    {

        timeout -= 1f * Time.deltaTime;

        if (timeout < 0)
        {
            Destroy(gameObject);
        }
    }


    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        
        if( targetedTags.Contains(collider2D.gameObject.tag) )
        {
            Health enemy = collider2D.GetComponent<Health>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

        }

        //TODO i think more elegant solution is possible
        if (collider2D.tag != "Bullet")
            if (DestroyOnImpact)
                Destroy(gameObject);


    }

}


