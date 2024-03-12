using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parachuting : MonoBehaviour
{
    public GameObject tileDead;
    public bool isdead;
    public bool isCimetiere;
    public GameObject cimetierePos;
    public GameObject parachutePrefab;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            isdead = true;
        }


        if (isdead)
        {
            tileDead.transform.position = cimetierePos.transform.position;
            isCimetiere = true;
        }


        if (isCimetiere)
        {
            Parachuting();
        }


    }

    void Parachuting()
    {
        GameObject parachuteEffect = Instantiate(parachutePrefab, transform.position, Quaternion.identity);
        Destroy(parachuteEffect, 3f);
        isCimetiere = false;
    }
}
