using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ColorOnState : MonoBehaviour
{
	// Use this for initialization
	void Start ()
    {
        GameData.GameStateChanged += OnStateChanged;

        //Initialize
        OnStateChanged(GameData.gameState);
    }

    void OnStateChanged(GameData.GameState newState)
    {
        switch (newState)
        {
            case GameData.GameState.Unplanted:
                GetComponent<Image>().color = Color.green;
                break;
            case GameData.GameState.Planted:
                GetComponent<Image>().color = Color.red;
                break;
            case GameData.GameState.GameOver:
                GetComponent<Image>().color = Color.white;
                break;
        }
    }
}
