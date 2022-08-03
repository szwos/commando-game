using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public SoundManager.Sound explosionSound;
    public List<string> targetedTags;
    
    public int damage = 30;
    



    // Start is called before the first frame update
    void Start()
    {

        SoundManager.PlaySound(explosionSound, transform.position);
        //Debug.Log("BOOM!");
        //deal dmg to enemies in radius
    }


    void EndAnimation ()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        
        if( targetedTags.Contains(collider2D.gameObject.tag) )
        {
            IHealth enemy = collider2D.GetComponent<IHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

        }



    }

}
