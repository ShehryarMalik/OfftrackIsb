using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarageSceneWaves : MonoBehaviour
{
    [SerializeField] GarageSceneCinematics garageCinematics;
    [SerializeField] ZT2[] wave1;
    [SerializeField] ZT2[] wave2;
    [SerializeField] ZT2[] wave3;

    public bool wave1On = false, wave1Condition = false;
    public bool wave2On = false, wave2Condition = false;
    public bool wave3On = false, wave3Condition = false;

    private void Update()
    {
        if(wave1On)
        {
            wave1Condition = true;
            
            for(int i = 0; i < wave1.Length; i++)
            {
                if(!wave1[i].dead)
                {
                    wave1Condition = false;
                    break;
                }
            }

            if(wave1Condition)
            {
                //condition met
                Debug.Log("Wave 1 Complete");

                for (int i = 0; i < wave2.Length; i++)
                {
                    wave2[i].gameObject.SetActive(true);
                }

                wave2On = true;
                wave1On = false;
            }
        }

        if (wave2On)
        {
            wave2Condition = true;
            for (int i = 0; i < wave2.Length; i++)
            {
                if (!wave2[i].dead)
                {
                    wave2Condition = false;
                    break;
                }
            }

            if (wave2Condition)
            {
                //condition met
                Debug.Log("Wave 2 Complete");

                for (int i = 0; i < wave3.Length; i++)
                {
                    wave3[i].gameObject.SetActive(true);
                }

                wave3On = true;
                wave2On = false;
            }
        }

        if (wave3On)
        {
            wave3Condition = true;
            for (int i = 0; i < wave3.Length; i++)
            {
                if (!wave3[i].dead)
                {
                    wave3Condition = false;
                    break;
                }
            }

            if (wave3Condition)
            {
                //condition met
                garageCinematics.WavesCompleteDialogue();
                wave3On = false;
            }
        }

    }
}
