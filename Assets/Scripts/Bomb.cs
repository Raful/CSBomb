using UnityEngine;
using System.Collections;
using System;

public class Bomb : MonoBehaviour
{
    [SerializeField]
    private float m_fuseTimer = 1, m_defuseTimer = 1;
    
    private float m_buttonPressedTimer = 0;
    private bool m_isPlaying;

    public static Bomb instance { get; private set; }
    
    /// <summary>
    /// Returns the progress of the current action (0 if there's no action going on)
    /// </summary>
    /// <returns>The progress as a value in [0, 1]</returns>
    public float GetProgress()
    {
        if (InputListener.MainButtonIsDown)
        {
            switch (GameData.gameState)
            {
                case GameData.GameState.Unplanted:
                    return m_buttonPressedTimer / m_fuseTimer;
                case GameData.GameState.Planted:
                    return m_buttonPressedTimer / m_defuseTimer;
            }
        }

        return 0;
    }

    private void Awake()
    {
        Debug.Assert(instance == null, "An instance of Bomb already exists");

        instance = this;
    }

    private void Start()
    {
        GameData.GameStateChanged += OnStateChanged;
        InputListener.ButtonDown += OnButtonDown;

        m_isPlaying = GameData.gameState != GameData.GameState.GameOver;
    }

    private void Update()
    {
        if (InputListener.MainButtonIsDown)
        {
            m_buttonPressedTimer += Time.deltaTime;
            
            switch (GameData.gameState)
            {
                case GameData.GameState.Unplanted:
                    HandleFuseDone();
                    break;
                case GameData.GameState.Planted:
                    HandleDefuseDone();
                    break;
            }
        }
    }

    private void OnButtonDown()
    {
        m_buttonPressedTimer = 0;
    }

    private void HandleFuseDone()
    {
        if (m_buttonPressedTimer >= m_fuseTimer)
        {
            SoundHandler.PlayPlantedSound();
            GameData.gameState = GameData.GameState.Planted;
            InputListener.InterruptMainButton();
        }
    }

    private void HandleDefuseDone()
    {
        if (m_buttonPressedTimer >= m_defuseTimer)
        {
            SoundHandler.PlayDefusedSound();
            GameData.gameState = GameData.GameState.GameOver;
        }
    }

    private void OnStateChanged(GameData.GameState newState)
    {
        InputListener.InterruptMainButton();
        m_buttonPressedTimer = 0;
    }
}
