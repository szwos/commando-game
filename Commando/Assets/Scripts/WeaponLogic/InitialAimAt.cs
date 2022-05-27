using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialAimAt : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        GameObject targetObject = GameObject.FindWithTag("Player");

        if (targetObject != null)
        {
            transform.LookAt(targetObject.transform);


            Vector3 targetPosition = targetObject.transform.position;
            Vector3 sourcePosition = transform.position;
            targetPosition.x = targetPosition.x - sourcePosition.x;
            targetPosition.y = targetPosition.y - sourcePosition.y;

            float angle = Mathf.Atan2(targetPosition.y, targetPosition.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));


        }
    }
}
