using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Playables;
using Cinemachine;
using TMPro;
public class Introlevel_Srt1 : MonoBehaviour
{
    [SerializeField] private int StartingCinematic_01______;
    [SerializeField] int DramaticDelay;
    [SerializeField] private GameObject blackFiller;
    [SerializeField] private GameObject movementJoystick;
    [SerializeField] private int End_StartingCinematic_01______;
    [SerializeField] private IntroLevelDialogues introDialogues;
    [SerializeField] private PlayableDirector director, doorclosetCinematicDirector, doorclosetCinematic2Director;
    [SerializeField] private ThirdPersonCC cc;
    [SerializeField] private CinemachineBrain camBrain;
    [SerializeField] private GameObject tutorial1, objectives, interactionButtonGO, backButtonGO, interactionTextGO;
    [SerializeField] private CinemachineVirtualCamera deadbody1InspectionCamera, deadbody2InspectionCamera;
    [SerializeField] private DeadBody2Interaction deadbody2InteractionGO;
    [SerializeField] private Door1_1Interaction doorInteractionGO;
    [SerializeField] private TextMeshProUGUI objectivesText;
    public AudioSource BGM;
    private bool break1 = false;
    private bool deadBody1InteractionBreak = false;
    private bool deadBody2InteractionBreak = false;
    private bool doorBeforeBreak = false;
    private bool doorAfterBreak = false;
    [SerializeField] private GameObject zombie1InteractiveMark, zombie2InteractiveMark, doorExitEffect;
    public void TriggerStartingCinematic_01()
    {
        StartCoroutine("StartingCinematic_01");
    }

    public void startBGMusic()
    {
        if (BGM)
            BGM.Play();
    }

    public void endBGMusic()
    {
        if(BGM)
        StartCoroutine(FadeOutSound.FadeOut(BGM, 1));
    }

    private void Update()
    {
        if (break1)
        {
            if (introDialogues.introLevelStartingDialogue.dialogueFinished)
            {
                break1 = false;
                StartCoroutine("StartingCinematic_02");
            }
        }

        if(deadBody1InteractionBreak)
        {
            if (introDialogues.introLevelDeadBody1Dialogue.dialogueFinished)
            {
                deadBody1InteractionBreak = false;
                endDeadbody1Intraction();
            }
        }

        if (deadBody2InteractionBreak)
        {
            if (introDialogues.introLevelDeadBody2Dialogue.dialogueFinished)
            {
                deadBody2InteractionBreak = false;
                endDeadbody2Intraction();
            }
        }

        if (doorBeforeBreak)
        {
            if (introDialogues.introLevelDoorBeforeDialogue.dialogueFinished)
            {
                doorBeforeBreak = false;
                endDoorBeforeIntraction();
            }
        }

        if (doorAfterBreak)
        {
            if (introDialogues.introLevelDoorAfterDialogue.dialogueFinished)
            {
                doorAfterBreak = false;
                endDoorAfterIntraction();
            }
        }
    }

    IEnumerator StartingCinematic_01()
    {

        Debug.Log("Starting Cinematic: StartingCinematic_01");
        //Starting setup
        blackFiller.SetActive(true);
        movementJoystick.SetActive(false); //disable joystick
        cc.canMove = false; //disable character movement;
        camBrain.enabled = false; //turn off cinemachine brain

        Debug.Log("Waiting" + DramaticDelay + "Seconds");
        yield return new WaitForSeconds(DramaticDelay);

        Debug.Log("Dialogue break");
        break1 = true;
        ///Start dialogue
        introDialogues.TriggerintroLevelStartingDialogue();
    }

    IEnumerator StartingCinematic_02()
    {
        yield return new WaitForSeconds(1);
        Debug.Log("Starting director (Eliza Waking up)");
        blackFiller.SetActive(false);
        director.Play();
    }

    public void triggerStartingCinematic_03()
    {
        StartCoroutine(startingCinematic_03());
    }

    IEnumerator startingCinematic_03()
    {
        yield return new WaitForSeconds(1);
        triggerTutorial1();
        Debug.Log("Finishing startingcinematic");
    }

    void enablePlayer()
    {
        movementJoystick.SetActive(true);
        cc.canMove = true;
        camBrain.enabled = true;
        //objectives.SetActive(true);
    }

