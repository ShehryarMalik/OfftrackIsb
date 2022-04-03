using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroLevelSceneManager : MonoBehaviour
{
    [SerializeField] private GameObject cinematicManager;
    void Start()
    {
        //Level Starts
        cinematicManager.GetComponent<Introlevel_Srt1>().TriggerStartingCinematic_01();
        
    }

}
