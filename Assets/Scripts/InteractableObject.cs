using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableObject : MonoBehaviour
{
    public enum InteractionType
    {
        Click
    }

    public InteractionType interactionType;
    public bool canInteract;

    public abstract string GetDescription();
    public abstract void Interact();

}
