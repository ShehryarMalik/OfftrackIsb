using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MainMenuSceneManager : MonoBehaviour
{
    [SerializeField]
    private GameObject blackFiller, confirmNewSaveDialogue, noSaveLoadDialogue, confirmLoadDataDialogue, startingNewGame;
    [SerializeField]
    private float blackScreenTimeDelay;
    [SerializeField]
    private float startingTimeDelay;
    [SerializeField]
    private MainMenuFillerAnim blackFillerAnimation;
    [SerializeField]
    private RectTransform wholeFiller;

    private LevelsData levelsData;
    void Start()
    {
        levelsData = SaveSystem.LoadLevel();

        if(levelsData == null)
        {
            Debug.Log("No save file");
        }
        else
        {
            Debug.Log("Save file found");
        }

        Invoke("fadeIn", blackScreenTimeDelay);
    }

    void fadeIn()
    {
        wholeFiller.LeanAlpha(0f, 7f).setOnComplete(startWait);
    }

    void startWait()
    {
        wholeFiller.gameObject.SetActive(false);
        StartCoroutine(enableBlackFiller());
    }

    IEnumerator enableBlackFiller()
    {
        yield return new WaitForSeconds(startingTimeDelay);
        blackFiller.SetActive(true);
        blackFillerAnimation.blackFillerAnimation();
    }

    public void startNewGame()
    {
        Debug.Log("starting New Level");

        //no save file exists
        if (levelsData == null)
        {
            startingNewGame.SetActive(true);
            SceneManager.LoadScene("IntroLevelT1");
        }
        //save file exists
        else
        {
            confirmNewSaveDialogue.SetActive(true);
        }
    }

    public void ConfirmedNewGame()
    {
        SceneManager.LoadScene("IntroLevelT1");
    }

    public void loadGame()
    {
        //no save file exists
        if (levelsData == null)
        {
            noSaveLoadDialogue.SetActive(true);
        }
        //save file exists
        else
        {
            confirmLoadDataDialogue.SetActive(true);
        }
    }

    public void ConfirmedLoadGame()
    {
        SceneManager.LoadScene("LevelSelectorScene");
    }
}
