using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZT2Shootable : ShootableObject
{
    private ZT2 zt2Comp;

    private void Awake()
    {
        zt2Comp = GetComponent<ZT2>();
    }
    public override void ShotReaction(int damageAmount)
    {
        zt2Comp.takeDamage(damageAmount);
    }
}