    void disablePlayer()
    {
        movementJoystick.SetActive(false);
        cc.isMovementPressed = false;
        cc.canMove = false;
        //cc.cancelMovement();
        interactionButtonGO.SetActive(false);
        objectives.SetActive(false);
        interactionTextGO.SetActive(false);

    }

    public void backReset()
    {
        movementJoystick.SetActive(true);
        cc.canMove = true;
        interactionButtonGO.SetActive(true);
        //objectives.SetActive(true);
        camBrain.enabled = true;
        deadbody1InspectionCamera.Priority = 0;
        deadbody2InspectionCamera.Priority = 0;
        backButtonGO.SetActive(false);
    }

    public void triggerTutorial1()
    {
        tutorial1.SetActive(true);

    }

    public void endTutorial1()
    {
        tutorial1.SetActive(false);
        //objectives.SetActive(true);
        enablePlayer();
        zombie1InteractiveMark.SetActive(true);
    }

    public void deadBody1Interaction()
    {
        disablePlayer();
        //change to inspection camera
        deadbody1InspectionCamera.Priority = 50;

        if(!introDialogues.introLevelDeadBody1Dialogue.dialogueFinished)
        {
            StartCoroutine("deadbody1Interaction2");
        }

        else
        {
            deadbody1InteractionAfter();
        }
    }

    IEnumerator deadbody1Interaction2()
    {
        zombie1InteractiveMark.SetActive(false);
        zombie2InteractiveMark.SetActive(true);
        yield return new WaitForSeconds(2);
        //trigger deadbody 1 dialogue
        Debug.Log("Triggering deadbody 1 dialogue");
        introDialogues.TriggerintroLevelDeadBody1Dialogue();
        deadBody1InteractionBreak = true;
    }

    void deadbody1InteractionAfter()
    {
        backButtonGO.SetActive(true);
    }

    public void endDeadbody1Intraction()
    {
        deadbody1InspectionCamera.Priority = 0;
        deadbody2InteractionGO.canInteract = true;
        doorInteractionGO.canInteract = true;
        enablePlayer();
    }

    public void deadBody2Interaction()
    {
        disablePlayer();
        //change to inspection camera
        deadbody2InspectionCamera.Priority = 50;

        if (!introDialogues.introLevelDeadBody2Dialogue.dialogueFinished)
        {
            StartCoroutine("deadbody2Interaction2");
        }

        else
        {
            deadbody2InteractionAfter();
        }
    }

    IEnumerator deadbody2Interaction2()
    {
        zombie2InteractiveMark.SetActive(false);
        doorExitEffect.SetActive(true);
        yield return new WaitForSeconds(2);
        //trigger deadbody 1 dialogue
        Debug.Log("Triggering deadbody 2 dialogue");
        introDialogues.TriggerintroLevelDeadBody2Dialogue();
        deadBody2InteractionBreak = true;
    }
    public void endDeadbody2Intraction()
    {
        deadbody2InspectionCamera.Priority = 0;
        enablePlayer();
        doorInteractionGO.canOpen = true;
        objectivesText.text = "Exit the room";
    }

    void deadbody2InteractionAfter()
    {
        backButtonGO.SetActive(true);
    }

    public void doorBeforeInteraction()
    {
        disablePlayer();
        doorBeforeBreak = true;
        introDialogues.introLevelDoorBeforeDialogue.dialogueFinished = false; //faulty
        introDialogues.TriggerintroLevelDoorBeforeDialogue(); 
    }

    public void endDoorBeforeIntraction()
    {
        enablePlayer();
    }

    public void triggerDoorAfterInteraction()
    {
        doorExitEffect.SetActive(false);

        disablePlayer();
        camBrain.enabled = false;
        doorclosetCinematicDirector.Play(); //signals at the end to trigger door closet dialogue
        endBGMusic();
    }

    public void triggerDoorClosetDialogue()
    {
        doorAfterBreak = true;
        introDialogues.TriggerintroLevelDoorAfterDialogue();
    }

    public void endDoorAfterIntraction()
    {
        doorclosetCinematic2Director.Play(); //last cinematic, signals at the end
    }

    public void exitRoom()
    {
        SceneManager.LoadScene("IntroLevelUpperFloorArea");
    }
}
