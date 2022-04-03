using UnityEngine;
using Cinemachine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using System.Collections;
public class DownStairsAreaCinematicManager : MonoBehaviour
{
    [SerializeField] private ThirdPersonCC cc;
    [SerializeField] private GameObject movementJoystick, sneakButton, runButton, interactionButton, Zt1, Zt1CameraTrack, PlayerCameraTrack;
    [SerializeField] private CinemachineVirtualCamera InstaKillCVM, InstaKillCVM2;
    [SerializeField] private GameObject EmanGO, spotForKill, spotForKill2;
    [SerializeField] private PlayableDirector FirstZombieinstaKillDirector, SecondZombieinstaKillDirector;
    [SerializeField] private DownStairsDialogues downStairsDialogues;
    [SerializeField] ZT1Controller firstZombie, SecondZombie;
    bool killTriggered = false;

    bool instaKillDialogueBreak = false;

    private void Start()
    {
        entryDialogue();
    }

    private void Update()
    {
        if (instaKillDialogueBreak)
        {
            if (downStairsDialogues.DownStairsinstaKillDialogue.dialogueFinished)
            {
                instaKillDialogueBreak = false;
                EndInstaKillCinematic();
            }
        }
    }

    //Things that need to be disabled during cinematic
    void DisablePlayer()
    {
        cc.canMove = false;
        sneakButton.SetActive(false);
        runButton.SetActive(false);
        movementJoystick.SetActive(false);
        interactionButton.SetActive(false);
        cc.cancelMovement();
    }

    //Things that need to be enabled to give control back to player
    void EnablePlayer()
    {
        cc.canMove = true;
        sneakButton.SetActive(true);
        runButton.SetActive(true);
        movementJoystick.SetActive(true);
    }

    public void entryDialogue()
    {
        DisablePlayer();
        StartCoroutine("StartEntryDialogue");
    }

    IEnumerator StartEntryDialogue()
    {
        yield return new WaitForSeconds(2f);
        downStairsDialogues.TriggerEntryDialogue();
        StartCoroutine("EntryDialogueBreak");
    }
    IEnumerator EntryDialogueBreak()
    {
        while (!downStairsDialogues.EntryDialogue.dialogueFinished)
        {
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);
        EnablePlayer();
    }

    // ---------------------1 1stZombieInstakill1Animation-----------------------------
    public void TriggerFirstZombieInstaKillCinematic()
    {
        if(!killTriggered)
        {
            killTriggered = true;
            SecondZombie.FullyStopZombie();
            Debug.Log("Playing FirstZombieInstakill cinematic");
            DisablePlayer();
            InstaKillCVM.Priority = 100;
            EmanGO.transform.localPosition = spotForKill.transform.position;
            EmanGO.transform.localEulerAngles = spotForKill.transform.eulerAngles;
            FirstZombieinstaKillDirector.Play();
        }
    }

    public void instaKillDialogueTrigger()
    {
        downStairsDialogues.TriggerDownStairsInstaKillDialogue();
        instaKillDialogueBreak = true;
    }

    public void EndInstaKillCinematic()
    {
        StartCoroutine("restartScene");
    }

    IEnumerator restartScene()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("DownStairArea");
    }
    // ---------------------End-----------------------------

    // ---------------------1 2ndZombieInstakill1Animation-----------------------------
    public void TriggerSecondZombieInstaKillCinematic()
    {
        if (!killTriggered)
        {
            killTriggered = true;
            firstZombie.FullyStopZombie();
            Debug.Log("Playing FirstZombieInstakill cinematic");
            DisablePlayer();
            InstaKillCVM2.Priority = 100;
            EmanGO.transform.localPosition = spotForKill2.transform.position;
            EmanGO.transform.localEulerAngles = spotForKill2.transform.eulerAngles;
            SecondZombieinstaKillDirector.Play();
        }
    }
    // ---------------------End---------------------------------------------------------
}
