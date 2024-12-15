using TMPro;
using UnityEngine;

public class HelperManager : MonoBehaviour
{
    public TurnManager turn_manager; // Reference to TurnManager
    public TextMeshProUGUI text; // Reference to the TextMeshProUGUI component for displaying tips

    void Start()
    {
        // Find TurnManager and the TextMeshProUGUI component in the scene
        turn_manager = GameObject.Find("TurnManager").GetComponent<TurnManager>();
        text = GameObject.Find("Tip").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTipMessage();
    }

    void UpdateTipMessage()
    {
        // Determine the current player's turn
        string currentPlayer = turn_manager.currentPlayer == TurnManager.Player.Player1 ? "Player 1 (Guard)" : "Player 2 (Invader)";

        // Check if the player has already moved
        if (turn_manager.hasMoved)
        {
            text.text = $"{currentPlayer} has already moved! Pass the turn.";
        }
        else
        {
            text.text = $"{currentPlayer}'s turn. Make your move.";
        }
    }
}