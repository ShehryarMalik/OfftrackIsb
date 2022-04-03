using UnityEngine;
using UnityEngine.SceneManagement;

public class ToDownStairsTrigger : InteractableObject
{
    public bool canGo;
    [SerializeField] ZT1Controller zt1Controller;

    private void Update()
    {
        canGo = !zt1Controller.targetFound;
    }

    public override string GetDescription()
    {
        if (canGo)
            return "Go downstairs";
        else
            return "";
    }

    public override void Interact()
    {
        Debug.Log("Interacting with door");

        if (canGo)
        {
            SceneManager.LoadScene("DownStairArea");
        }
    }
}
