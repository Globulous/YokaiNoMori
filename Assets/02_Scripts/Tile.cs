using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool imEmpty;

    public void GoEmpty()
    {
        imEmpty = true;

    }

    public void GoFull()
    {
        imEmpty = false;

    }

    public float raycastDistance = 10f;
    public LayerMask layerMask;

    private void Awake()
    {
        CheckTile();
    }
    public void CheckTile()
    {
        ShootRaycastFromTop();
    }

    /*private void FixedUpdate()
    {
        CheckTile();
    }*/

    void ShootRaycastFromTop()
    {
        // Calculer l'origine du raycast
        Vector3 raycastOrigin = transform.position + Vector3.up * transform.localScale.y;

        RaycastHit hit;
        if (Physics.Raycast(raycastOrigin, Vector3.up, out hit, raycastDistance, layerMask))
        {
            Debug.Log("Touché quelque chose : " + hit.collider.name);
            GoFull();
        }
        else
        {
            Debug.Log("Aucun objet touché.");
            GoEmpty();
        }

        // Debug dessiner le rayon
        Debug.DrawRay(raycastOrigin, Vector3.up * raycastDistance, Color.red);
    }
}
