using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    
    public void open(Collider2D collider2D)
    {
        StartCoroutine(DisableCollisions(collider2D));
    }

    IEnumerator DisableCollisions(Collider2D collider2D)
    {
        Collider2D platformCollider = gameObject.GetComponent<BoxCollider2D>();

        Physics2D.IgnoreCollision(collider2D, platformCollider);
        yield return new WaitForSeconds(0.25f);
        Physics2D.IgnoreCollision(collider2D, platformCollider, false);
    }
}
