using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deadbodyTrigger : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("Player entered in trigger");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("Player exited the trigger");
        }
    }
}
