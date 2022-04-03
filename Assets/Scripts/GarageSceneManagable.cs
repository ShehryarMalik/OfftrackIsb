using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
public class GarageSceneManagable : SceneManagable
{
    [SerializeField] private GarageSceneCinematics garageCinematics;
    [SerializeField] PistolGun pistol;
    [SerializeField] GarageSceneDialogues dialogues;
    [SerializeField] GameObject passValuesPrefab;
    GameObject passValuesGO;

    private void Awake()
    {
        //Search for pass values and if it doesnt exist, create it
        passValuesGO = GameObject.Find("PassValues");
        if (passValuesGO == null)
        {
            passValuesGO = Instantiate(passValuesPrefab);
            passValuesGO.name = "PassValues";
        }
    }

    private void Start()
    {
        garageCinematics.EntryCinematic1();
        pistol.updateAmmoUI();
    }
    public override void characterDeath()
    {
        dialogues.TriggerGeneralDeathDialogue();
        //Invoke(nameof(restartScene), timeBeforeLoadingScene);
        StartCoroutine("GeneralDeathDialogueBreak");
    }

    IEnumerator GeneralDeathDialogueBreak()
    {
        while (!dialogues.GeneralDeathDialogue.dialogueFinished)
        {
            yield return null;
        }

        yield return new WaitForSeconds(3f);
        restartScene();
    }

    void restartScene()
    {
        SceneManager.LoadScene("GarageMap");
    }
}
