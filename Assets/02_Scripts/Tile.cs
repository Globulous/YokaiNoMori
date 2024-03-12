using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool imEmpty;

    public GameObject tileForward;
    public GameObject tileForwardRight;
    public GameObject tileForwardLeft;
    public GameObject tileRight;
    public GameObject tileLeft;
    public GameObject tileBack;
    public GameObject tileBackLeft;
    public GameObject tileBackRight;

    
    public Collider pionOnMe;

    public void GoEmpty()
    {
        imEmpty = true;

    }

    public void GoFull()
    {
        imEmpty = false;

    }

    public float raycastDistance = 1f;
    public LayerMask layerMask;

    private void Awake()
    {
        CheckTile();
    }
    public void CheckTile()
    {
        ShootRaycastFromTop();
    }

    private void FixedUpdate()
    {
        CheckTile();
    }

    public void ShootRaycastFromTop()
    {
        // Calculer l'origine du raycast
        Vector3 raycastOrigin = transform.position + Vector3.up * transform.localScale.y;

        RaycastHit hit;
        if (Physics.Raycast(raycastOrigin, Vector3.up, out hit, raycastDistance, layerMask))
        {
            //Debug.Log("Touché quelque chose : " + hit.collider.name);
            GoFull();

            pionOnMe = hit.collider;
            pionOnMe.GetComponent<Pion>().tiles = this.gameObject;
        }
        else
        {
            //Debug.Log("Aucun objet touché.");
            GoEmpty();
            //pionOnMe = hit.collider.GetComponent<GameObject>();
        }

        // Debug dessiner le rayon
        Debug.DrawRay(raycastOrigin, Vector3.up * raycastDistance, Color.red);
    }
}
