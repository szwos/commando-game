using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoldier : MonoBehaviour, IHealth
{

    //layerMasks
    public LayerMask targetLayer;
    
    //variables
    public float speed = 10f;
    public float attackCooldown = 5f;
    public float shootProximity = 5f;
    public float punchProximity = 1f;
    public int health = 100;
    private bool Awake = false;

    //classes
    //public CharacterController2D controller2D; when it will be actually usefull i wil use Controller2D
    public FieldOfView fov;
    public DialogueTrigger deathMessage;

    //objects
    public Transform playerPos; 
    public Transform firePoint;

    public Rigidbody2D rb;

    public GameObject fistPrefab;
    public GameObject bulletPrefab;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(fov.CanSeePlayer)
        {
            Awake = true;
        }



        //idle
        if (!Awake)
        {
            //zzz animation and fixed update still looks for player in fov
            
        }
        else //start combat, player has been spotted
        {

            //flip Solider in direction of player
            float rotationY = 180f;
            if (transform.position.x < playerPos.transform.position.x)
            {
                rotationY = 0f;
            }
            transform.eulerAngles = new Vector3(transform.rotation.x, rotationY, transform.rotation.z);


            Collider2D[] rangeCheck = Physics2D.OverlapCircleAll(transform.position, shootProximity, targetLayer);

            //if found any players in range
            if (rangeCheck.Length > 0)
            {
                if (Vector3.Distance(rangeCheck[0].transform.position, transform.position) < punchProximity)
                {
                    rb.velocity = new Vector2(0f, 0f);
                    Punch();
                } else
                {
                    rb.velocity = new Vector2(0f, 0f);
                    Shoot();
                }
            } else //no player in range
            {
                
                Follow();
            }
           
        }


    }
    
    private void Punch()
    {
        
        if(AttackCooldown())
        {
            //Debug.Log("punching");
            Instantiate(fistPrefab, firePoint.position, firePoint.rotation);
        }

    }

    private float lastTimeAttacked = 0f;
    //returns true when punch available
    private bool AttackCooldown()
    {
        

        if(lastTimeAttacked + attackCooldown < Time.time)
        {
            lastTimeAttacked = Time.time;
            return true;
        } return false;

    }

    private void Shoot()
    {
        if(AttackCooldown())
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }

    private void Follow()
    {
        
        //TODO no idea why, but this makes Enemy move veeery slow
        rb.velocity = transform.TransformDirection(new Vector2(speed * Time.deltaTime, 0));

        //controller2D.Move(speed * Time.deltaTime, false, false); when it will be actually usefull i wil use Controller2D
    }

    public void TakeDamage(int damage)
    {
        //TODO solve problem with taking multiple damage

        health -= damage;


        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        //Instantiate(deathEffect, transform.position, Quaternion.identity);
        deathMessage.TriggerDialogue();
        Destroy(gameObject);
    }

}


