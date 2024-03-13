using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public Tile[] tiles;

    public GameObject[] cimP1;
    public GameObject[] cimP2;

    public GameObject roiP1;
    public GameObject roiP2;
    public GameObject EndUi;
    public TextMeshProUGUI TEXTwIN;

    public List<GameObject> pionDeadForP1 = new List<GameObject>();
    public List<GameObject> pionDeadForP2 = new List<GameObject>();

    public List<Tile> tilesEmpty = new List<Tile>();

    public bool turnbot = true;

    public List<Pion> pionPlayerBot = new List<Pion>();
    public List<Pion> pionPlayerTop = new List<Pion>();

    public List<string> moovP1 = new List<string>();
    public List<string> moovP2 = new List<string>();

    public bool actionPion = false;
    public GameObject pionUse;

    public TextMeshProUGUI textMeshProUGUI;

    private void Start()
    {
        FindEmptyTiles();
        switchPionActive();
        Time.timeScale = 1f;
    }

    public void FixedUpdate()
    {
        if (roiP1.GetComponent<Pion>().isDead)
        {
            Time.timeScale = 0;
            EndUi.SetActive(true);
            TEXTwIN.text = "Player 2 Win";
        }

        if (roiP2.GetComponent<Pion>().isDead)
        {
            Time.timeScale = 0;
            EndUi.SetActive(true);
            TEXTwIN.text = "Player 1 Win";
        }

        
    }

    void checkMoov()//Check for matchNull
    {
        if (moovP1.Count == 6 && moovP2.Count == 6)
        {
            if (moovP1[0] == moovP1[2] && moovP1[2] == moovP1[4] && moovP1[1] == moovP1[5])
            {
                if (moovP2[0] == moovP2[2] && moovP2[2] == moovP2[4] && moovP2[1] == moovP2[5])
                {
                    Time.timeScale = 0;
                    EndUi.SetActive(true);
                    TEXTwIN.text = "Match Null";
                }
            }
        }
        
    }

    public void FindEmptyTiles()
    {
        tilesEmpty.Clear();

        for (int i = 0; i < tiles.Length; i++)
        {
            tiles[i].ShootRaycastFromTop();
        }

        for (int i = 0; i < tiles.Length; i++)
        {
            if (tiles[i].imEmpty)
            {
                tilesEmpty.Add(tiles[i]);
            }

        }
    }

    public void CheckTileEmptyInGame()
    {
        tilesEmpty.Clear();

        for (int i = 0; i < tiles.Length; i++)
        {
            if (tiles[i].pionOnMe == null)
            {
                tilesEmpty.Add(tiles[i]);
            }
        }
    }

    public void switchPionActive()
    {
        if (turnbot)
        {
            for (int i = 0; i < pionPlayerBot.Count; i++)
            {
                pionPlayerBot[i].GetComponent<Collider>().enabled = true;
            }

            for (int i = 0; i < pionPlayerTop.Count; i++)
            {
                pionPlayerTop[i].GetComponent<Collider>().enabled = false;
            }
        }
        else
        {
            for (int i = 0; i < pionPlayerBot.Count; i++)
            {
                pionPlayerBot[i].GetComponent<Collider>().enabled = false;
            }

            for (int i = 0; i < pionPlayerTop.Count; i++)
            {
                pionPlayerTop[i].GetComponent<Collider>().enabled = true;
            }
        }

    }

    public void RefoundTiles()
    {
        for (int i = 0; i < pionPlayerBot.Count; i++)
        {
            if (pionPlayerBot[i].tiles != null)
            {
                pionPlayerBot[i].tiles.GetComponent<Tile>().pionOnMe = pionPlayerBot[i].gameObject;
            }          
        }

        for (int i = 0; i < pionPlayerTop.Count; i++)
        {
            if (pionPlayerTop[i].tiles != null)
            {
                pionPlayerTop[i].tiles.GetComponent<Tile>().pionOnMe = pionPlayerTop[i].gameObject;
            }
        }



    }

    public void ResetAllTileMatt()
    {
        for (int i = 0;i < tiles.Length;i++)
        {
            tiles[i].GetComponent<Tile>().ResetMatt();
        }
    }

    public void EndTurn()
    {
        checkMoov();

        ResetAllTileMatt();

        turnbot = !turnbot;

        if (turnbot)
        {
            textMeshProUGUI.text = "Au player 1 de jouer";
        }
        else
        {
            textMeshProUGUI.text = "Au player 2 de jouer";
        }

        switchPionActive();
        

        pionUse = null;
        actionPion = false;

    }
}
