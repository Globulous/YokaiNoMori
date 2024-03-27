using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public GameObject[,] tilesArray = new GameObject[4, 3]; // 3 lignes, 4 colonnes

    public Material goodMat;
    public Material normalMat;
    public Material killMat;

    public bool step;


    public List<GameObject> PionPlayer1OnBoard;
    public List<GameObject> PionPlayer2OnBoard;

    public List<GameObject> PionPlayer1OnCim = new List<GameObject>();
    public List<GameObject> PionPlayer2OnCim = new List<GameObject>();

    public Transform[] transformCimPlayer1;
    public Transform[] transformCimPlayer2;

    public GameObject playerSelected;

    void Start()
    {
        for (int i = 0; i < 4; i++) // Pour chaque ligne
        {
            for (int j = 0; j < 3; j++) // Pour chaque colonne
            {               
                // Par exemple, si vos tiles sont nommées "Tile_0_0", "Tile_0_1", ..., "Tile_2_3"
                string tileName = "Tile_" + i + "_" + j;
                tilesArray[i, j] = GameObject.Find(tileName);

                if (tilesArray[i, j] != null)
                {
                    //Debug.Log("Tile (" + i + ", " + j + ") found: " + tilesArray[i, j].name);
                }
                else
                {
                    Debug.LogWarning("Tile (" + i + ", " + j + ") not found!");
                }
            }
        }

        ChageStep();
        //exemple : tilesArray[0, 0] pour la première tile
    }

    public void ChangeColor()
    {
        for (int i = 0; i < 4; i++) // Pour chaque ligne
        {
            for (int j = 0; j < 3; j++) // Pour chaque colonne
            {
               
                if (tilesArray[i, j] != null)
                {
                    tilesArray[i, j].GetComponent<Renderer>().material = normalMat;
                }
                
            }
        }
    }


    public void ChageStep()//step = true alors c'est au player 1 de jouer
    {
        step = !step;

        if (step)
        {
            for (int i = 0; i < PionPlayer1OnBoard.Count; i++)
            {
                PionPlayer2OnBoard[i].GetComponent<Collider>().enabled = false;
                PionPlayer1OnBoard[i].GetComponent<Collider>().enabled = true;
            }
        }
        else
        {
            for (int i = 0; i < PionPlayer2OnBoard.Count; i++)
            {
                PionPlayer2OnBoard[i].GetComponent<Collider>().enabled = true;
                PionPlayer1OnBoard[i].GetComponent<Collider>().enabled = false;
            }
        }
        
    }

    public void PionGoDie(GameObject pion)
    {
        if (step)
        {

        }
        else
        {

        }
    }

    /// <summary>
    /// 
    ///
  /*public void CheckPossibleMoves(int row, int col, MovementType pionMovement)
    {
        // Vérifiez les mouvements possibles pour le pion
        for (int i = row - 1; i <= row + 1; i++)
        {
            for (int j = col - 1; j <= col + 1; j++)
            {
                // Assurez-vous que les indices sont valides et ne dépassent pas les limites du tableau
                if (i >= 0 && i < tilesArray.GetLength(0) && j >= 0 && j < tilesArray.GetLength(1))
                {
                    // Ne pas inclure la tile cliquée elle-même
                    if (i != row || j != col)
                    {
                        // Traitez la tile adjacente (par exemple, vérifiez si le mouvement est autorisé)
                        if (IsMovementAllowed(pionMovement, i - row, j - col))
                        {
                            GameObject adjacentTile = tilesArray[i, j];
                            if (adjacentTile != null)
                            {
                                Debug.Log("Possible move: " + adjacentTile.name);
                                // Vous pouvez faire autre chose ici avec la tile adjacente
                            }
                        }
                    }
                }
            }
        }
    }*/

    
}
