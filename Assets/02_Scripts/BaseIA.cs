using UnityEngine;
using System.Collections.Generic;

public class BaseIA : MonoBehaviour
{
    public TileManager tileManager;
    public int maxDepth = 3; // Profondeur maximale pour la recherche Minimax

    private Vector3 pionOriginalPosition; // Variable pour stocker la position d'origine du pion
    private bool destinationOriginalState; // Variable pour stocker l'état d'origine de la tuile de destination

    private void Start()
    {
        tileManager = FindObjectOfType<TileManager>();
    }

    public void PlayTurn()
    {
        // Obtenir tous les pions contrôlés par l'IA
        List<Pion> pionsIA = GetPionsIA();

        // Initialiser les valeurs alpha et beta
        float alpha = Mathf.NegativeInfinity;
        float beta = Mathf.Infinity;

        // Pour chaque pion, évaluer les mouvements possibles et choisir le meilleur
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
            // Faire une copie temporaire de l'état du jeu pour simuler le mouvement
            GameState gameState = SaveGameState();

            // Appliquer le mouvement au plateau de jeu
            MovePion(pion.gameObject, tileObject);

            // Évaluer le score pour ce mouvement en utilisant une fonction d'évaluation appropriée
            float score = EvaluatePosition();

            // Annuler le mouvement pour revenir à l'état précédent du jeu
            UndoMove(pion.gameObject, tileObject);

            // Si vous atteignez la profondeur maximale ou si le jeu est terminé, évaluez le score et arrêtez la récursion
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
                // Appliquer récursivement Minimax avec élagage Alpha-Beta pour chaque mouvement possible
                score = -AlphaBetaPruning(alpha, beta, depth + 1);
                if (score > bestScore)
                {
                    bestScore = score;
                    bestTile = tileObject;
                }
                alpha = Mathf.Max(alpha, bestScore);
                if (alpha >= beta)
                    break; // Élagage Alpha-Beta
            }
        }

        // Effectuer le meilleur mouvement trouvé
        if (bestTile != null)
        {
            MovePion(pion.gameObject, bestTile);
        }
    }

    private void UndoMove(GameObject pion, GameObject destination)
    {
        // Remettre le pion sur sa tuile d'origine
        pion.transform.position = pionOriginalPosition;

        // Restaurer l'état de la tuile de destination
        destination.GetComponent<Tile>().imEmpty = destinationOriginalState;
    }


    private GameState SaveGameState()
    {
        // Créer un nouvel objet GameState pour stocker l'état actuel du jeu
        GameState gameState = new GameState();

        // Enregistrer les positions actuelles des pions
        foreach (Pion pion in tileManager.pions)
        {
            gameState.pionPositions.Add(pion.gameObject.transform.position);
        }

        // Enregistrer l'état actuel de chaque tuile (occupée ou non)
        foreach (Tile tile in tileManager.tiles)
        {
            gameState.tileStates.Add(tile.imEmpty);
        }

        return gameState;
    }

    public class GameState
    {
        public List<Vector3> pionPositions; // Positions des pions
        public List<bool> tileStates; // État des tuiles (occupées ou non)

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
                // Évaluer le score pour ce mouvement en utilisant une fonction d'évaluation appropriée
                float score = -AlphaBetaPruning(-beta, -alpha, depth + 1);
                if (score >= beta)
                {
                    return beta; // Élagage Beta
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
                    // Pour Kodama Samuraï, tous les mouvements sont autorisés sauf arrière droit et arrière gauche
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
                // Le Koropokkuru peut se déplacer dans toutes les directions
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
                // Le Tanuki peut se déplacer vers l'avant, l'arrière, à droite et à gauche
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

        // Fonction pour ajouter une tuile disponible à la liste
        void AddAvailableTile(GameObject tile)
        {
            // Vérifiez si la tuile existe et est vide
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

        // Sauvegarder l'état d'origine de la tuile de destination
        destinationOriginalState = destination.GetComponent<Tile>().imEmpty;

        // Déplacer le pion vers la tuile de destination
        // Implémentez votre logique de déplacement ici
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
        // Valeurs arbitraires pour l'évaluation
        float pieceValue = 1.0f;
        float kingSafetyValue = 5.0f;
        float dangerValue = -1.0f; // Valeur négative pour être en danger
        float captureValue = 1.5f; // Valeur pour capturer une pièce

        float score = 0.0f;

        // Évaluer le nombre de pièces restantes pour chaque joueur
        int piecesPlayer1 = CountPieces(tileManager.pionPlayerTop);
        int piecesPlayer2 = CountPieces(tileManager.pionPlayerBot);

        // Attribuer des points en fonction du nombre de pièces restantes
        score += (piecesPlayer1 - piecesPlayer2) * pieceValue;

        // Évaluer la sécurité des rois
        float kingSafetyPlayer1 = KingSafety(tileManager.roiP1);
        float kingSafetyPlayer2 = KingSafety(tileManager.roiP2);

        // Attribuer des points en fonction de la sécurité des rois
        score += (kingSafetyPlayer1 - kingSafetyPlayer2) * kingSafetyValue;

        // Ajouter des points pour chaque pion qui peut être capturé par l'adversaire
        foreach (Pion pion in tileManager.pionPlayerTop)
        {
            if (IsInDanger(pion))
            {
                score += dangerValue;
            }
        }

        // Ajouter des points pour chaque pion qui peut être capturé par le joueur
        foreach (Pion pion in tileManager.pionPlayerBot)
        {
            if (IsInDanger(pion))
            {
                score -= dangerValue;
            }
        }
        // Ajouter des points pour chaque pièce que l'IA peut capturer
        List<GameObject> possibleCaptures = FindPossibleCaptures();
        score += possibleCaptures.Count * captureValue;

        return score;
    }
    private bool IsInDanger(Pion pion)
    {
        // Récupérer la tuile sur laquelle se trouve le pion
        Tile tile = pion.tiles.GetComponent<Tile>();

        // Vérifier les tuiles adjacentes pour la présence d'une pièce ennemie
        foreach (Tile adjacentTile in tileManager.GetAdjacentTiles(tile))
        {
            if (adjacentTile.pionOnMe != null && adjacentTile.pionOnMe.GetComponent<Pion>().bot != pion.bot)
            {
                // Le pion est en danger s'il y a une pièce ennemie adjacente
                return true;
            }
        }

        // Le pion n'est pas en danger s'il n'y a pas de pièce ennemie adjacente
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

        // Obtenir tous les pions contrôlés par l'IA
        List<Pion> pionsIA = GetPionsIA();

        // Parcourir chaque pion contrôlé par l'IA
        foreach (Pion pion in pionsIA)
        {
            // Obtenir les tuiles disponibles pour ce pion
            List<GameObject> tilesAvailable = GetAvailableTiles(pion);

            // Parcourir chaque tuile disponible pour ce pion
            foreach (GameObject tileObject in tilesAvailable)
            {
                // Vérifier s'il y a une pièce ennemie sur cette tuile
                Tile tile = tileObject.GetComponent<Tile>();
                if (tile.pionOnMe != null && tile.pionOnMe.GetComponent<Pion>().bot != pion.bot)
                {
                    // Ajouter cette tuile à la liste des captures possibles
                    possibleCaptures.Add(tileObject);
                }
            }
        }

        return possibleCaptures;
    }


    private float KingSafety(GameObject roi)
    {
        // Récupérer la tuile sur laquelle se trouve le roi
        Tile tile = roi.GetComponent<Pion>().tiles.GetComponent<Tile>();

        // Initialiser le score de sécurité du roi
        float safetyScore = 1.0f;

        // Vérifier les menaces directes sur le roi
        foreach (Tile adjacentTile in tileManager.GetAdjacentTiles(tile))// JE VERIFIE ICI OU SONT LES CASES ADJACENTES AUTOUR DE MOIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIII
        {
            if (adjacentTile.pionOnMe != null && adjacentTile.pionOnMe.GetComponent<Pion>().bot != tileManager.turnbot)
            {
                // Le roi est menacé par une pièce adverse adjacente, réduire le score de sécurité
                safetyScore -= 0.2f;
            }
        }

        // Vérifier la protection par d'autres pièces
        int friendlyPieces = 0;
        foreach (Tile adjacentTile in tileManager.GetAdjacentTiles(tile))
        {
            if (adjacentTile.pionOnMe != null && adjacentTile.pionOnMe.GetComponent<Pion>().bot == tileManager.turnbot)
            {
                // La pièce adjacente est amie, augmenter le nombre de pièces amies
                friendlyPieces++;
            }
        }
        // Ajouter des points de sécurité en fonction du nombre de pièces amies adjacents
        safetyScore += friendlyPieces * 0.1f;

        // Retourner le score de sécurité normalisé entre 0 et 1
        return Mathf.Clamp01(safetyScore);
    }




    // IF CAPTURE POSSIBLE = ROI -> OBLIGATOIREMENT CAPTURER LUI
}
