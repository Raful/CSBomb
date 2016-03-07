using UnityEngine;
using System.Collections;

public class GameStarter : MonoBehaviour
{
	// Use this for initialization
	void OnEnable ()
    {
        if (GameData.gameState == GameData.GameState.GameOver)
        {
            InputListener.ButtonDown += StartGame;
        }

        GameData.GameStateChanged += OnGameStateChanged;
    }

    void OnDisable()
    {
        InputListener.ButtonDown -= StartGame;
    }

    private void StartGame()
    {
        InputListener.ButtonDown -= StartGame;
        GameData.gameState = GameData.GameState.Unplanted;
    }

    private void OnGameStateChanged(GameData.GameState newState)
    {
        if (newState == GameData.GameState.GameOver)
        {
            InputListener.ButtonDown += StartGame;
        }
        else
        {
            InputListener.ButtonDown -= StartGame;
        }
    }
}
