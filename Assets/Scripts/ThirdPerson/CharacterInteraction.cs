using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInteraction : MonoBehaviour
{
    //private void OnTriggerEnter(Collider other)
    //{
    //    Debug.Log(other.gameObject.name);
    //}

    [SerializeField] private Camera cam;
    [SerializeField] private float interactionDistance;
    [SerializeField] private TMPro.TextMeshProUGUI interactionText;
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private GameObject interactButton;
    [SerializeField] private InputManager inputManager;

    private InteractableObject currentInteractable;
    private void Update()
    {
        Ray ray = cam.ScreenPointToRay(new Vector3((Screen.width / 2f) + 5, Screen.height / 2f, 0f));
        RaycastHit hit;

        Debug.DrawRay(ray.origin,ray.direction * interactionDistance,Color.green);

        bool successfulHit = false;

        if (Physics.Raycast(ray, out hit, interactionDistance, interactableLayer))
        {
            InteractableObject interactable = hit.collider.GetComponent<InteractableObject>();

            //found an interactable
            if (interactable != null)
            {
                interactionText.text = interactable.GetDescription();
                successfulHit = true;
                interactButton.SetActive(true);
                currentInteractable = interactable;
            }
        }

        // if we miss, hide the UI
        if (!successfulHit)
        {
            interactButton.SetActive(false);
            interactionText.text = "";
            currentInteractable = null;
        }
    }

    public void HandleInteraction()
    {
        if(currentInteractable != null)
        {
            switch (currentInteractable.interactionType)
            {
                case InteractableObject.InteractionType.Click:
                    currentInteractable.Interact();

                    break;

                //case Interactable.InteractionType.Hold:
                //    if (Input.GetKey(key))
                //    {
                //        // we are holding the key, increase the timer until we reach 1f
                //        interactable.IncreaseHoldTime();
                //        if (interactable.GetHoldTime() > 1 f) {
                //            interactable.Interact();
                //            interactable.ResetHoldTime();
                //        }
                //    }
                //    else
                //    {
                //        interactable.ResetHoldTime();
                //    }
                //    interactionHoldProgress.fillAmount = interactable.GetHoldTime();
                //    break;
                //// here is started code for your custom interaction :)
                //case Interactable.InteractionType.Minigame:
                //    // here you make ur minigame appear
                //    break;

                // helpful error for us in the future
                default:
                    throw new System.Exception("Unsupported type of interactable.");
            }
        }
        
    }

}
