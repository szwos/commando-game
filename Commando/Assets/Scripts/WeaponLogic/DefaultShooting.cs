using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultShooting : MonoBehaviour, ISpecialBehaviour
{
    public void Shoot(GameObject bulletPrefab, Transform firePoint)
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
