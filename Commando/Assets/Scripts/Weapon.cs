using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//ideas of cool reloading
//reload faster when pressed reload in right timing
//reload faster accoriding to number of enemies killed / bullets in the air (generally the more dynamic the game gets, the faster reloading)
//those ideas might relised as an aqquired perk (pickup / consumable / character scaling reward)


public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public GameObject punchPrefab;
    public SoundManager.Sound gunshotSound;
    public Text ammunitionText;

    public int clipSize = 10;
    public float fireRate = 15f;
    public float reloadTime = 1f; //adjust reload time to make it melodic, like shotgun is right now
    public bool autoFire = true;

    private float nextTimeToFire = 0f;
    private int currentAmmo;
    private bool inputFire = false;
    private bool isReloading = false;

    // Start is called before the first frame update
    void Start()
    {
        currentAmmo = clipSize;
    }


    private void OnEnable()
    {
        isReloading = false;
    }

    //TODO make this work somehow (ammo types, and taking their state from player's eq)
    /*public y.tube/jakWybracTypZmiennejZaPomocaMonoBehaviour ammoType;
    void OnEnable()
    {
        inventory = GetComponent<Inventory>();
        currentAmmo = inventory.getAmmo<ammoType>();
    }
    */


    // Update is called once per frame
    void Update()
    {
        if (isReloading)
            return;


        if (Input.GetButtonDown("Reload"))
            StartCoroutine(Reload());



        //TODO i think this can be simplified for the sake of cleanCode
        if (autoFire == false && Input.GetButtonDown("Fire1"))
        {
            if (currentAmmo <= 0)
            {
                StartCoroutine(Reload());
                return;
            }else 
                inputFire = true;

        }else if (autoFire == true && Input.GetButton("Fire1"))
        {

            if (currentAmmo <= 0)
            {
                StartCoroutine(Reload());
                return;
            }
            else
                inputFire = true;
        }
        else
            inputFire = false;


        if (inputFire && Time.time >= nextTimeToFire)
        {
            SoundManager.PlaySound(gunshotSound);
            ISpecialBehaviour special = GetComponent<ISpecialBehaviour>(); //TODO not sure if this isnt memory leak/concern, maybe i should move getComponent to some other place

            nextTimeToFire = Time.time + 1/fireRate;
            currentAmmo--;
            
            special.Shoot(bulletPrefab, firePoint);

            //TODO when shooting bigger weapon recoil pushes player in oposite direction the weapon is facing
            //playerRigidbody.velocity = new Vector2(10f*-Mathf.Cos(armTransform.rotation.z), 10f*Mathf.Cos(armTransform.rotation.z));

        }
        
        if (Input.GetButtonDown("Fire2"))
        {
            Instantiate(punchPrefab, firePoint.position, firePoint.rotation);
        }
    }

    //Reload should also be "special"
    IEnumerator Reload()
    {
        isReloading = true;

        
        //TODO play different reload sound for different weapon (remember, that weapons can share the same SpecialBehaviour, but can have other reload sounds)
        SoundManager.PlaySound(SoundManager.Sound.shotgun_reload);

        yield return new WaitForSeconds(reloadTime);
        //currentAmmo = clipSize;
        currentAmmo = 0 + InventoryManager.getAmmo(clipSize);

        
        isReloading = false;
    }

    private void OnGUI()
    {
        ammunitionText.text = currentAmmo.ToString() + "/" + clipSize.ToString();   
    }

}

