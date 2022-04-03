using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadBody1Interaction : InteractableObject
{
    [SerializeField] private Introlevel_Srt1 introLevel_st1;
    public override string GetDescription()
    {
        return "DeadBody";
    }

    public override void Interact()
    {
        Debug.Log("Interacting with Deadbody 1");
        introLevel_st1.deadBody1Interaction();
    }
}
