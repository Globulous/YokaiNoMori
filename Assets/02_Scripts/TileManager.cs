using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public Tile[] tiles;

    public List<Tile> tilesEmpty = new List<Tile>();
    private void Start()
    {
        FindEmptyTiles();
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
}
