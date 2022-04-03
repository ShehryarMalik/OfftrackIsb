using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LvlSelectorCinematics : MonoBehaviour
{
    [SerializeField] private GameObject levelSelectui;

    public void levelOnePressed()
    {
        SceneManager.LoadScene("IntroLevelT1");
    }

    public void enableLevelSelectUI()
    {
        levelSelectui.SetActive(true);
    }

    public void quitGamePressed()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
