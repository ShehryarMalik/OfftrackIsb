using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootSteps : MonoBehaviour
{
    public bool Playfootsteps = false;
    ThirdPersonCC cc;
    bool waiting = false;

    [SerializeField] InputManager inputManager;
    [SerializeField] float waitTime = 0.7f;
    [SerializeField] float waitRunTime = 0.4f;
    [SerializeField] AudioSource audioSource;
    void Start()
    {
        cc = GetComponentInParent<ThirdPersonCC>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Playfootsteps && !waiting && audioSource && cc.canMove && !cc.isSneakPressed && cc.isMovementPressed)
        {
            audioSource.pitch = Random.Range(0.8f, 1.1f);
            audioSource.Play();
            waiting = true;
            StartCoroutine("waitForNext");
        }
    }

    IEnumerator waitForNext()
    {
        if (inputManager.runToggleButton)
            yield return new WaitForSeconds(waitRunTime);

        else
            yield return new WaitForSeconds(waitTime);
            
        waiting = false;
    }
}

