using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
public class UpperFloorAreaZombieEntryTrigger : MonoBehaviour
{
    [SerializeField] private ZT1Controller zt1Controller;
    [SerializeField] private UpperFloorAreaCinematics upperFloorCinematics;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            zt1Controller.canMoveToPoints = true;
            upperFloorCinematics.ZombieEntryCinematic1();
        }
    }
}
