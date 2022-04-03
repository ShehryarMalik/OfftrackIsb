using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class booterScript : MonoBehaviour
{
    public void Start()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
