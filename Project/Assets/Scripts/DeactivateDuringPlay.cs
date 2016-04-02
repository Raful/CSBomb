using UnityEngine;
using System.Collections;

public class DeactivateDuringPlay : MonoBehaviour
{
    //Handling GameData.GameStateChanged in Awake and OnDestroy because the script will disable itself
    void Awake ()
    {
        GameData.GameStateChanged += OnGameStateChanged;
	}

    void OnDestroy()
    {
        GameData.GameStateChanged -= OnGameStateChanged;
    }
	
    private void OnGameStateChanged(GameData.GameState state)
    {
        if (state == GameData.GameState.GameOver)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
