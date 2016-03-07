using System;
using UnityEngine;

public class GameData : MonoBehaviour
{
    const string
        MAX_UNPLANTED_KEY = "MaxUnplantedTime",
        MAX_PLANTED_KEY = "MaxPlantedTime";

    public enum GameState
    {
        Unplanted,
        Planted,
        GameOver
    }

    public static float gameTimer { get; private set; }

    private static float m_maxUnplantedTime;
    public static float maxUnplantedTime
    {
        get
        {
            return m_maxUnplantedTime;
        }
        set
        {
            m_maxUnplantedTime = value;
            PlayerPrefs.SetFloat(MAX_UNPLANTED_KEY, value);
        }
    }

    private static float m_maxPlantedTime;
    public static float maxPlantedTime
    {
        get
        {
            return m_maxPlantedTime;
        }
        set
        {
            m_maxPlantedTime = value;
            PlayerPrefs.SetFloat(MAX_PLANTED_KEY, value);
        }
    }
    public static float maxTime
    {
        get
        {
            if (gameState == GameState.Planted)
                return maxPlantedTime;
            else
                return maxUnplantedTime;
        }
    }

    public static bool isMaxTimeSet
    {
        get
        {
            return maxTime > 0.0001f;
        }
    }


    private static GameState m_gameState = GameState.GameOver;
    public static GameState gameState
    {
        get { return m_gameState; }
        set
        {
            m_gameState = value;
            if (GameStateChanged != null) GameStateChanged(value);
        }
    }

    public static event Action<GameState> GameStateChanged;
    
    private void Awake()
    {
        m_maxUnplantedTime = PlayerPrefs.GetFloat(MAX_UNPLANTED_KEY, 0);
        m_maxPlantedTime = PlayerPrefs.GetFloat(MAX_PLANTED_KEY, 0);
    }

    private void Start()
    {
        GameStateChanged += (state) =>
        {
            Debug.Log("New game state: " + state);
            gameTimer = 0;
        };
    }

    private void Update()
    {
        if (m_gameState != GameState.GameOver)
        {
            gameTimer += Time.deltaTime;

            if (isMaxTimeSet)
            {
                if (maxTime - gameTimer <= 0)
                {
                    SoundHandler.HandleTimerExpired();
                    gameState = GameState.GameOver;
                }
            }
        }
    }
}
