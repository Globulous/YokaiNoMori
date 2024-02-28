using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parachuting : MonoBehaviour
{
    public bool isdead;
    public bool isCimetiere;
    public GameObject cimetierePos;
    public GameObject parachutePrefab;

    void Update()
    {
        if (isdead)
        {
            transform.position = cimetierePos.transform.position;
        }
        else if (isCimetiere)
        {
            PerformParachuting();
        }
    }

    void PerformParachuting()
    {
        GameObject parachuteEffect = Instantiate(parachutePrefab, transform.position, Quaternion.identity);
        Destroy(parachuteEffect, 3f);
        isCimetiere = false;
    }
}
