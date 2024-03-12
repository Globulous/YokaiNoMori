using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public Tile[] tiles;

    public GameObject[] cimP1;
    public GameObject[] cimP2;

    public List<GameObject> pionDeadForP1 = new List<GameObject>();
    public List<GameObject> pionDeadForP2 = new List<GameObject>();

    public List<Tile> tilesEmpty = new List<Tile>();

    public bool turnbot = true;

    public List<Pion> pionPlayerBot = new List<Pion>();
    public List<Pion> pionPlayerTop = new List<Pion>();

    public bool actionPion = false;
    public GameObject pionUse;

    public TextMeshProUGUI textMeshProUGUI;

    private void Start()
    {
        FindEmptyTiles();
        switchPionActive();
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

    public void ResetAllTileMatt()
    {
        for (int i = 0;i < tiles.Length;i++)
        {
            tiles[i].GetComponent<Tile>().ResetMatt();
        }
    }

    public void EndTurn()
    {
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
