using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public int Id;

    public bool imEmpty;
    public Vector3 pionTransform;

    public GameObject tileForward;
    public GameObject tileForwardRight;
    public GameObject tileForwardLeft;
    public GameObject tileRight;
    public GameObject tileLeft;
    public GameObject tileBack;
    public GameObject tileBackLeft;
    public GameObject tileBackRight;

    
    public GameObject pionOnMe;
    public bool imGoodForPlay = false;

    public Material normalColor;
    public Material usableColor;
    public Material usableKillColor;

    public TileManager tileManager;

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
    

    private void FixedUpdate()
    {
        CheckTile();

        
    }

    public void CheckTile()
    {
        ShootRaycastFromTop();
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

            pionOnMe = hit.collider.gameObject;
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

    public void Usable()
    {
        GetComponent<Renderer>().material = usableColor;
        imGoodForPlay = true;
    }

    public void ResetMatt()
    {
        GetComponent<Renderer>().material = normalColor;
        imGoodForPlay = false;
    }

    public void usableKill()
    {
        GetComponent<Renderer>().material = usableKillColor;
    }

    public void OnMouseDown()
    {
        if (tileManager.actionPion && imGoodForPlay)
        {
            if (pionOnMe != null)
            {
                if (pionOnMe.GetComponent<Pion>().bot != tileManager.pionUse.GetComponent<Pion>().bot)
                {
                    tileManager.pionUse.transform.position = pionTransform;

                    if (tileManager.turnbot)
                    {
                        pionOnMe.GetComponent<Pion>().isDead = true;

                        if (pionOnMe.GetComponent<Pion>().pionSO.pionName == "Kodama Samura")
                        {
                            pionOnMe.transform.position = tileManager.pionPlayerTop[1].transform.position;//Swap kodama samira en kodama

                            pionOnMe.SetActive(false);
                            tileManager.pionPlayerTop[1].gameObject.SetActive(true);

                            pionOnMe = tileManager.pionPlayerTop[1].gameObject;

                            tileManager.pionDeadForP1.Add(pionOnMe);

                            //Change camp P2 to P1
                            tileManager.pionPlayerBot.Add(pionOnMe.GetComponent<Pion>());
                            tileManager.pionPlayerTop.Remove(pionOnMe.GetComponent<Pion>());
                            pionOnMe.GetComponent<Pion>().bot = true;
                            pionOnMe.GetComponent<Renderer>().material = pionOnMe.GetComponent<Pion>().p1;
                            pionOnMe.transform.rotation = Quaternion.Euler(0,180,0);
                            pionOnMe.GetComponent<Pion>().tiles = null;


                            for (int i = 0; i < tileManager.pionDeadForP1.Count; i++)
                            {
                                tileManager.pionDeadForP1[i].transform.position = tileManager.cimP1[i].transform.position;
                            }

                            
                        }
                        else//jeu mormal
                        {
                            tileManager.pionDeadForP1.Add(pionOnMe);

                            //Change camp P2 to P1
                            tileManager.pionPlayerBot.Add(pionOnMe.GetComponent<Pion>());
                            tileManager.pionPlayerTop.Remove(pionOnMe.GetComponent<Pion>());
                            pionOnMe.GetComponent<Pion>().bot = true;
                            pionOnMe.GetComponent<Renderer>().material = pionOnMe.GetComponent<Pion>().p1;
                            pionOnMe.transform.rotation = Quaternion.Euler(0, 180, 0);
                            pionOnMe.GetComponent<Pion>().tiles = null;
                            //Debug.Log("uiiiiiiiiiiiiiiiiiii");

                            for (int i = 0; i < tileManager.pionDeadForP1.Count; i++)
                            {
                                tileManager.pionDeadForP1[i].transform.position = tileManager.cimP1[i].transform.position;
                            }
                          
                        }

                        
                    }
                    else
                    {
                        if (pionOnMe.GetComponent<Pion>().pionSO.pionName == "Kodama Samura")
                        {
                            pionOnMe.transform.position = tileManager.pionPlayerBot[1].transform.position;//Swap kodama samira en kodama

                            pionOnMe.SetActive(false);
                            tileManager.pionPlayerBot[1].gameObject.SetActive(true);

                            pionOnMe = tileManager.pionPlayerBot[1].gameObject;

                            tileManager.pionDeadForP2.Add(pionOnMe);

                            //Change camp P1 to P2
                            tileManager.pionPlayerTop.Add(pionOnMe.GetComponent<Pion>());
                            tileManager.pionPlayerBot.Remove(pionOnMe.GetComponent<Pion>());
                            pionOnMe.GetComponent<Pion>().bot = false;
                            pionOnMe.GetComponent<Renderer>().material = pionOnMe.GetComponent<Pion>().p2;
                            pionOnMe.transform.rotation = Quaternion.Euler(0, 0, 0);
                            pionOnMe.GetComponent<Pion>().tiles = null;

                            for (int i = 0; i < tileManager.pionDeadForP2.Count; i++)
                            {
                                tileManager.pionDeadForP2[i].transform.position = tileManager.cimP2[i].transform.position;
                            }

  
                        }
                        else
                        {
                            
                            pionOnMe.GetComponent<Pion>().isDead = true;

                            tileManager.pionDeadForP2.Add(pionOnMe);

                            //Change camp P1 to P2
                            tileManager.pionPlayerTop.Add(pionOnMe.GetComponent<Pion>());
                            tileManager.pionPlayerBot.Remove(pionOnMe.GetComponent<Pion>());
                            pionOnMe.GetComponent<Pion>().bot = false;
                            pionOnMe.GetComponent<Renderer>().material = pionOnMe.GetComponent<Pion>().p2;
                            pionOnMe.transform.rotation = Quaternion.Euler(0, 0, 0);
                            pionOnMe.GetComponent<Pion>().tiles = null;


                            for (int i = 0; i < tileManager.pionDeadForP2.Count; i++)
                            {
                                tileManager.pionDeadForP2[i].transform.position = tileManager.cimP2[i].transform.position;
                            }
                        }
                    }
                    

                    //Debug.Log("je tue et je prend la place");

                    tileManager.pionUse.GetComponent<Pion>().DeplacementAction();

                    pionOnMe = tileManager.pionUse;

                    pionOnMe.GetComponent<Pion>().tiles = this.gameObject;

                    if (tileManager.turnbot)//Add for match null
                    {
                        tileManager.moovP1.Add("" + pionOnMe.GetComponent<Pion>().pionSO.pionName + Id);
                    }
                    else
                    {
                        tileManager.moovP2.Add("" + pionOnMe.GetComponent<Pion>().pionSO.pionName + Id);
                    }

                    pionOnMe.GetComponent<Pion>().KickHoldTile();

                    tileManager.EndTurn();

                }              
            }
            else
            {
                if (imEmpty)//respawn after death
                {
                    tileManager.pionUse.transform.position = pionTransform;

                    //tileManager.pionUse.GetComponent<Pion>().DeplacementAction();

                    pionOnMe = tileManager.pionUse;

                    pionOnMe.GetComponent<Pion>().tiles = this.gameObject;

                    if (tileManager.turnbot)//Add for match null 
                    {
                        tileManager.moovP1.Add("" + pionOnMe.GetComponent<Pion>().pionSO.pionName + Id);
                        tileManager.pionDeadForP1.Remove(pionOnMe);
                        if (pionOnMe.GetComponent<Pion>().pionSO.pionName == "Kodama" && Id == 10 | Id == 11 | Id == 12)
                        {
                            //pionOnMe.GetComponent<Pion>().transformation = true;
                        }
                    }
                    else
                    {
                        tileManager.moovP2.Add("" + pionOnMe.GetComponent<Pion>().pionSO.pionName + Id);
                        tileManager.pionDeadForP2.Remove(pionOnMe);
                        if (pionOnMe.GetComponent<Pion>().pionSO.pionName == "Kodama" && Id == 1 | Id == 2 | Id == 3)
                        {
                            //pionOnMe.GetComponent<Pion>().transformation = true;
                        }
                    }

                    pionOnMe.GetComponent<Pion>().isDead = false;
                    

                    Debug.Log("yaaa c'est ici !!!");
                    //pionOnMe.GetComponent<Pion>().KickHoldTile();


                    tileManager.EndTurn();

                    
                }
                else
                {
                    Debug.Log("tu peux pas jouer la ");
                }
            }

            
        }
    }


}
