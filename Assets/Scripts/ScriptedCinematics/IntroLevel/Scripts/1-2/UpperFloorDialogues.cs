using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpperFloorDialogues : MonoBehaviour
{
    public Dialogue upperFloorZombieEntryDialogue;
    public Dialogue instaKill1Dialogue;

    public DialogueManager dialogueManager;

    public void TriggerUpperFloorZombieEntryDialogue()
    {
        dialogueManager.StartDialogue(upperFloorZombieEntryDialogue);
    }

    public void TriggerInstakill1Dialogue()
    {
        dialogueManager.StartDialogue(instaKill1Dialogue);
    }
}
