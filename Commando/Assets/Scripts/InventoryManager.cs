using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;




// TODO Spaghetti code, refactor
//TODO do i need MonoBehaviour??
public partial class InventoryManager : MonoBehaviour
{

    private class AmmoType
    {
        public AmmoType(EAmmoType ID, int ammoAmount)
        {
            this.ID = ID;
            this.ammoAmount = ammoAmount;
        }
        public EAmmoType ID { get; set; }
        public int ammoAmount { get; set; } = 0;

    }


    public Text ammunitionText;

    private static AmmoType[] ammoTypes = {
        new AmmoType(EAmmoType.low_caliber, 20),
        new AmmoType(EAmmoType.shotgun_shells, 50),
        new AmmoType(EAmmoType.high_caliber, 5), 
        new AmmoType(EAmmoType.grenade, 10)
    };


    
    private void Start()
    {

    }

    public static int getClip(int clipSize, EAmmoType eammoType)
    {
        foreach(AmmoType ammo in ammoTypes)
        {
            if (ammo.ID == eammoType)
            {
                if (clipSize <= ammo.ammoAmount)
                {
                    ammo.ammoAmount -= clipSize;
                    return clipSize;
                }
                else
                {
                    int remainingAmmo = ammo.ammoAmount;
                    ammo.ammoAmount -= ammo.ammoAmount; //ammo = 0;
                    return remainingAmmo;

                }

            }
                
        }
        //foreach ended, no ammo found
        Debug.Log("missing ammo type in inventory, returning 0");
        return 0;

        //TODO instead of throwing away current clip and loading a new one, add missing ammo to current clip and subtract only this ammo from ammo in inventory
        
    }

    public static int getAmmo(EAmmoType eAmmo)
    {
        foreach (AmmoType ammo in ammoTypes)
        {
            if (ammo.ID == eAmmo)
                return ammo.ammoAmount;
        }

        //foreach ended, no ammo found
        Debug.Log("missing ammo type in inventory, returning 0");
        return 0;
    }



}