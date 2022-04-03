using UnityEngine;
using System.Collections;
using TMPro;
public class PistolGun : MonoBehaviour
{
    public int damage = 10;
    public float range = 100f;
    [SerializeField] private AudioSource shootSFX, reloadSFX, noAmmoSFX;
    [SerializeField] private Camera fpsCam;
    [SerializeField] private ParticleSystem bulletImpact1;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject bulletImpact;
    //[SerializeField] private float impactForce = 30f;

    public int maxAmmo = 12;
    public int currentAmmo;
    public int currentCarryingAmmo;
    public int maxCarryingAmmo = 50;
    public float reloadTime = 1f;

    [SerializeField] TMP_Text currentAmmoText;
    [SerializeField] TMP_Text currentCarryingAmmoText;

    public bool isReloading;

    private void OnEnable()
    {
        isReloading = false;
        updateAmmoUI();
    }

    public void Shoot()
    {
        if(currentAmmo > 0)
        {
            shootSFX.Play();
            currentAmmo--;
            updateAmmoUI();
            animator.SetTrigger("ShootTrigger");
            bulletImpact1.Play();
            RaycastHit hit;

            //If ray hits something
            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range, LayerMask.NameToLayer("Player")))
            {
                ShootableObject target = hit.transform.GetComponent<ShootableObject>();

                if (target != null)
                {
                    target.ShotReaction(damage);
                }

                Instantiate(bulletImpact, hit.point, Quaternion.LookRotation(hit.normal));
            }
        }
        else
        {
            //No ammo
            noAmmoSFX.Play();
        }
    }
    public void Reload()
    {
        if(!isReloading && currentCarryingAmmo > 0 &&currentAmmo < maxAmmo)
        {
            reloadSFX.Play();
            isReloading = true;
            StartCoroutine(ReloadCo());
        }
        else
        {
            //cant reload/no need to reload
        }
    }

    IEnumerator ReloadCo()
    {
        animator.SetTrigger("Reload");
        yield return new WaitForSeconds(reloadTime);

        //if totalammo itself have enough bullets
        if (currentCarryingAmmo >= maxAmmo)
        {
            int neededAmmo = maxAmmo - currentAmmo;
            currentCarryingAmmo -= neededAmmo;
            currentAmmo += neededAmmo;
        }

        //if total ammo is less than the mag size
        else if (currentCarryingAmmo > 0)
        {
            int neededAmmo = maxAmmo - currentAmmo;
            if (neededAmmo < currentCarryingAmmo)
            {
                currentCarryingAmmo -= neededAmmo;
                currentAmmo += neededAmmo;
            }
            else
            {
                currentAmmo += currentCarryingAmmo;
                currentCarryingAmmo = 0;
            }
        }

        updateAmmoUI();
        isReloading = false;
    }

    public void updateAmmoUI()
    {
        currentAmmoText.text = currentAmmo + " / " + maxAmmo;
        currentCarryingAmmoText.text = currentCarryingAmmo.ToString();
    }

    public void Refillammo()
    {
        currentCarryingAmmo = maxCarryingAmmo;
        updateAmmoUI();
    }
}
