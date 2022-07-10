using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public bool horizontalScrolling = true;
    public bool smoothMovement = true;
    public float dampTime = 0.3f;

    //how much percent from boundaries of camera the player can be within untill camera will rearranged
    [Range(0, 1)]
    public float playerInRangeArea = 0.25f;

    public Transform playerTransform;
    private Camera cam;

    private float camHeight;
    private Vector3 dampVelocity = Vector3.zero;


    void Start()
    {

        //WARNING this won't work propely if camera is getting resized
        //getting height of camera in world coordinates
        Vector2 edgeVector = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        camHeight = edgeVector.y * 2;




        cam = transform.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(playerTransform.position.x, transform.position.y, transform.position.z);
        Vector3 screenPos = cam.WorldToScreenPoint(playerTransform.position);
        //Debug.Log(transform.position.y + cam.pixelHeight / 2);
        

        if(horizontalScrolling)
        {

            if (screenPos.y > cam.pixelHeight - cam.pixelHeight * playerInRangeArea)
            {
                if(smoothMovement)
                {
                    transform.position = Vector3.SmoothDamp(
                        transform.position, 
                        new Vector3(transform.position.x, transform.position.y + camHeight / 2, transform.position.z), 
                        ref dampVelocity, 
                        dampTime);
                } else
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y + camHeight / 2, transform.position.z);
                }
                
            }

            //if player is at lower 20% of screen, move camera to the bottom
            if (screenPos.y < 0 + cam.pixelHeight * playerInRangeArea)
            {
                if(smoothMovement)
                {
                    transform.position = Vector3.SmoothDamp(
                        transform.position, 
                        new Vector3(transform.position.x, transform.position.y - camHeight / 2, transform.position.z), 
                        ref dampVelocity, 
                        dampTime);
                }
                else
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y - camHeight / 2, transform.position.z);
                }
                
            }

        }


    }
}
