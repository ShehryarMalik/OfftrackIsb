using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
public class GarageSceneCinematics : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera cvm, followCamPov;
    [SerializeField] PlayableDirector entryCinematicDirector, entryCinematic2Director, entryCinematic3Director, quickTimePassCinematicDirector;
    [SerializeField] GarageSceneDialogues garageDialogues;

    [SerializeField] private GameObject[] hudElements;

    [SerializeField] private GameObject quickTimecanvas, qtb1, qtb2, qtb3;
    [SerializeField] private GameObject zt21, interactButtonGO, aimButton, ammoUIGroup, gunTutorial;
    [SerializeField] private ThirdPersonCC characterCC;
    [SerializeField] private GarageSceneWaves waves;
    [SerializeField] private GarageDoorExit garageExitDoor;
    [SerializeField] private GameObject garageExitEffect;
    [SerializeField] private AudioSource tenseMusic;

    public void startTenseMusic()
    {
        if (tenseMusic)
            tenseMusic.Play();
    }

    private void hideHUD()
    {
        for (int i = 0; i < hudElements.Length; i++)
            hudElements[i].SetActive(false);
        
        characterCC.canMove = false;
    }

    private void unHideHUD()
    {
        for (int i = 0; i < hudElements.Length; i++)
            hudElements[i].SetActive(true);

        interactButtonGO.SetActive(false);
        characterCC.canMove = true;
    }

    public void EntryCinematic1()
    {
        cvm.Priority = 100;
        hideHUD();
        entryCinematicDirector.Play();
    }

    //Signal for dialogue
    public void EnteryCinematic2()
    {
        garageDialogues.TriggerGarageEntryDialogue();
        StartCoroutine("EntryCinematicDialogueBreak");
    }

    IEnumerator EntryCinematicDialogueBreak()
    {

        while(!garageDialogues.GarageEntryDialogue.dialogueFinished)
        {
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);
        EntryCinematic3();
    }

    public void EntryCinematic3()
    {
        entryCinematicDirector.Stop();
        entryCinematic2Director.Play();
        garageDialogues.TriggerGarageEntryDialogue2();
        StartCoroutine("EntryCinematicDialogue2Break");
    }
    IEnumerator EntryCinematicDialogue2Break()
    {

        while (!garageDialogues.GarageEntryDialogue2.dialogueFinished)
        {
            yield return null;
        }

        EntryCinematic4();
    }

    void EntryCinematic4()
    {
        entryCinematicDirector.Stop();
        entryCinematic2Director.Stop();
        entryCinematic3Director.Play(); //QuickTime cinematic
    }

    public void triggerEnteryCinematicQuickTime()
    {
        qtb2.SetActive(false);
        qtb3.SetActive(false);
        quickTimecanvas.SetActive(true);
        qtb1.SetActive(true);
    }

    public void QuickTimePass()
    {
        zt21.SetActive(false);
        entryCinematic3Director.Stop();
        quickTimecanvas.SetActive(false);
        quickTimePassCinematicDirector.Play();
    }

    public void TriggerQuickTimePassDialogue() //signaled
    {
        StartCoroutine("TriggerQuickTimePassDialogue2");
    }

    IEnumerator TriggerQuickTimePassDialogue2()
    {
        yield return new WaitForSeconds(1f);
        garageDialogues.TriggerQuickTimePassDialogue();
        StartCoroutine("QuickTimePassDialogueBreak");
    }
    IEnumerator QuickTimePassDialogueBreak()
    {

        while (!garageDialogues.QuickTimePassDialogue.dialogueFinished)
        {
            yield return null;
        }

        yield return new WaitForSeconds(1);
        endQuickTimeCutscene();
    }

    public void QuickTimeFail()
    {
        quickTimecanvas.SetActive(false);
        StartCoroutine("QuickTimeFail2");
    }

    IEnumerator QuickTimeFail2()
    {
        yield return new WaitForSeconds(2f);
        zt21.SetActive(false);
        garageDialogues.TriggerQuickTimeFailDialogue();
        StartCoroutine("QuickTimeFailDialogueBreak");
    }

    IEnumerator QuickTimeFailDialogueBreak()
    {

        while (!garageDialogues.QuickTimeFailDialogue.dialogueFinished)
        {
            yield return null;
        }

        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("GarageMap");
    }

    void endQuickTimeCutscene()
    {
        //Gun tutorial
        gunTutorial.SetActive(true);
        startTenseMusic();
    }

    public void endQuickTimeCutscene2()
    {
        entryCinematicDirector.Stop();
        entryCinematic2Director.Stop();
        entryCinematic3Director.Stop(); //QuickTime cinematic
        quickTimePassCinematicDirector.Stop();
        cvm.Priority = 0;
        unHideHUD();
        zt21.transform.position = new Vector3(-14.3699999f, -0.230000004f, 1.75999999f);
        zt21.SetActive(true);
        ZT2 zcc = zt21.GetComponent<ZT2>();
        zcc.takingBreak = false;
        zcc.detectedTarget = true;
        zcc.footStepSFX = true;
        StartCoroutine("ResetCam");
        waves.wave1On = true;
    }

    ///////////AFter waves are complete
    public void WavesCompleteDialogue()
    {
        StartCoroutine(WavesCompleteCom());
        StartCoroutine(FadeOutSound.FadeOut(tenseMusic, 1));
    }
    IEnumerator WavesCompleteCom()
    {
        yield return new WaitForSeconds(2f);
        hideHUD();
        garageDialogues.TriggerWaveClearDialogue();
        StartCoroutine("WavesCompleteDialogueBreak");
    }

    IEnumerator WavesCompleteDialogueBreak()
    {
        while (!garageDialogues.WavesClearDialogue.dialogueFinished)
        {
            yield return null;
        }

        yield return new WaitForSeconds(1);
        unHideHUD();
        garageExitDoor.canOpen = true;
        garageExitEffect.SetActive(true);
    }
    ////////////////////////////////

    IEnumerator ResetCam()
    {
        yield return new WaitForSeconds(1);
        CinemachinePOV pov = followCamPov.GetCinemachineComponent<CinemachinePOV>();
        if (pov != null)
        {
            pov.m_HorizontalAxis.Value = 174f;
        }
        else
        {
            Debug.Log("Error pov");
        }
    }
}
