using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToUpstairs : InteractableObject
{
    private GameObject PassValuesGameObject;
    private PassValuesObject passValues;

    [SerializeField] private PistolGun gun;
    [SerializeField] private ThirdPersonCC characterCC;

    private void Start()
    {
        PassValuesGameObject = GameObject.Find("PassValues");

        if (PassValuesGameObject != null)
        {
            passValues = PassValuesGameObject.GetComponent<PassValuesObject>();
        }
    }

    public override string GetDescription()
    {
            return "Go Upstairs";
    }

    public override void Interact()
    {
        if(passValues != null)
            passValues.loadAmmoAndHealthValues(gun.currentAmmo, gun.currentCarryingAmmo, characterCC.getCurrentHealth());
        
        SceneManager.LoadScene("BackUpperFloorArea");
    }
}
