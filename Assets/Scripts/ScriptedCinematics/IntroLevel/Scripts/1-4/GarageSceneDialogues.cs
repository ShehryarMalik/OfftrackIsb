using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarageSceneDialogues : MonoBehaviour
{
    public Dialogue GarageEntryDialogue, GarageEntryDialogue2, QuickTimeFailDialogue, QuickTimePassDialogue, WavesClearDialogue;
    public Dialogue GeneralDeathDialogue;

    public DialogueManager dialogueManager;

    public void TriggerGeneralDeathDialogue()
    {
        dialogueManager.StartDialogue(GeneralDeathDialogue);
    }

    public void TriggerGarageEntryDialogue()
    {
        dialogueManager.StartDialogue(GarageEntryDialogue);
    }

    public void TriggerGarageEntryDialogue2()
    {
        dialogueManager.StartDialogue(GarageEntryDialogue2);
    }
    public void TriggerQuickTimeFailDialogue()
    {
        dialogueManager.StartDialogue(QuickTimeFailDialogue);
    }
    public void TriggerQuickTimePassDialogue()
    {
        dialogueManager.StartDialogue(QuickTimePassDialogue);
    }
    public void TriggerWaveClearDialogue()
    {
        dialogueManager.StartDialogue(WavesClearDialogue);
    }
}
