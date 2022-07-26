using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("BOOM!");
        //deal dmg to enemies in radius
    }


    void EndAnimation ()
    {
        Destroy(gameObject);
    }
}
