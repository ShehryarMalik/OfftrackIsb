using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public struct GameObjectToManageStruct
{
    public GameObject gameObject;
    public bool enabledInStart;
}

public class StartingStatesManager : MonoBehaviour
{
    [SerializeField]
    private GameObjectToManageStruct[] gms;

    private void Start()
    {
        if(gms.Length != 0)
        {
            for(int i = 0; i < gms.Length; i++)
            {
                if(gms[i].enabledInStart)
                {
                    gms[i].gameObject.SetActive(true);
                }

                else
                {
                    gms[i].gameObject.SetActive(false);
                }
            }
        }
    }
}
