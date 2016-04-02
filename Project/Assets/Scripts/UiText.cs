using UnityEngine;
using UnityEngine.UI;

public class UiText : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    private string m_plantingText, m_defusingText, m_idlePlantText, m_idleDefuseText, m_gameOver;
#pragma warning restore 0649

    private Text m_textComponent;

    void Awake()
    {
        m_textComponent = GetComponent<Text>();
    }

	// Use this for initialization
	void Start ()
    {
        InputListener.ButtonDown += SetTextOnDown;
        InputListener.ButtonUp += SetTextOnUp;
        GameData.GameStateChanged += OnGameStateChanged;

        SetTextOnUp();
    }

    private void SetTextOnDown()
    {
        Debug.Log("SetTextOnDown: " + name);
        switch (GameData.gameState)
        {
            case GameData.GameState.Unplanted:
                m_textComponent.text = m_plantingText;
                break;
            case GameData.GameState.Planted:
                m_textComponent.text = m_defusingText;
                break;
            case GameData.GameState.GameOver:
                m_textComponent.text = m_gameOver;
                break;
        }
    }

    private void SetTextOnUp()
    {
        Debug.Log("SetTextOnUp: " + name);
        switch (GameData.gameState)
        {
            case GameData.GameState.Unplanted:
                m_textComponent.text = m_idlePlantText;
                break;
            case GameData.GameState.Planted:
                m_textComponent.text = m_idleDefuseText;
                break;
            case GameData.GameState.GameOver:
                m_textComponent.text = m_gameOver;
                break;
        }
    }

    private void OnGameStateChanged(GameData.GameState newState)
    {
        SetTextOnUp();
    }
}
