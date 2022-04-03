using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Playables;
using Cinemachine;
public class Lv1CompletionCinematics : MonoBehaviour
{
    [SerializeField] private Lv1CompletionDialogues dialogues;
    [SerializeField] private PlayableDirector c1, c2, c3, c4, c5, c6, c7, c8, c9, c10, c11;
    [SerializeField] private GameObject emanNormal, emanGreenEye1st, blackFiller;
    [SerializeField] private CinemachineVirtualCamera cvm2nd;
    [SerializeField] AudioSource roomAmbiance, tenseLoop, distortion;
    public void start1stDialogue()
    {
        dialogues.Triggerl1c1Dialogue();
        StartCoroutine("FirstDialogueBreak");
    }

    IEnumerator FirstDialogueBreak()
    {
        while (!dialogues.l1c1Dialogue.dialogueFinished)
        {
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);
        c2.Play();
        c1.Stop();
    }

    public void start2stDialogue()
    {
        dialogues.Triggerl1c2Dialogue();
        StartCoroutine("SecondDialogueBreak");
        roomAmbiance.Stop();
    }

    IEnumerator SecondDialogueBreak()
    {
        while (!dialogues.l1c2Dialogue.dialogueFinished)
        {
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);
        c3.Play();
        c2.Stop();
    }

    public void start3rdDialogue()
    {
        dialogues.Triggerl1c3Dialogue();
        StartCoroutine("ThirdDialogueBreak");
    }

    IEnumerator ThirdDialogueBreak()
    {
        while (!dialogues.l1c3Dialogue.dialogueFinished)
        {
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);
        c4.Play();
        c3.Stop();
    }

    public void start4thDialogue()
    {
        dialogues.Triggerl1c4Dialogue();
        StartCoroutine("FourthDialogueBreak");
    }

    IEnumerator FourthDialogueBreak()
    {
        while (!dialogues.l1c4Dialogue.dialogueFinished)
        {
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);
        emanNormal.SetActive(false);
        c5.Play();
        c4.Stop();
    }

    public void start5thDialogue()
    {
        dialogues.Triggerl1c5Dialogue();
        StartCoroutine("FifthDialogueBreak");
    }

    IEnumerator FifthDialogueBreak()
    {
        while (!dialogues.l1c5Dialogue.dialogueFinished)
        {
            yield return null;
        }
        blackFiller.SetActive(true);
        yield return new WaitForSeconds(2f);
        start6thDialogue();
    }

    public void start6thDialogue()
    {
        dialogues.Triggerl1c6Dialogue();
        StartCoroutine("SixthDialogueBreak");
    }

    IEnumerator SixthDialogueBreak()
    {
        while (!dialogues.l1c6Dialogue.dialogueFinished)
        {
            yield return null;
        }
        blackFiller.SetActive(false);
        tenseLoop.Play();
        distortion.Play();
        c6.Play();
        c5.Stop();
    }

    public void start62Dialogue()
    {
        dialogues.Triggerl1c62Dialogue();
        StartCoroutine("SixtwoDialogueBreak");
    }

    IEnumerator SixtwoDialogueBreak()
    {
        while (!dialogues.l1c62Dialogue.dialogueFinished)
        {
            yield return null;
        }
        c7.Play();
        c6.Stop();
    }

    public void start7thDialogue()
    {
        dialogues.Triggerl1c7Dialogue();
        StartCoroutine("SeventhDialogueBreak");
    }

    IEnumerator SeventhDialogueBreak()
    {
        while (!dialogues.l1c7Dialogue.dialogueFinished)
        {
            yield return null;
        }
        c8.Play();
        c7.Stop();
    }

    public void start8thDialogue()
    {
        dialogues.Triggerl1c8Dialogue();
        StartCoroutine("EighthDialogueBreak");
        tenseLoop.Stop();
        distortion.Stop();
    }

    IEnumerator EighthDialogueBreak()
    {
        while (!dialogues.l1c8Dialogue.dialogueFinished)
        {
            yield return null;
        }
        emanNormal.SetActive(true);
        emanGreenEye1st.SetActive(false);
        c9.Play();
        cvm2nd.Priority = 10;
        c8.Stop();
    }

    public void start9thDialogue()
    {
        dialogues.Triggerl1c9Dialogue();
        StartCoroutine("NinthhDialogueBreak");
    }

    IEnumerator NinthhDialogueBreak()
    {
        while (!dialogues.l1c9Dialogue.dialogueFinished)
        {
            yield return null;
        }
        c10.Play();
        c9.Stop();
    }

    public void start10thDialogue()
    {
        dialogues.Triggerl1c10Dialogue();
        StartCoroutine("TenthDialogueBreak");
    }
    IEnumerator TenthDialogueBreak()
    {
        while (!dialogues.l1c10Dialogue.dialogueFinished)
        {
            yield return null;
        }
        yield return new WaitForSeconds(2f);
        blackFiller.SetActive(true);
        yield return new WaitForSeconds(2f);
        c11.Play();
        blackFiller.SetActive(false);
        c10.Stop();
    }

    public void start11thDialogue()
    {
        dialogues.Triggerl1c11Dialogue();
        StartCoroutine("EleventhDialogueBreak");
    }
    IEnumerator EleventhDialogueBreak()
    {
        while (!dialogues.l1c11Dialogue.dialogueFinished)
        {
            yield return null;
        }
        Debug.Log("The end");
        saveGame();
        SceneManager.LoadScene("LevelSelectorScene");
    }

    public void saveGame()
    {
        SaveSystem.saveGame(1);
    }

}
