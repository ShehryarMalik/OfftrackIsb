using UnityEngine.SceneManagement;
using UnityEngine;

public class DownStairsDoorInteraction : InteractableObject
{
    public bool canOpen, haveKey;
    [SerializeField] GameObject keyEffect;

    public override string GetDescription()
    {
        if (canOpen)
        {
            if (haveKey)
                return "To Garage";
            else
                return "Locked";
        }
        else
            return "";
        
    }

    public override void Interact()
    {
        Debug.Log("Interacting with door");

        if (canOpen && haveKey)
        {
            SceneManager.LoadScene("GarageMap");
        }

        else
        {
            if (!keyEffect.activeSelf)
                keyEffect.SetActive(true);
        }
    }
}
