using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroLevelDialogues : MonoBehaviour
{
    public Dialogue introLevelStartingDialogue;
    public Dialogue introLevelDeadBody1Dialogue;
    public Dialogue introLevelDeadBody2Dialogue;
    public Dialogue introLevelDoorBeforeDialogue;
    public Dialogue introLevelDoorAfterDialogue;

    public DialogueManager dialogueManager;

    public void TriggerintroLevelStartingDialogue()
    {
        dialogueManager.StartDialogue(introLevelStartingDialogue);
    }
    public void TriggerintroLevelDeadBody1Dialogue()
    {
        dialogueManager.StartDialogue(introLevelDeadBody1Dialogue);
    }
    public void TriggerintroLevelDeadBody2Dialogue()
    {
        dialogueManager.StartDialogue(introLevelDeadBody2Dialogue);
    }
    public void TriggerintroLevelDoorBeforeDialogue()
    {
        dialogueManager.StartDialogue(introLevelDoorBeforeDialogue);
    }
    public void TriggerintroLevelDoorAfterDialogue()
    {
        dialogueManager.StartDialogue(introLevelDoorAfterDialogue);
    }

}
