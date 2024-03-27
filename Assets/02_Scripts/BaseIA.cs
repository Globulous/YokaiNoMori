using UnityEngine;
using System.Collections.Generic;

public class BaseIA : MonoBehaviour
{
    public TileManager tileManager;
    public int maxDepth = 3; // Profondeur maximale pour la recherche Minimax

    private Vector3 pionOriginalPosition; // Variable pour stocker la position d'origine du pion
    private bool destinationOriginalState; // Variable pour stocker l'�tat d'origine de la tuile de destination

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

    private void UndoMove(GameObject pion, GameObject destination)
    {
        // Remettre le pion sur sa tuile d'origine
        pion.transform.position = pionOriginalPosition;

        // Restaurer l'�tat de la tuile de destination
        destination.GetComponent<Tile>().imEmpty = destinationOriginalState;
    }


    private GameState SaveGameState()
    {
        // Cr�er un nouvel objet GameState pour stocker l'�tat actuel du jeu
        GameState gameState = new GameState();

        // Enregistrer les positions actuelles des pions
        foreach (Pion pion in tileManager.pions)
        {
            gameState.pionPositions.Add(pion.gameObject.transform.position);
        }

        // Enregistrer l'�tat actuel de chaque tuile (occup�e ou non)
        foreach (Tile tile in tileManager.tiles)
        {
            gameState.tileStates.Add(tile.imEmpty);
        }

        return gameState;
    }

    public class GameState
    {
        public List<Vector3> pionPositions; // Positions des pions
        public List<bool> tileStates; // �tat des tuiles (occup�es ou non)

        public GameState()
        {
            pionPositions = new List<Vector3>();
            tileStates = new List<bool>();
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
        // Sauvegarder la position d'origine du pion
        pionOriginalPosition = pion.transform.position;

        // Sauvegarder l'�tat d'origine de la tuile de destination
        destinationOriginalState = destination.GetComponent<Tile>().imEmpty;

        // D�placer le pion vers la tuile de destination
        // Impl�mentez votre logique de d�placement ici
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
        float kingSafetyValue = 5.0f;
        float dangerValue = -1.0f; // Valeur n�gative pour �tre en danger
        float captureValue = 1.5f; // Valeur pour capturer une pi�ce

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

        // Ajouter des points pour chaque pion qui peut �tre captur� par l'adversaire
        foreach (Pion pion in tileManager.pionPlayerTop)
        {
            if (IsInDanger(pion))
            {
                score += dangerValue;
            }
        }

        // Ajouter des points pour chaque pion qui peut �tre captur� par le joueur
        foreach (Pion pion in tileManager.pionPlayerBot)
        {
            if (IsInDanger(pion))
            {
                score -= dangerValue;
            }
        }
        // Ajouter des points pour chaque pi�ce que l'IA peut capturer
        List<GameObject> possibleCaptures = FindPossibleCaptures();
        score += possibleCaptures.Count * captureValue;

        return score;
    }
    private bool IsInDanger(Pion pion)
    {
        // R�cup�rer la tuile sur laquelle se trouve le pion
        Tile tile = pion.tiles.GetComponent<Tile>();

        // V�rifier les tuiles adjacentes pour la pr�sence d'une pi�ce ennemie
        foreach (Tile adjacentTile in tileManager.GetAdjacentTiles(tile))
        {
            if (adjacentTile.pionOnMe != null && adjacentTile.pionOnMe.GetComponent<Pion>().bot != pion.bot)
            {
                // Le pion est en danger s'il y a une pi�ce ennemie adjacente
                return true;
            }
        }

        // Le pion n'est pas en danger s'il n'y a pas de pi�ce ennemie adjacente
        return false;
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

    private List<GameObject> FindPossibleCaptures()
    {
        List<GameObject> possibleCaptures = new List<GameObject>();

        // Obtenir tous les pions contr�l�s par l'IA
        List<Pion> pionsIA = GetPionsIA();

        // Parcourir chaque pion contr�l� par l'IA
        foreach (Pion pion in pionsIA)
        {
            // Obtenir les tuiles disponibles pour ce pion
            List<GameObject> tilesAvailable = GetAvailableTiles(pion);

            // Parcourir chaque tuile disponible pour ce pion
            foreach (GameObject tileObject in tilesAvailable)
            {
                // V�rifier s'il y a une pi�ce ennemie sur cette tuile
                Tile tile = tileObject.GetComponent<Tile>();
                if (tile.pionOnMe != null && tile.pionOnMe.GetComponent<Pion>().bot != pion.bot)
                {
                    // Ajouter cette tuile � la liste des captures possibles
                    possibleCaptures.Add(tileObject);
                }
            }
        }

        return possibleCaptures;
    }


    private float KingSafety(GameObject roi)
    {
        // R�cup�rer la tuile sur laquelle se trouve le roi
        Tile tile = roi.GetComponent<Pion>().tiles.GetComponent<Tile>();

        // Initialiser le score de s�curit� du roi
        float safetyScore = 1.0f;

        // V�rifier les menaces directes sur le roi
        foreach (Tile adjacentTile in tileManager.GetAdjacentTiles(tile))// JE VERIFIE ICI OU SONT LES CASES ADJACENTES AUTOUR DE MOIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIII
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
    }




    // IF CAPTURE POSSIBLE = ROI -> OBLIGATOIREMENT CAPTURER LUI
}
