using UnityEngine.SceneManagement;
using UnityEngine;
using Cinemachine;
using UnityEngine.Playables;

public class UpperFloorAreaCinematics : MonoBehaviour
{
    [SerializeField] private GameObject movementJoystick, sneakButton, runButton, interactionButton,Zt1,Zt1CameraTrack,PlayerCameraTrack;
    [SerializeField] private ThirdPersonCC cc;
    [SerializeField] private CinemachineBrain camBrain;
    [SerializeField] private CinemachineVirtualCamera CVM,InstaKillCVM;
    [SerializeField] private PlayableDirector entryCinematicDirector,zombieEntryCinematicDirector,instaKillDirector;
    [SerializeField] private UpperFloorDialogues upperFloorDialogues;
    [SerializeField] private GameObject zombieEntryTrigger,EmanGO, spotForKill;
    [SerializeField] private ZT1Controller Zt1Controller;
    [SerializeField] private GameObject tutorial2;

    bool dialogueBreak1 = false, instaKillDialogueBreak = false;
    private void Update()
    {
        if (dialogueBreak1)
        {
            if (upperFloorDialogues.upperFloorZombieEntryDialogue.dialogueFinished)
            {
                dialogueBreak1 = false;
                EndzombieEntryCinematic();
            }
        }

        if (instaKillDialogueBreak)
        {
            if (upperFloorDialogues.instaKill1Dialogue.dialogueFinished)
            {
                instaKillDialogueBreak = false;
                EndInstaKillCinematic();
            }
        }
    }

    private void Start()
    {
        //scene starts with Entry cinematic
        EntryCinematic1();
    }

    // ---------------1-Entry Cinematic----------------------
    public void EntryCinematic1()
    {
        Debug.Log("Playing Cinematic: EntryCinematic 1");
        DisablePlayer();
        entryCinematicDirector.Play();
    }

    public void endEntryCinematic1()
    {
        EnablePlayer();
        Debug.Log("Ending Cinematic: EntryCinematic 1");
    }
    // ---------------------End-----------------------------

    // ---------------2-Zombie Entry----------------------
    public void ZombieEntryCinematic1()
    {
        Debug.Log("Playing Cinematic: Zombie entry 1");
        DisablePlayer();
        zombieEntryTrigger.SetActive(false);
        CVM.m_LookAt = Zt1CameraTrack.transform;
        zombieEntryCinematicDirector.Play();
    }

    public void ZombieEntryCinematic2()
    {
        CVM.m_LookAt = PlayerCameraTrack.transform;
        Debug.Log("Dialogue Break 1");
        dialogueBreak1 = true;
        upperFloorDialogues.TriggerUpperFloorZombieEntryDialogue();
        Zt1Controller.canMoveToPoints = false;
        Zt1Controller.StopAgent();
        Zt1Controller.isEating = true;
        Zt1Controller.canDetect = true;
        Zt1.transform.localPosition = new Vector3(8.70331001f, 1.47000003f, -20.7099991f);
        Zt1.transform.localRotation = Quaternion.Euler(0f, 79.778f, 0f);
    }

    public void EndzombieEntryCinematic()
    {
        Debug.Log("Ending Zombie entry cinematic");
        CVM.m_LookAt = null;
        zombieEntryCinematicDirector.Stop();
        tutorial2.SetActive(true);
        EnablePlayer();
    }
    // ---------------------End-----------------------------

    // ---------------------3Instakill1Animation-----------------------------
    public void TriggerInstaKillCinematic()
    {
        Debug.Log("Playing Instakill cinematic");
        DisablePlayer();
        InstaKillCVM.Priority = 100;
        EmanGO.transform.localPosition = spotForKill.transform.position;
        EmanGO.transform.localEulerAngles = spotForKill.transform.eulerAngles;
        instaKillDirector.Play();
    }

    public void instaKillDialogueTrigger()
    {
        upperFloorDialogues.TriggerInstakill1Dialogue();
        instaKillDialogueBreak = true;
    }

    public void EndInstaKillCinematic()
    {
        SceneManager.LoadScene("IntroLevelUpperFloorArea");
    }
    // ---------------------End-----------------------------


    //Things that need to be disabled during cinematic
    void DisablePlayer()
    {
        cc.canMove = false;
        sneakButton.SetActive(false);
        runButton.SetActive(false);
        movementJoystick.SetActive(false);
        interactionButton.SetActive(false);
        //camBrain.enabled = false;
        CVM.Priority = 50;
        cc.cancelMovement();
    }

    //Things that need to be enabled to give control back to player
    void EnablePlayer()
    {
        cc.canMove = true;
        sneakButton.SetActive(true);
        runButton.SetActive(true);
        movementJoystick.SetActive(true);
        camBrain.enabled = true;
        CVM.Priority = 1;
    }
}
