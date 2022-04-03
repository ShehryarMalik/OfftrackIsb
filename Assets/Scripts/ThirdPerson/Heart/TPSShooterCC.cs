using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class TPSShooterCC : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera TPSFollowCam, TPSAimCam;

    public void StartAim()
    {
        TPSAimCam.Priority = 40;
        TPSFollowCam.Priority = 10;
    }

    public void CancelAim()
    {
        TPSAimCam.Priority = 10;
        TPSFollowCam.Priority = 40;
    }

}
