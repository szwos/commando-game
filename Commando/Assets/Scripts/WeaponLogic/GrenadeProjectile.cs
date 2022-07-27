using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeProjectile : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject explosionPrefab;
    public List<string> targetedTags;

    [Tooltip("set to 0 if player is using it")]
    public float verticalSpeed = 10;
    public float throwSpeed = 10;
    public float fuseTime = 4;
    public bool destroyOnImpact = true;


    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * throwSpeed;
        rb.AddForce(new Vector2(0, verticalSpeed));
    }

    // Update is called once per frame
    void Update()
    {
        fuseTime -= 1f * Time.deltaTime;

        if (fuseTime < 0)
        {
            Explode();
        }

    }

    void Explode()
    {

        //Identity Quaternion - no rotation
        Instantiate(explosionPrefab, transform.position, new Quaternion(1, 0, 0, 0));

        //deal damage to entities described by selected layers (or tags idk, tags propably better) in certain range
        //Destroy(gameobject);

        Destroy(gameObject);

    }

    
    private void  OnCollisionEnter2D(Collision2D collision2D)
    {
        if (destroyOnImpact)
        {
            if (targetedTags.Contains(collision2D.gameObject.tag))
            {
                Explode();
            }
        }
    }
}
