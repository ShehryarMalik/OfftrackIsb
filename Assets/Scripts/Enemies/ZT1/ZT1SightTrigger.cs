using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZT1SightTrigger : MonoBehaviour
{
    ZT1Controller zt1Controller;
    private void Start()
    {
        zt1Controller = GetComponentInParent<ZT1Controller>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            zt1Controller.TriggerTargetFound();
        }
    }
}
