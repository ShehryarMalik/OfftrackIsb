using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GarageDoorExit : InteractableObject
{
    [SerializeField] private PassValuesObject passValuesGO;
    [SerializeField] private PistolGun gun;
    [SerializeField] private ThirdPersonCC characterCC;

    public bool canOpen = false;

    private void Start()
    {
        passValuesGO = GameObject.Find("PassValues").GetComponent<PassValuesObject>();
    }

    public override string GetDescription()
    {
        if (canOpen)
            return "Door";
        else
            return "";
    }

    public override void Interact()
    {
        if (canOpen)
        {
            passValuesGO.loadAmmoAndHealthValues(gun.currentAmmo,gun.currentCarryingAmmo,characterCC.getCurrentHealth());
            SceneManager.LoadScene("BackDownStairArea");
        }

        else
        {

        }
    }
}
