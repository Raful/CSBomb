using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PrintGameTimer : MonoBehaviour
{
    Text m_textComponent;

    private void Awake()
    {
        m_textComponent = GetComponent<Text>();
    }

	// Update is called once per frame
	void Update ()
    {
        float time;
        if (GameData.isMaxTimeSet)
        {
            time = GameData.maxTime - GameData.gameTimer;
        }
        else
        {
            time = GameData.gameTimer;
        }

        int minutes = (int)time / 60;
        int seconds = (int)time % 60;
        
        m_textComponent.text = minutes + ":" + seconds.ToString("00");
    }
}
