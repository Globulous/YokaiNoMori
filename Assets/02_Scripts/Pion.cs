using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pion : MonoBehaviour
{
    public PionSO pionSO;

    public GameObject tiles;

    public bool bot;

    [SerializeField]
    TileManager tileManager;

    public bool isDead;




    public void OnMouseDown()
    {
        tileManager.ResetAllTileMatt();

        Debug.Log(gameObject.name);

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

        }

        if (pionSO.pionName == "Kodama")//Dep 1
        {
            if (bot)
            {
                if (tiles.GetComponent<Tile>().tileForward.GetComponent<Tile>().imEmpty)
                {
                    tiles.GetComponent<Tile>().tileForward.GetComponent<Tile>().Usable();
                }
                /*else
                {
                    tiles.GetComponent<Tile>().tileForward.GetComponent<Tile>().usableKill();
                }*/
            }
            else
            {
                if (tiles.GetComponent<Tile>().tileBack.GetComponent<Tile>().imEmpty)
                {
                    tiles.GetComponent<Tile>().tileBack.GetComponent<Tile>().Usable();
                }
                /*else
                {
                    tiles.GetComponent<Tile>().tileBack.GetComponent<Tile>().usableKill();
                }*/
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

    public void DeplacementAction()
    {
        tiles.GetComponent<Tile>().pionOnMe = null;  
    }
}
