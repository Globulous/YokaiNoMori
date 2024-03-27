using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Flags]
public enum TileType
{
    Forward_Left = 1 << 0,
    Forward_Mid = 1 << 1,
    Forward_Right = 1 << 2,
    Mid_Left = 1 << 3,
    Mid_Right = 1 << 4,
    Back_Left = 1 << 5,
    Back_Mid = 1 << 6,
    Back_Right = 1 << 7,
}


public class TileV2 : MonoBehaviour
{
    public TileType tileType;
    public GameManager gameManager;

    public GameObject pionOnMe;

    public bool getRedy = false;

    public Material m_Good;
    public Material m_kill;

    private void Start()
    {
        if (gameManager == null)
        {
            // Trouver le TileManager dans la sc�ne s'il n'est pas d�fini
            gameManager = GameObject.FindObjectOfType<GameManager>();

            // V�rifier si le TileManager a �t� trouv�
            if (gameManager == null)
            {
                Debug.LogError("TileManager not found in the scene!");
                return;
            }
        }

        CheckIfTileIsEmpty();
    }

    void OnMouseDown()
    {
        if (pionOnMe != null)
        {
            gameManager.PionGoDie(pionOnMe);


        }
        else
        {

        }
    }

    //gestion des tiles dispo pour le moovement 
    public void CheckAdjacentTiles(string name, bool playerCamp)
    {
        
        GameObject clickedTile = gameObject;
        gameManager.ChangeColor();
        // Trouvez la position de la tile cliqu�e dans le tableau tilesArray
        int rowIndex = -1;
        int colIndex = -1;
        for (int i = 0; i < gameManager.tilesArray.GetLength(0); i++)
        {
            for (int j = 0; j < gameManager.tilesArray.GetLength(1); j++)
            {
                if (gameManager.tilesArray[i, j] == clickedTile)
                {
                    rowIndex = i;
                    colIndex = j;
                    break;
                }
            }
            if (rowIndex != -1)
                break;
        }
        // V�rifiez les tiles adjacentes
        for (int i = rowIndex - 1; i <= rowIndex + 1; i++)
        {
            for (int j = colIndex - 1; j <= colIndex + 1; j++)
            {
                /*// Assurez-vous que les indices sont valides et ne d�passent pas les limites du tableau Pour all
                if (i >= 0 && i < gameManager.tilesArray.GetLength(0) && j >= 0 && j < gameManager.tilesArray.GetLength(1))
                {
                    // Ne pas inclure la tile cliqu�e elle-m�me
                    if (i != rowIndex || j != colIndex)
                    {
                        // Traitez la tile adjacente (par exemple, imprimez son nom dans la console)
                        GameObject adjacentTile = gameManager.tilesArray[i, j];
                        adjacentTile.GetComponent<TileV2>().GetComponent<Renderer>().material = m_Good;

                        if (adjacentTile != null)
                        {
                            Debug.Log("Tile adjacent: " + adjacentTile.name);
                        }
                    }
                }*/

                // Assurez-vous que les indices sont valides et ne d�passent pas les limites du tableau
                if (i >= 0 && i < gameManager.tilesArray.GetLength(0) && j >= 0 && j < gameManager.tilesArray.GetLength(1))
                {
                    // Ne traitez pas la tuile du pion lui-m�me
                    if (i != rowIndex || j != colIndex)
                    {
                        // D�terminez la position relative de la tuile par rapport au pion
                        int rowOffset = i - rowIndex;
                        int colOffset = j - colIndex;


                        if (name == "Kodama" | name == "Kodama Samura" | name == "Koropokkuru")
                        {
                            if (playerCamp)//player 1
                            {
                                if (rowOffset == 1 && colOffset == 0) // Devant
                                {
                                    GameObject frontTile = gameManager.tilesArray[i, j];
                                    if (frontTile.GetComponent<TileV2>().pionOnMe != null)
                                    {
                                        frontTile.GetComponent<TileV2>().GetComponent<Renderer>().material = m_kill;
                                    }
                                    else
                                    {
                                        frontTile.GetComponent<TileV2>().GetComponent<Renderer>().material = m_Good;
                                    }
                                   
                                    // Traitez la tuile devant le pion
                                }
                            } 
                            else//player 2
                            {
                                if (rowOffset == -1 && colOffset == 0) // Derri�re
                                {
                                    GameObject backTile = gameManager.tilesArray[i, j];
                                    if (backTile.GetComponent<TileV2>().pionOnMe != null)
                                    {
                                        backTile.GetComponent<TileV2>().GetComponent<Renderer>().material = m_kill;
                                    }
                                    else
                                    {
                                        backTile.GetComponent<TileV2>().GetComponent<Renderer>().material = m_Good;
                                    }
                                    // Traitez la tuile derri�re le pion
                                }
                            }
                            
                        }
                        // S�lectionnez les tuiles selon les diff�rentes directions
                        /*if (rowOffset == 1 && colOffset == 0) // Devant
                        {
                            GameObject frontTile = gameManager.tilesArray[i, j];
                            frontTile.GetComponent<TileV2>().GetComponent<Renderer>().material = m_Good;
                            // Traitez la tuile devant le pion
                        }
                        else if (rowOffset == 1 && colOffset == -1) // Devant � gauche
                        {
                            GameObject frontLeftTile = gameManager.tilesArray[i, j];
                            frontLeftTile.GetComponent<TileV2>().GetComponent<Renderer>().material = m_Good;
                            // Traitez la tuile devant � gauche du pion
                        }
                        else if (rowOffset == 1 && colOffset == 1) // Devant � droite
                        {
                            GameObject frontRightTile = gameManager.tilesArray[i, j];
                            frontRightTile.GetComponent<TileV2>().GetComponent<Renderer>().material = m_Good;
                            // Traitez la tuile devant � droite du pion
                        }
                        else if (rowOffset == -1 && colOffset == 0) // Derri�re
                        {
                            GameObject backTile = gameManager.tilesArray[i, j];
                            backTile.GetComponent<TileV2>().GetComponent<Renderer>().material = m_Good;
                            // Traitez la tuile derri�re le pion
                        }
                        else if (rowOffset == -1 && colOffset == -1) // Derri�re � gauche
                        {
                            GameObject backLeftTile = gameManager.tilesArray[i, j];
                            backLeftTile.GetComponent<TileV2>().GetComponent<Renderer>().material = m_Good;
                            // Traitez la tuile derri�re � gauche du pion
                        }
                        else if (rowOffset == -1 && colOffset == 1) // Derri�re � droite
                        {
                            GameObject backRightTile = gameManager.tilesArray[i, j];
                            backRightTile.GetComponent<TileV2>().GetComponent<Renderer>().material = m_Good;
                            // Traitez la tuile derri�re � droite du pion
                        }
                        else if (rowOffset == 0 && colOffset == -1) // � gauche
                        {
                            GameObject leftTile = gameManager.tilesArray[i, j];
                            leftTile.GetComponent<TileV2>().GetComponent<Renderer>().material = m_Good;
                            // Traitez la tuile � gauche du pion
                        }
                        else if (rowOffset == 0 && colOffset == 1) // � droite
                        {
                            GameObject rightTile = gameManager.tilesArray[i, j];
                            rightTile.GetComponent<TileV2>().GetComponent<Renderer>().material = m_Good;
                            // Traitez la tuile � droite du pion
                        }*/
                    }
                }
            }
        }
    }

    public void CheckIfTileIsEmpty()//Start and after all 
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.up, out hit))
        {
            // R�cup�re le GameObject de la tile
            GameObject pion = hit.collider.gameObject;

            if (pion != null)
            {
                pionOnMe = pion;

                Debug.Log("Pion sur la tile " + pion);
            }
            else
            {
                pionOnMe = null;

                Debug.LogWarning("Pion component not found on the tile!");
            }
        }
    }
}




