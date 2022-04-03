using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassValuesObject : MonoBehaviour
{
    private static bool created = false;
    public int currentAmmo, currentCarryingAmmo, currentHealth;

    private void Awake()
    {
        if(!created)
        {
          DontDestroyOnLoad(this.gameObject);
            created = true;
        }
    }

    public void loadAmmoAndHealthValues(int CurrentAmmo,int CurrentCarryingAmmo,int CurrentHealth)
    {
        currentAmmo = CurrentAmmo;
        currentCarryingAmmo = CurrentCarryingAmmo;
        currentHealth = CurrentHealth;
    }
}
