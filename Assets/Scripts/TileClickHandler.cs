using System;
using System.Collections;
using UnityEngine;

public class TileClickHandler : MonoBehaviour
{
    public GameObject player1_guard_capsule; // Reference to the capsule in the scene
    public GameObject player2_invasor_capsule; // Reference to the capsule in the scene
    
    public TurnManager turn_manager;
    private static bool isMoving = false; // Track if the capsule is moving

    void Start()
    {
        player1_guard_capsule = GameObject.Find("Capsule_Player1_Guard").gameObject;
        player2_invasor_capsule = GameObject.Find("Capsule_Player2_Invasor").gameObject;
        
        turn_manager = GameObject.Find("TurnManager").GetComponent<TurnManager>();
    }

    private void OnMouseOver()
    {
        // Check for right mouse button click
        if (Input.GetMouseButtonDown(1) && !isMoving && !turn_manager.hasMoved) // 1 = Right Mouse Button
        {
            MoveCapsuleToTile();
        }
    }

    private void MoveCapsuleToTile()
    {
        GameObject capsuleToMove = null;

        // Determine which capsule to move based on the current player
        if (turn_manager.currentPlayer == TurnManager.Player.Player1)
        {
            capsuleToMove = player1_guard_capsule;
        }
        else if (turn_manager.currentPlayer == TurnManager.Player.Player2)
        {
            capsuleToMove = player2_invasor_capsule;
        }
        
        if (capsuleToMove != null && !isMoving)
        {
            isMoving = true; // Block further clicks
            Vector3 targetPosition = transform.position;
            targetPosition.y = 0.3f; // Ensure y remains at 0.3
            StartCoroutine(SmoothMove(capsuleToMove.transform, targetPosition, 0.5f));
            
            turn_manager.PlayerMoved();
        }
    }

    private IEnumerator SmoothMove(Transform objectToMove, Vector3 targetPosition, float duration)
    {
        Vector3 startPosition = objectToMove.position;
        startPosition.y = 0.3f; // Ensure consistent y for start
        targetPosition.y = 0.3f; // Ensure consistent y for target

        float elapsed = 0f;

        while (elapsed < duration)
        {
            objectToMove.position = Vector3.Lerp(startPosition, targetPosition, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        objectToMove.position = targetPosition;

        isMoving = false; // Unlock clicks once the movement is complete
    }
}