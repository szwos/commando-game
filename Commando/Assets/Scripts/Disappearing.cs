using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disappearing : MonoBehaviour
{
    public float duration = 5;

    private float timer;
    private SpriteRenderer spr;
    private Color color;

    private void Start()
    {
        spr = gameObject.GetComponent<SpriteRenderer>();
        timer = duration;
    }

    void Update()
    {
        if (timer <= 0)
            Destroy(gameObject);

        color = new Color(1, 1, 1, timer/duration);
        

        spr.color = color;

        timer -= Time.deltaTime;

    }
}
