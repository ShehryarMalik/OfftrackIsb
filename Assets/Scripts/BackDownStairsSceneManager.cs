using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackDownStairsSceneManager : SceneManagable
{
    public GameObject PassValuesGameObject;
    private PassValuesObject passValues;

    [SerializeField] private PistolGun gun;
    [SerializeField] ThirdPersonCC characterCC;
    [SerializeField] private HealthBar healthBar;

    private void Start()
    {
        PassValuesGameObject = GameObject.Find("PassValues");

        if(PassValuesGameObject != null)
        {
            passValues = PassValuesGameObject.GetComponent<PassValuesObject>();
        }

        gun.currentAmmo = passValues.currentAmmo;
        gun.currentCarryingAmmo = passValues.currentCarryingAmmo;
        characterCC.currentHealth = passValues.currentHealth;
        healthBar.setHealth(passValues.currentHealth);
        gun.updateAmmoUI();
        
    }

    [SerializeField] float timeBeforeLoadingScene = 4f;
    public override void characterDeath()
    {
        Invoke(nameof(restartScene), timeBeforeLoadingScene);
    }

    void restartScene()
    {
        SceneManager.LoadScene("BackDownStairArea");
    }
}
