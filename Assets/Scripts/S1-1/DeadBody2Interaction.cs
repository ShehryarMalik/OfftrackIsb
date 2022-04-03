using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadBody2Interaction : InteractableObject
{
    [SerializeField] private Introlevel_Srt1 introLevel_st1;
    public override string GetDescription()
    {
        return "Deadbody";
    }

    public override void Interact()
    {
        Debug.Log("Interacting with Deadbody 2");
        introLevel_st1.deadBody2Interaction();
    }
}
