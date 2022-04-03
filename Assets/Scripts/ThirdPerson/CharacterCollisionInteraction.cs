using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCollisionInteraction : MonoBehaviour
{
    //[SerializeField] private LayerMask interactableLayer;
    [SerializeField] private TMPro.TextMeshProUGUI interactionText;
    //[SerializeField] private bool interacting;
    [SerializeField] private GameObject interactButton, interactionTextGO;
    [SerializeField] private InteractableObject currentInteractable;

    public void resetCurrentInterable()
    {
        interactButton.SetActive(false);
        interactionTextGO.SetActive(false);
        interactionText.text = "";
        currentInteractable = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("InteractableLayer"))
        {
            InteractableObject interactable = other.GetComponent<InteractableObject>();
            
            if (interactable != null && interactable.canInteract)
            {
                interactionText.text = interactable.GetDescription();
                //interacting = true;
                interactButton.SetActive(true);
                interactionTextGO.SetActive(true);
                currentInteractable = interactable;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("InteractableLayer"))
        {
            interactButton.SetActive(false);
            interactionTextGO.SetActive(false);
            interactionText.text = "";
            currentInteractable = null;
        }
    }

    public void HandleInteraction()
    {
        if (currentInteractable != null)
        {
            switch (currentInteractable.interactionType)
            {
                case InteractableObject.InteractionType.Click:
                    currentInteractable.Interact();

                    break;
                
                default:
                    throw new System.Exception("Unsupported type of interactable.");
            }
        }

    }
}
