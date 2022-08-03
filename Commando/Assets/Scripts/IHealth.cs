using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealth
{

    //TODO having multiple colliders on an entity causes it to take dmg multiple times
    //fix a) cooldown for taking damage (few miliseconds, like in minecraft)
    //fix b) stop dealing damage using colliders (kind of limiting approach)
    //fix c) disable colliders GetComponent<Collider>().enabled = false and enable them in next frame / after timeout


    void TakeDamage(int damage);
    void Die();
}
