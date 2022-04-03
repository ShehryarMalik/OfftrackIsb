using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MainMenuFillerAnim : MonoBehaviour
{
    [SerializeField] private Transform BlackFiller, GreenFiller;
    [SerializeField] private GameObject titleText, versionText;
    [SerializeField] private RectTransform newGameButton, loadGameButton;

    public void blackFillerAnimation()
    {
        newGameButton.anchoredPosition = new Vector3(-400f, -4.080048f, 0f);
        loadGameButton.anchoredPosition = new Vector3(-400f, -91.60001f, 0f);

        titleText.SetActive(false);
        versionText.SetActive(false);

        GreenFiller.gameObject.SetActive(true);
        GreenFiller.localScale = new Vector3(0, 1, 1);
        GreenFiller.LeanScaleX(3.1f, 1.5f).setEaseInOutQuad().setOnComplete(EnableTitle);

        BlackFiller.localScale = new Vector3(0, 1, 1);
        BlackFiller.LeanScaleX(3.1f, 1.5f).setEaseInOutQuad().setOnComplete(ButtonsAnimation).delay = 1.5f;
    }

    void ButtonsAnimation()
    {
        newGameButton.gameObject.SetActive(true);
        loadGameButton.gameObject.SetActive(true);
        newGameButton.LeanMoveX(46.21716f, 0.5f).setEaseOutQuad();
        loadGameButton.LeanMoveX(50.71712f, 0.5f).setEaseInOutQuad();
        versionText.SetActive(true);
    }

    public void EnableTitle()
    {
        Debug.Log("Enable title function triggered");
        titleText.SetActive(true);
    }
}
