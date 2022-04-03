using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastSceneDialogues : MonoBehaviour
{
    public DialogueManager dialogueManager;

    public Dialogue LC1Dialogue1, LC2Dialogue;
    public void TriggerLC1Dialogue1()
    {
        dialogueManager.StartDialogue(LC1Dialogue1);
    }

    public void TriggerLC2Dialogue()
    {
        dialogueManager.StartDialogue(LC2Dialogue);
    }
}
