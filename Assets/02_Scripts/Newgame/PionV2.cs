using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.ScrollRect;

public class PionV2 : MonoBehaviour
{
    public GameManager gameManager;
    public PionSO pionSO;

    [SerializeField]
    private bool imPlayer1;

    [SerializeField]
    private GameObject tileUderMe;

    public void Awake()
    {
        if (gameManager == null)
        {
            // Trouver le TileManager dans la scène s'il n'est pas défini
            gameManager = GameObject.FindObjectOfType<GameManager>();

            // Vérifier si le TileManager a été trouvé
            if (gameManager == null)
            {
                Debug.LogError("TileManager not found in the scene!");
                return;
            }
        }
    }

    void OnMouseDown()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit))
        {

            GameObject tile = hit.collider.gameObject;
            
            if (tile != null)
            {
                tile.GetComponent<TileV2>().CheckAdjacentTiles(pionSO.name, imPlayer1);
                gameManager.playerSelected = this.gameObject;

                Debug.Log("Pion sur la tile " + tile );
            }
            else
            {
                Debug.LogWarning("tile component not found on the tile!");
            }
        }

        
    }
}
