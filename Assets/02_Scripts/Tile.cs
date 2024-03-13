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

                    if (tileManager.turnbot)// Player 1
                    {
                        pionOnMe.GetComponent<Pion>().isDead = true;

                        if (pionOnMe.GetComponent<Pion>().pionSO.pionName == "Kodama Samura")
                        {
                            pionOnMe.SetActive(false);


                            tileManager.pionDeadForP1.Add(pionOnMe);

                            //Change camp P2 to P1
                            tileManager.pionPlayerBot.Add(pionOnMe.GetComponent<Pion>());
                            tileManager.pionPlayerTop.Remove(pionOnMe.GetComponent<Pion>());
                            pionOnMe.GetComponent<Pion>().bot = true;
                            pionOnMe.GetComponent<Renderer>().material = pionOnMe.GetComponent<Pion>().p1;
                            pionOnMe.transform.rotation = Quaternion.Euler(0,180,0);
                            pionOnMe.GetComponent<Pion>().tiles = null;
                            pionOnMe.GetComponent<Pion>().tiles = null;


                            Debug.Log("bbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbb");

                            for (int i = 0; i < tileManager.pionDeadForP1.Count; i++)
                            {
                                tileManager.pionDeadForP1[i].transform.position = tileManager.cimP1[i].transform.position;
                            }

                            if (pionOnMe.name == "KodamaSamurai P1")
                            {
                                tileManager.koramaP1.gameObject.transform.position = pionOnMe.transform.position;
                                tileManager.koramaP1.gameObject.SetActive(true);

                                tileManager.pionPlayerBot.Add(tileManager.koramaP1);
                                tileManager.pionPlayerTop.Remove(tileManager.koramaP1);
                                tileManager.koramaP1.GetComponent<Pion>().bot = true;
                                tileManager.koramaP1.GetComponent<Renderer>().material = pionOnMe.GetComponent<Pion>().p1;
                                tileManager.koramaP1.transform.rotation = Quaternion.Euler(0, 180, 0);
                                tileManager.koramaP1.GetComponent<Pion>().tiles = null;
                                tileManager.koramaP1.GetComponent<Pion>().tiles = null;

                            }
                            else if (pionOnMe.name == "KodamaSamurai P2")
                            {
                                tileManager.koramaP2.gameObject.transform.position = pionOnMe.transform.position;
                                tileManager.koramaP2.gameObject.SetActive(true);

                                tileManager.pionPlayerBot.Add(tileManager.koramaP2);
                                tileManager.pionPlayerTop.Remove(tileManager.koramaP2);
                                tileManager.koramaP2.GetComponent<Pion>().bot = true;
                                tileManager.koramaP2.GetComponent<Renderer>().material = pionOnMe.GetComponent<Pion>().p1;
                                tileManager.koramaP2.transform.rotation = Quaternion.Euler(0, 180, 0);
                                tileManager.koramaP2.GetComponent<Pion>().tiles = null;
                                tileManager.koramaP2.GetComponent<Pion>().tiles = null;
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

                            if (pionOnMe.GetComponent<Pion>().pionSO.pionName == "Kodama")
                            {
                                tileManager.pionPlayerBot.Add(tileManager.samuraP2);
                                tileManager.pionPlayerTop.Remove(tileManager.samuraP2);
                                tileManager.samuraP2.GetComponent<Pion>().bot = true;
                                tileManager.samuraP2.GetComponent<Renderer>().material = tileManager.samuraP2.GetComponent<Pion>().p1;
                                tileManager.samuraP2.transform.rotation = Quaternion.Euler(0, 180, 0);
                                tileManager.samuraP2.GetComponent<Pion>().tiles = null;
                            }
                            //Debug.Log("uiiiiiiiiiiiiiiiiiii");

                            for (int i = 0; i < tileManager.pionDeadForP1.Count; i++)
                            {
                                tileManager.pionDeadForP1[i].transform.position = tileManager.cimP1[i].transform.position;
                            }
                            Debug.Log("samuuuuraaaa BBBB");
                        }

                        
                    }
                    else //Player 2
                    {
                        if (pionOnMe.GetComponent<Pion>().pionSO.pionName == "Kodama Samura")
                        {
                            pionOnMe.SetActive(false);


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

                            if (pionOnMe.name == "KodamaSamurai P1")
                            {
                                tileManager.koramaP1.gameObject.transform.position = pionOnMe.transform.position;
                                tileManager.koramaP1.gameObject.SetActive(true);

                                tileManager.pionPlayerTop.Add(tileManager.koramaP1);
                                tileManager.pionPlayerBot.Remove(tileManager.koramaP1);
                                tileManager.koramaP1.GetComponent<Pion>().bot = false;
                                tileManager.koramaP1.GetComponent<Renderer>().material = pionOnMe.GetComponent<Pion>().p2;
                                tileManager.koramaP1.transform.rotation = Quaternion.Euler(0, 0, 0);
                                tileManager.koramaP1.GetComponent<Pion>().tiles = null;
                            }
                            else if (pionOnMe.name == "KodamaSamurai P2")
                            {
                                tileManager.koramaP2.gameObject.transform.position = pionOnMe.transform.position;
                                tileManager.koramaP2.gameObject.SetActive(true);

                                tileManager.pionPlayerTop.Add(tileManager.koramaP2);
                                tileManager.pionPlayerBot.Remove(tileManager.koramaP2);
                                tileManager.koramaP2.GetComponent<Pion>().bot = false;
                                tileManager.koramaP2.GetComponent<Renderer>().material = pionOnMe.GetComponent<Pion>().p2;
                                tileManager.koramaP2.transform.rotation = Quaternion.Euler(0, 0, 0);
                                tileManager.koramaP2.GetComponent<Pion>().tiles = null;
                            }  

                            Debug.Log("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
      
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

                            if (pionOnMe.GetComponent<Pion>().pionSO.pionName == "Kodama")
                            {
                                tileManager.pionPlayerTop.Add(tileManager.samuraP1);
                                tileManager.pionPlayerBot.Remove(tileManager.samuraP1);
                                tileManager.samuraP1.GetComponent<Pion>().bot = false;
                                tileManager.samuraP1.GetComponent<Renderer>().material = tileManager.samuraP1.GetComponent<Pion>().p2;
                                tileManager.samuraP1.transform.rotation = Quaternion.Euler(0, 0, 0);
                                tileManager.samuraP1.GetComponent<Pion>().tiles = null;
                            }


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
                    Debug.Log("samuuuuraaaa CCCC");

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
                        if (pionOnMe.GetComponent<Pion>().pionSO.pionName == "Kodama" && Id == 10 | Id == 11 | Id == 12 && pionOnMe.GetComponent<Pion>().isDead)
                        {
                            pionOnMe.GetComponent<Pion>().transformation = true;
                        }
                    }
                    else
                    {
                        tileManager.moovP2.Add("" + pionOnMe.GetComponent<Pion>().pionSO.pionName + Id);
                        tileManager.pionDeadForP2.Remove(pionOnMe);
                        if (pionOnMe.GetComponent<Pion>().pionSO.pionName == "Kodama" && Id == 1 | Id == 2 | Id == 3 && pionOnMe.GetComponent<Pion>().isDead)
                        {
                            pionOnMe.GetComponent<Pion>().transformation = true;
                        }
                    }

                    pionOnMe.GetComponent<Pion>().isDead = false;
                    



                    Debug.Log("yaaa c'est ici !!!");



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
