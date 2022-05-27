using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    public GameObject player;


    // Update is called once per frame
    void Update()
    {
        //Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        //difference.Normalize();

        //float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);



        //gun following mouse
        Vector3 mousePos = Input.mousePosition;
        Vector3 gunPos = Camera.main.WorldToScreenPoint(transform.position);
        mousePos.x = mousePos.x - gunPos.x;
        mousePos.y = mousePos.y - gunPos.y;

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x < transform.position.x)
        {
            transform.rotation = Quaternion.Euler(new Vector3(180f, 0f, -angle));
        } else 
        {
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        }




        //swapping player left/right
        Vector3 mousePosInWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float rotationY = 0f;
        if (mousePosInWorld.x < player.transform.position.x)
        {
            rotationY = 180f;
        }

        player.transform.eulerAngles = new Vector3(player.transform.rotation.x, rotationY, player.transform.rotation.z);



        /*
        //if mouse is on left side of screen, rotate gun and player
        if (rotationZ < -90 || rotationZ > 90)
        { 

            if (player.transform.eulerAngles.y == 0)
            {
                transform.localRotation = Quaternion.Euler(180f, 0f, -rotationZ);
            }
            else if (player.transform.eulerAngles.y == 180)
            {
                transform.localRotation = Quaternion.Euler(180f, 180f, -rotationZ);
            }

        }*/
    }
}
