using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MainFreeLookController : MonoBehaviour
{
    CinemachineFreeLook FLC;
    [SerializeField] private Transform characterCameraFocus;

    void Start()
    {
        FLC = this.GetComponent<CinemachineFreeLook>();
    }

    public void FocusTransformForSeconds(Transform transform, float seconds)
    {
        StartCoroutine(focusTransform(transform,seconds));
    }

    IEnumerator focusTransform(Transform transform, float seconds)
    {
        FLC.m_LookAt = transform;
        yield return new WaitForSeconds(seconds);
        FLC.m_LookAt = characterCameraFocus;
    }
}
