using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door1_1Interaction : InteractableObject
{
    [SerializeField] private Introlevel_Srt1 introLevel_st1;

    public bool canOpen;
    public override string GetDescription()
    {
        return "Door";
    }

    public override void Interact()
    {
        Debug.Log("Interacting with door");

        if(canOpen)
        {
            introLevel_st1.triggerDoorAfterInteraction();
        }

        else
        {
            introLevel_st1.doorBeforeInteraction();
        }
    }
}
