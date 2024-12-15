using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public enum Player { Player1, Player2 }
    public Player currentPlayer;

    public bool hasMoved = false; // Tracks if the current player has moved during their turn
    
    public Camera player_1_camera;
    public Camera player_2_camera;
    
    void Start()
    {
        // Find and assign the cameras
        player_1_camera = GameObject.Find("Camera_Player1_Guard").GetComponent<Camera>();
        player_2_camera = GameObject.Find("Camera_Player2_Invasor").GetComponent<Camera>();
        player_2_camera.enabled = false;

        // Ensure cameras are set up correctly at the start
        currentPlayer = Player.Player1;
        UpdateActiveCamera();
        Debug.Log($"Start Game: {currentPlayer}'s turn!");
    }

    public void EndTurn()
    {
        hasMoved = false; // Reset move status

        // Toggle between Player1 and Player2
        currentPlayer = currentPlayer == Player.Player1 ? Player.Player2 : Player.Player1;

        // Update the active camera based on the current player
        UpdateActiveCamera();

        Debug.Log($"Turn Ended. It's now {currentPlayer}'s turn.");
    }

    public void OnPassTurnButtonClicked()
    {
        EndTurn();
    }

    private void UpdateActiveCamera()
    {
        if (currentPlayer == Player.Player1)
        {
            player_1_camera.enabled = true;
            player_2_camera.enabled = false;
        }
        else if (currentPlayer == Player.Player2)
        {
            player_1_camera.enabled = false;
            player_2_camera.enabled = true;
        }
    }
    
    public void PlayerMoved()
    {
        hasMoved = true;
        Debug.Log($"{currentPlayer} has moved this turn.");
    }
}