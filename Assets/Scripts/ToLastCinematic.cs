using UnityEngine.SceneManagement;
public class ToLastCinematic : InteractableObject
{
    public override string GetDescription()
    {
        return "Door";
    }

    public override void Interact()
    {
        SceneManager.LoadScene("LastCinematicScene");
    }
}
