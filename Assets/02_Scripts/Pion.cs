using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Pion : MonoBehaviour
{
    public PionSO pionSO;

    public GameObject tiles;

    public bool bot;

    [SerializeField]
    TileManager tileManager;

    public bool isDead;
    
    public bool transformation = false;

    public Material p1;
    public Material p2;


    public void OnMouseDown()
    {
        tileManager.ResetAllTileMatt();

        Debug.Log(gameObject.name);

        if (isDead)
        {
            if (pionSO.pionName == "Kitsune")
            {
                tileManager.CheckTileEmptyInGame();

                if (tileManager.turnbot == true && tileManager.pionDeadForP1.Contains(this.gameObject))
                {
                    for (int i = 0; i < tileManager.tilesEmpty.Count; i++)
                    {
                        tileManager.tilesEmpty[i].GetComponent<Tile>().Usable();
                    }
                    //Debug.Log("ouiiiiiiiiii");
                }
                else if (tileManager.turnbot == false && tileManager.pionDeadForP2.Contains(this.gameObject))
                {
                    for (int i = 0; i < tileManager.tilesEmpty.Count; i++)
                    {
                        tileManager.tilesEmpty[i].GetComponent<Tile>().Usable();
                    }
                    //Debug.Log("noooooooooon");
                }

                tileManager.actionPion = true;
                tileManager.pionUse = this.gameObject;
            }

            if (pionSO.pionName == "Kodama")//Dep 1
            {
                tileManager.CheckTileEmptyInGame();

                if (tileManager.turnbot == true && tileManager.pionDeadForP1.Contains(this.gameObject))
                {
                    for (int i = 0; i < tileManager.tilesEmpty.Count; i++)
                    {
                        tileManager.tilesEmpty[i].GetComponent<Tile>().Usable();
                    }
                    //Debug.Log("ouiiiiiiiiii");
                }
                else if (tileManager.turnbot == false && tileManager.pionDeadForP2.Contains(this.gameObject))
                {
                    for (int i = 0; i < tileManager.tilesEmpty.Count; i++)
                    {
                        tileManager.tilesEmpty[i].GetComponent<Tile>().Usable();
                    }
                    //Debug.Log("noooooooooon");
                }

                tileManager.actionPion = true;
                tileManager.pionUse = this.gameObject;
            }

            if (pionSO.pionName == "Koropokkuru")//Dep 1
            {
                tileManager.CheckTileEmptyInGame();

                if (tileManager.turnbot == true && tileManager.pionDeadForP1.Contains(this.gameObject))
                {
                    for (int i = 0; i < tileManager.tilesEmpty.Count; i++)
                    {
                        tileManager.tilesEmpty[i].GetComponent<Tile>().Usable();
                    }
                    //Debug.Log("ouiiiiiiiiii");
                }
                else if (tileManager.turnbot == false && tileManager.pionDeadForP2.Contains(this.gameObject))
                {
                    for (int i = 0; i < tileManager.tilesEmpty.Count; i++)
                    {
                        tileManager.tilesEmpty[i].GetComponent<Tile>().Usable();
                    }
                    //Debug.Log("noooooooooon");
                }

                tileManager.actionPion = true;
                tileManager.pionUse = this.gameObject;
            }

            if (pionSO.pionName == "Tanuki")
            {
                tileManager.CheckTileEmptyInGame();

                if (tileManager.turnbot == true && tileManager.pionDeadForP1.Contains(this.gameObject))
                {
                    for (int i = 0; i < tileManager.tilesEmpty.Count; i++)
                    {
                        tileManager.tilesEmpty[i].GetComponent<Tile>().Usable();
                    }
                    //Debug.Log("ouiiiiiiiiii");
                }
                else if (tileManager.turnbot == false && tileManager.pionDeadForP2.Contains(this.gameObject))
                {
                    for (int i = 0; i < tileManager.tilesEmpty.Count; i++)
                    {
                        tileManager.tilesEmpty[i].GetComponent<Tile>().Usable();
                    }
                    //Debug.Log("noooooooooon");
                }

                tileManager.actionPion = true;
                tileManager.pionUse = this.gameObject;
            }
        }
        else
        {
            if (pionSO.pionName == "Kitsune")
            {
                if (tiles.GetComponent<Tile>().tileForwardRight != null)
                {
                    tiles.GetComponent<Tile>().tileForwardRight.GetComponent<Tile>().Usable();
                }

                if (tiles.GetComponent<Tile>().tileForwardLeft != null)
                {
                    tiles.GetComponent<Tile>().tileForwardLeft.GetComponent<Tile>().Usable();
                }

                if (tiles.GetComponent<Tile>().tileBackLeft != null)
                {
                    tiles.GetComponent<Tile>().tileBackLeft.GetComponent<Tile>().Usable();
                }

                if (tiles.GetComponent<Tile>().tileBackRight != null)
                {
                    tiles.GetComponent<Tile>().tileBackRight.GetComponent<Tile>().Usable();
                }

                tileManager.actionPion = true;
                tileManager.pionUse = this.gameObject;
            }

            if (pionSO.pionName == "Kodama Samura")
            {
                if (bot)
                {
                    if (tiles.GetComponent<Tile>().tileForward != null)
                    {
                        tiles.GetComponent<Tile>().tileForward.GetComponent<Tile>().Usable();
                    }

                    if (tiles.GetComponent<Tile>().tileBack != null)
                    {
                        tiles.GetComponent<Tile>().tileBack.GetComponent<Tile>().Usable();
                    }

                    if (tiles.GetComponent<Tile>().tileLeft != null)
                    {
                        tiles.GetComponent<Tile>().tileLeft.GetComponent<Tile>().Usable();
                    }

                    if (tiles.GetComponent<Tile>().tileRight != null)
                    {
                        tiles.GetComponent<Tile>().tileRight.GetComponent<Tile>().Usable();
                    }

                    if (tiles.GetComponent<Tile>().tileForwardLeft != null)
                    {
                        tiles.GetComponent<Tile>().tileForwardLeft.GetComponent<Tile>().Usable();
                    }

                    if (tiles.GetComponent<Tile>().tileForwardRight != null)
                    {
                        tiles.GetComponent<Tile>().tileForwardRight.GetComponent<Tile>().Usable();
                    }


                }
                else
                {
                    if (tiles.GetComponent<Tile>().tileForward != null)
                    {
                        tiles.GetComponent<Tile>().tileForward.GetComponent<Tile>().Usable();
                    }

                    if (tiles.GetComponent<Tile>().tileBack != null)
                    {
                        tiles.GetComponent<Tile>().tileBack.GetComponent<Tile>().Usable();
                    }

                    if (tiles.GetComponent<Tile>().tileLeft != null)
                    {
                        tiles.GetComponent<Tile>().tileLeft.GetComponent<Tile>().Usable();
                    }

                    if (tiles.GetComponent<Tile>().tileRight != null)
                    {
                        tiles.GetComponent<Tile>().tileRight.GetComponent<Tile>().Usable();
                    }

                    if (tiles.GetComponent<Tile>().tileBackRight != null)
                    {
                        tiles.GetComponent<Tile>().tileBackRight.GetComponent<Tile>().Usable();
                    }

                    if (tiles.GetComponent<Tile>().tileBackLeft != null)
                    {
                        tiles.GetComponent<Tile>().tileBackLeft.GetComponent<Tile>().Usable();
                    }
                }

                tileManager.actionPion = true;
                tileManager.pionUse = this.gameObject;
            }

            if (pionSO.pionName == "Kodama")//Dep 1
            {
                if (bot)
                {
                    if (tiles.GetComponent<Tile>().tileForward != null)
                    {
                        tiles.GetComponent<Tile>().tileForward.GetComponent<Tile>().Usable();
                    }

                }
                else
                {
                    if (tiles.GetComponent<Tile>().tileBack != null)
                    {
                        tiles.GetComponent<Tile>().tileBack.GetComponent<Tile>().Usable();
                    }

                }

                tileManager.actionPion = true;
                tileManager.pionUse = this.gameObject;
            }

            if (pionSO.pionName == "Koropokkuru")//Dep 1
            {
                if (tiles.GetComponent<Tile>().tileForward != null)
                {
                    tiles.GetComponent<Tile>().tileForward.GetComponent<Tile>().Usable();
                }

                if (tiles.GetComponent<Tile>().tileBack != null)
                {
                    tiles.GetComponent<Tile>().tileBack.GetComponent<Tile>().Usable();
                }

                if (tiles.GetComponent<Tile>().tileLeft != null)
                {
                    tiles.GetComponent<Tile>().tileLeft.GetComponent<Tile>().Usable();
                }

                if (tiles.GetComponent<Tile>().tileRight != null)
                {
                    tiles.GetComponent<Tile>().tileRight.GetComponent<Tile>().Usable();
                }

                if (tiles.GetComponent<Tile>().tileForwardLeft != null)
                {
                    tiles.GetComponent<Tile>().tileForwardLeft.GetComponent<Tile>().Usable();
                }

                if (tiles.GetComponent<Tile>().tileForwardRight != null)
                {
                    tiles.GetComponent<Tile>().tileForwardRight.GetComponent<Tile>().Usable();
                }

                if (tiles.GetComponent<Tile>().tileBackRight != null)
                {
                    tiles.GetComponent<Tile>().tileBackRight.GetComponent<Tile>().Usable();
                }

                if (tiles.GetComponent<Tile>().tileBackLeft != null)
                {
                    tiles.GetComponent<Tile>().tileBackLeft.GetComponent<Tile>().Usable();
                }

                tileManager.actionPion = true;
                tileManager.pionUse = this.gameObject;
            }

            if (pionSO.pionName == "Tanuki")
            {
                if (tiles.GetComponent<Tile>().tileForward != null)
                {
                    tiles.GetComponent<Tile>().tileForward.GetComponent<Tile>().Usable();
                }

                if (tiles.GetComponent<Tile>().tileBack != null)
                {
                    tiles.GetComponent<Tile>().tileBack.GetComponent<Tile>().Usable();
                }

                if (tiles.GetComponent<Tile>().tileLeft != null)
                {
                    tiles.GetComponent<Tile>().tileLeft.GetComponent<Tile>().Usable();
                }

                if (tiles.GetComponent<Tile>().tileRight != null)
                {
                    tiles.GetComponent<Tile>().tileRight.GetComponent<Tile>().Usable();
                }

                tileManager.actionPion = true;
                tileManager.pionUse = this.gameObject;
            }
        }

        tileManager.RefoundTiles();
    }

    public void FixedUpdate()//For swap kodama en kodama samura
    {
        if (!isDead && !transformation) 
        {
            if (pionSO.pionName == "Kodama" && bot == true && tiles.GetComponent<Tile>().Id == 10)
            {
                tileManager.pionPlayerBot[4].GetComponent<GameObject>().transform.position = this.transform.position;//Swap le kodama avec le kodama samura

                tileManager.pionPlayerBot[4].GetComponent<Pion>().tiles = tiles;
                tiles.GetComponent<Tile>().pionOnMe = tileManager.pionPlayerBot[4].gameObject;

                tileManager.pionPlayerBot[4].gameObject.SetActive(true);
                this.gameObject.SetActive(false);

                transformation = true;
            }

            if (pionSO.pionName == "Kodama" && bot == true && tiles.GetComponent<Tile>().Id == 11)
            {
                tileManager.pionPlayerBot[4].GetComponent<Transform>().position = this.transform.position;

                tileManager.pionPlayerBot[4].GetComponent<Pion>().tiles = tiles;
                tiles.GetComponent<Tile>().pionOnMe = tileManager.pionPlayerBot[4].gameObject;

                tileManager.pionPlayerBot[4].gameObject.SetActive(true);
                this.gameObject.SetActive(false);

                transformation = true;
            }

            if (pionSO.pionName == "Kodama" && bot == true && tiles.GetComponent<Tile>().Id == 12)
            {
                tileManager.pionPlayerBot[4].GetComponent<Transform>().position = this.transform.position;

                tileManager.pionPlayerBot[4].GetComponent<Pion>().tiles = tiles;
                tiles.GetComponent<Tile>().pionOnMe = tileManager.pionPlayerBot[4].gameObject;

                tileManager.pionPlayerBot[4].gameObject.SetActive(true);
                this.gameObject.SetActive(false);

                transformation = true;
            }



            if (pionSO.pionName == "Kodama" && bot == false && tiles.GetComponent<Tile>().Id == 1)
            {
                tileManager.pionPlayerTop[4].GetComponent<Transform>().position = this.transform.position;

                tileManager.pionPlayerTop[4].GetComponent<Pion>().tiles = tiles;
                tiles.GetComponent<Tile>().pionOnMe = tileManager.pionPlayerTop[4].gameObject;

                tileManager.pionPlayerTop[4].gameObject.SetActive (true);
                this.gameObject.SetActive(false);

                transformation = true;
            }

            if (pionSO.pionName == "Kodama" && bot == false && tiles.GetComponent<Tile>().Id == 2)
            {
                tileManager.pionPlayerTop[4].GetComponent<Transform>().position = this.transform.position;

                tileManager.pionPlayerTop[4].GetComponent<Pion>().tiles = tiles;
                tiles.GetComponent<Tile>().pionOnMe = tileManager.pionPlayerTop[4].gameObject;

                tileManager.pionPlayerTop[4].gameObject.SetActive(true);
                this.gameObject.SetActive(false);

                transformation = true;
            }

            if (pionSO.pionName == "Kodama" && bot == false && tiles.GetComponent<Tile>().Id == 3)
            {
                tileManager.pionPlayerTop[4].GetComponent<Transform>().position = this.transform.position;

                tileManager.pionPlayerTop[4].GetComponent<Pion>().tiles = tiles;
                tiles.GetComponent<Tile>().pionOnMe = tileManager.pionPlayerTop[4].gameObject;

                tileManager.pionPlayerTop[4].gameObject.SetActive(true);
                this.gameObject.SetActive(false);

                transformation = true;
            }

            

            
        }
        
    }

    public void DeplacementAction()
    {
        tiles.GetComponent<Tile>().pionOnMe = null;  
    }
}
