using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownStairsDialogues : MonoBehaviour
{
    public Dialogue DownStairsinstaKillDialogue, EntryDialogue;

    public DialogueManager dialogueManager;

    public void TriggerDownStairsInstaKillDialogue()
    {
        dialogueManager.StartDialogue(DownStairsinstaKillDialogue);
    }

    public void TriggerEntryDialogue()
    {
        dialogueManager.StartDialogue(EntryDialogue);
    }
}
