using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    private static int ammo = 20;
    public Text ammunitionText;


    private void Start()
    {
        //ammunitionText = GetComponent<Text>();
    }

    public static int getAmmo(int clipSize)
    {
        
        //TODO instead of throwing away current clip and loading a new one, add missing ammo to current clip and subtract only this ammo from ammo in inventory
        if (clipSize <= ammo)
        {
            ammo -= clipSize;
            return clipSize;
        } else
        {
            int remainingAmmo = ammo;
            ammo -= ammo; //ammo = 0;
            return remainingAmmo;
            
        }
    }

    private void Update()
    {
        ammunitionText.text = ammo.ToString();
    }



}