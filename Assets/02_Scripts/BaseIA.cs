using UnityEngine;
using System.Collections.Generic;

public class BaseIA : MonoBehaviour
{
   /* public TileManager tileManager;
    public int maxDepth = 3; // Profondeur maximale pour la recherche Minimax

    private void Start()
    {
        tileManager = FindObjectOfType<TileManager>();
    }

    public void PlayTurn()
    {
        // Obtenir tous les pions contr�l�s par l'IA
        List<Pion> pionsIA = GetPionsIA();

        // Initialiser les valeurs alpha et beta
        float alpha = Mathf.NegativeInfinity;
        float beta = Mathf.Infinity;

        // Pour chaque pion, �valuer les mouvements possibles et choisir le meilleur
        foreach (Pion pion in pionsIA)
        {
            EvaluateAndPlayBestMove(pion, alpha, beta, 0);
        }
    }

    private List<Pion> GetPionsIA()
    {
        List<Pion> pionsIA = new List<Pion>();

        foreach (Pion pion in tileManager.pionPlayerBot)
        {
            pionsIA.Add(pion);
        }

        return pionsIA;
    }

    private void EvaluateAndPlayBestMove(Pion pion, float alpha, float beta, int depth)
    {
        List<GameObject> tilesAvailable = GetAvailableTiles(pion);
        GameObject bestTile = null;
        float bestScore = Mathf.NegativeInfinity;

        foreach (GameObject tileObject in tilesAvailable)
        {
            // Faire une copie temporaire de l'�tat du jeu pour simuler le mouvement
            GameState gameState = SaveGameState();
            
            // Appliquer le mouvement au plateau de jeu
            MovePion(pion.gameObject, tileObject);

            // �valuer le score pour ce mouvement en utilisant une fonction d'�valuation appropri�e
            float score = EvaluatePosition();

            // Annuler le mouvement pour revenir � l'�tat pr�c�dent du jeu
            UndoMove(pion.gameObject, tileObject);

            // Si vous atteignez la profondeur maximale ou si le jeu est termin�, �valuez le score et arr�tez la r�cursion
            if (depth == maxDepth || IsGameFinished())
            {
                if (score > bestScore)
                {
                    bestScore = score;
                    bestTile = tileObject;
                }
            }
            else
            {
                // Appliquer r�cursivement Minimax avec �lagage Alpha-Beta pour chaque mouvement possible
                score = -AlphaBetaPruning(alpha, beta, depth + 1);
                if (score > bestScore)
                {
                    bestScore = score;
                    bestTile = tileObject;
                }
                alpha = Mathf.Max(alpha, bestScore);
                if (alpha >= beta)
                    break; // �lagage Alpha-Beta
            }
        }

        // Effectuer le meilleur mouvement trouv�
        if (bestTile != null)
        {
            MovePion(pion.gameObject, bestTile);
        }
    }


        // Effectuer le meilleur mouvement trouv�
        if (bestTile != null)
        {
            MovePion(pion.gameObject, bestTile);
        }
    }

    private float AlphaBetaPruning(float alpha, float beta, int depth)
    {
        if (depth == maxDepth || IsGameFinished())
        {
            return EvaluatePosition();
        }

        List<Pion> pionsIA = GetPionsIA();
        foreach (Pion pion in pionsIA)
        {
            List<GameObject> tilesAvailable = GetAvailableTiles(pion);
            foreach (GameObject tileObject in tilesAvailable)
            {
                // Appliquer le mouvement au plateau de jeu
                // �valuer le score pour ce mouvement en utilisant une fonction d'�valuation appropri�e
                float score = -AlphaBetaPruning(-beta, -alpha, depth + 1);
                if (score >= beta)
                {
                    return beta; // �lagage Beta
                }
                if (score > alpha)
                {
                    alpha = score;
                }
            }
        }
        return alpha;
    }

    private List<GameObject> GetAvailableTiles(Pion pion)
    {
        List<GameObject> tilesAvailable = new List<GameObject>();

        switch (pion.pionSO.pionName)
        {
            case "Kitsune":
                AddAvailableTile(pion.tiles.GetComponent<Tile>().tileForwardLeft);
                AddAvailableTile(pion.tiles.GetComponent<Tile>().tileForwardRight);
                AddAvailableTile(pion.tiles.GetComponent<Tile>().tileBackLeft);
                AddAvailableTile(pion.tiles.GetComponent<Tile>().tileBackRight);
                break;

            case "Kodama":
                if (pion.transformation)
                {
                    // Pour Kodama Samura�, tous les mouvements sont autoris�s sauf arri�re droit et arri�re gauche
                    AddAvailableTile(pion.tiles.GetComponent<Tile>().tileForward);
                    AddAvailableTile(pion.tiles.GetComponent<Tile>().tileForwardLeft);
                    AddAvailableTile(pion.tiles.GetComponent<Tile>().tileForwardRight);
                    AddAvailableTile(pion.tiles.GetComponent<Tile>().tileLeft);
                    AddAvailableTile(pion.tiles.GetComponent<Tile>().tileRight);
                    AddAvailableTile(pion.tiles.GetComponent<Tile>().tileBack);
                }
                else
                {
                    // Pour Kodama, le mouvement est uniquement vers l'avant
                    AddAvailableTile(pion.bot ? pion.tiles.GetComponent<Tile>().tileForward : pion.tiles.GetComponent<Tile>().tileBack);
                }
                break;

            case "Koropokkuru":
                // Le Koropokkuru peut se d�placer dans toutes les directions
                AddAvailableTile(pion.tiles.GetComponent<Tile>().tileForward);
                AddAvailableTile(pion.tiles.GetComponent<Tile>().tileBack);
                AddAvailableTile(pion.tiles.GetComponent<Tile>().tileLeft);
                AddAvailableTile(pion.tiles.GetComponent<Tile>().tileRight);
                AddAvailableTile(pion.tiles.GetComponent<Tile>().tileForwardLeft);
                AddAvailableTile(pion.tiles.GetComponent<Tile>().tileForwardRight);
                AddAvailableTile(pion.tiles.GetComponent<Tile>().tileBackLeft);
                AddAvailableTile(pion.tiles.GetComponent<Tile>().tileBackRight);
                break;

            case "Tanuki":
                // Le Tanuki peut se d�placer vers l'avant, l'arri�re, � droite et � gauche
                AddAvailableTile(pion.tiles.GetComponent<Tile>().tileForward);
                AddAvailableTile(pion.tiles.GetComponent<Tile>().tileBack);
                AddAvailableTile(pion.tiles.GetComponent<Tile>().tileLeft);
                AddAvailableTile(pion.tiles.GetComponent<Tile>().tileRight);
                break;

            // Ajoutez des cas suppl�mentaires pour d'autres pions si n�cessaire

            default:
                Debug.LogWarning("Pion inconnu : " + pion.pionSO.pionName);
                break;
        }

        return tilesAvailable;

        // Fonction pour ajouter une tuile disponible � la liste
        void AddAvailableTile(GameObject tile)
        {
            // V�rifiez si la tuile existe et est vide
            if (tile != null && tile.GetComponent<Tile>().imEmpty)
            {
                tilesAvailable.Add(tile);
            }
        }
    }

    private void MovePion(GameObject pion, GameObject destination)
    {
        // Impl�mentez la logique pour d�placer le pion vers la tuile de destination
    }

    private bool IsGameFinished()
    {
        if (tileManager.roiP1.GetComponent<Pion>().isDead || tileManager.roiP2.GetComponent<Pion>().isDead)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private float EvaluatePosition()
    {
        // Valeurs arbitraires pour l'�valuation
        float pieceValue = 1.0f;
        float kingSafetyValue = 2.0f;

        float score = 0.0f;

        // �valuer le nombre de pi�ces restantes pour chaque joueur
        int piecesPlayer1 = CountPieces(tileManager.pionPlayerTop);
        int piecesPlayer2 = CountPieces(tileManager.pionPlayerBot);

        // Attribuer des points en fonction du nombre de pi�ces restantes
        score += (piecesPlayer1 - piecesPlayer2) * pieceValue;

        // �valuer la s�curit� des rois
        float kingSafetyPlayer1 = KingSafety(tileManager.roiP1);
        float kingSafetyPlayer2 = KingSafety(tileManager.roiP2);

        // Attribuer des points en fonction de la s�curit� des rois
        score += (kingSafetyPlayer1 - kingSafetyPlayer2) * kingSafetyValue;

        return score;
    }

    private int CountPieces(List<Pion> pions)
    {
        int count = 0;
        foreach (Pion pion in pions)
        {
            if (!pion.isDead)
            {
                count++;
            }
        }
        return count;
    }

    private float KingSafety(GameObject roi)
    {
        // R�cup�rer la tuile sur laquelle se trouve le roi
        Tile tile = roi.GetComponent<Pion>().tiles.GetComponent<Tile>();

        // Initialiser le score de s�curit� du roi
        float safetyScore = 1.0f;

        // V�rifier les menaces directes sur le roi
        foreach (Tile adjacentTile in tileManager.GetAdjacentTiles(tile))
        {
            if (adjacentTile.pionOnMe != null && adjacentTile.pionOnMe.GetComponent<Pion>().bot != tileManager.turnbot)
            {
                // Le roi est menac� par une pi�ce adverse adjacente, r�duire le score de s�curit�
                safetyScore -= 0.2f;
            }
        }

        // V�rifier la protection par d'autres pi�ces
        int friendlyPieces = 0;
        foreach (Tile adjacentTile in tileManager.GetAdjacentTiles(tile))
        {
            if (adjacentTile.pionOnMe != null && adjacentTile.pionOnMe.GetComponent<Pion>().bot == tileManager.turnbot)
            {
                // La pi�ce adjacente est amie, augmenter le nombre de pi�ces amies
                friendlyPieces++;
            }
        }

        // Ajouter des points de s�curit� en fonction du nombre de pi�ces amies adjacents
        safetyScore += friendlyPieces * 0.1f;

        // Retourner le score de s�curit� normalis� entre 0 et 1
        return Mathf.Clamp01(safetyScore);
    }*/


}
