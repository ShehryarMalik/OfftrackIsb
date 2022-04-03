using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownStairsKey : InteractableObject
{
    [SerializeField] private DownStairsDoorInteraction downstairDoor;
    [SerializeField] CharacterCollisionInteraction cci;
    public override string GetDescription()
    {
        return "Key to Front Door";
    }

    public override void Interact()
    {
        downstairDoor.haveKey = true;
        cci.resetCurrentInterable();
        this.gameObject.SetActive(false);
    }
}
