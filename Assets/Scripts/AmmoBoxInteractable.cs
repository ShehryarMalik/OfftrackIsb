using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBoxInteractable : InteractableObject
{
    [SerializeField] PistolGun pistol;
    private AudioSource pickSFX;

    private void Start()
    {
        pickSFX = GetComponentInChildren<AudioSource>();
    }
    public override string GetDescription()
    {
        return "Ammo Box";
    }

    public override void Interact()
    {
        pistol.Refillammo();
        if (pickSFX)
            pickSFX.Play();
    }
}
