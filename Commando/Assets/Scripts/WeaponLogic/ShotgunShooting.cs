using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunShooting : MonoBehaviour, ISpecialBehaviour
{
    public int numberOfBullets = 3;
    

    [Range(1, 90)]
    public float spread = 90f;


    public void Shoot(GameObject bulletPrefab, Transform firePoint)
    {

        for (int i = 0; i < numberOfBullets; i++)
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation * RandomRotation());
        
        
    }

    
    private Quaternion RandomRotation()
    {
        return Quaternion.Euler(Random.Range(0.0f, 0.0f), Random.Range(0.0f, 0.0f), Random.Range(-spread, spread)); ;
    }
}
