using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
public class LastSceneCinematicManager : MonoBehaviour
{
    [SerializeField] private PlayableDirector LC1, LC2;
    [SerializeField] private LastSceneDialogues dialogueManager;
    [SerializeField] private GameObject blackFiller, animationCanvas;
    [SerializeField] private AudioSource BGM;
    public void startLastCinematic()
    {
        LC1.Play();
    }

    //Signal reciever
    public void TriggerLC1Dialogue()
    {
        dialogueManager.TriggerLC1Dialogue1();
        StartCoroutine("LC1DialogueBreak");
    }
    IEnumerator LC1DialogueBreak()
    {
        while (!dialogueManager.LC1Dialogue1.dialogueFinished)
        {
            yield return null;
        }

        yield return new WaitForSeconds(1);
        LC2.Play();
    }

    public void TriggerLC2Dialogue()
    {
        dialogueManager.TriggerLC2Dialogue();
        StartCoroutine("LC2DialogueBreak");
    }
    IEnumerator LC2DialogueBreak()
    {
        while (!dialogueManager.LC2Dialogue.dialogueFinished)
        {
            yield return null;
        }

        BGM.Stop();
        blackFiller.SetActive(true);
        yield return new WaitForSeconds(2f);
        animationCanvas.SetActive(true);
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Level1Completion");
    }

}
