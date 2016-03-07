using UnityEngine;
using System.Collections;

public class BottomDuringPlay : MonoBehaviour
{
    float m_startPosition;
    RectTransform m_rectTransform;

	void Awake ()
    {
        m_rectTransform = GetComponent<RectTransform>();

        m_startPosition = m_rectTransform.anchorMin.y;
	}
	
    void OnEnable()
    {
        GameData.GameStateChanged += OnGameStateChanged;
    }

    void OnDisable()
    {
        GameData.GameStateChanged -= OnGameStateChanged;
    }

    private void OnGameStateChanged(GameData.GameState state)
    {
        if (state == GameData.GameState.GameOver)
        {
            SetBottomAnchorY(m_startPosition);
        }
        else
        {
            SetBottomAnchorY(0);
        }
    }

    private void SetBottomAnchorY(float yPos)
    {
        Vector2 newAnchor = new Vector2(m_rectTransform.anchorMin.x, yPos);
        m_rectTransform.anchorMin = newAnchor;
    }
}
