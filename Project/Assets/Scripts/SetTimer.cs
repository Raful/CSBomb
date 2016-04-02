using UnityEngine;
using UnityEngine.UI;

public class SetTimer : MonoBehaviour
{
    [SerializeField]
    bool m_isPlantedTimer;

    private InputField m_inputField;
    private string m_lastValidInput;

    void Awake()
    {
        m_inputField = GetComponent<InputField>();
        m_lastValidInput = TimeToString(0);
    }

    void Start()
    {
        if (m_isPlantedTimer)
        {
            m_inputField.text = TimeToString((int)GameData.maxPlantedTime);
        }
        else
        {
            m_inputField.text = TimeToString((int)GameData.maxUnplantedTime);
        }
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
            m_inputField.enabled = true;
        }
        else
        {
            m_inputField.enabled = false;
        }
    }

    public void OnValueChanged()
    {
        Debug.Log("OnValueChanged");
        if (IsValidInput(m_inputField.text))
        {
            m_lastValidInput = m_inputField.text;
        }
        else
        {
            m_inputField.text = m_lastValidInput;
        }
    }

    public void OnEdit()
    {
        Debug.Log("OnEdit");
        //Replace . with :
        m_inputField.text = m_inputField.text.Replace('.', ':');
        
        string[] splitTime = m_inputField.text.Split(':');

        //There needs to be atmost one separator (i.e. 2 strings after separating)
        if (splitTime.Length > 2)
        {
            ResetInput();
        }
        
        //The times set can not be empty
        foreach (string s in splitTime)
        {
            if (string.IsNullOrEmpty(s))
            {
                ResetInput();
            }
        }

        int minutes;
        int seconds;
        if (splitTime.Length == 1)
        {
            //No separators, assume everything is in seconds
            minutes = 0;
            seconds = int.Parse(splitTime[0]);
        }
        else
        {
            //A separator exists, parse minutes and seconds
            minutes = int.Parse(splitTime[0]);
            seconds = int.Parse(splitTime[1]);
        }

        //Convert overflowing seconds (>=60) to minutes
        minutes += seconds / 60;
        seconds = seconds % 60;

        int timeInSeconds = minutes * 60 + seconds;

        //Ensure proper format
        m_inputField.text = TimeToString(timeInSeconds);

        //Apply to GameData
        if (m_isPlantedTimer)
            GameData.maxPlantedTime = timeInSeconds;
        else
            GameData.maxUnplantedTime = timeInSeconds;
    }

    public void ResetInput()
    {
        Debug.Log("ResetInput");
        TimeToString(0);
    }

    private bool IsValidInput(string input)
    {
        int separatorCount = 0;
        foreach (char c in input)
        {
            if (!IsValidInput(c))
                return false;

            if (c == ':' || c == '.')
            {
                separatorCount++;
            }
        }

        //There can only be one separator
        if (separatorCount > 1)
            return false;

        //Passed all tests
        return true;
    }

    private bool IsValidInput(char input)
    {
        //Check for separators
        if (input == ':' || input == '.')
            return true;

        //Check for numbers
        int dummy;
        if (int.TryParse(input.ToString(), out dummy))
            return true;

        //None of the checks were true
        return false;
    }

    private string TimeToString(int secondsToParse)
    {
        //Convert seconds into minutes and seconds
        int minutes = secondsToParse / 60;
        int seconds = secondsToParse % 60;

        return minutes + ":" + seconds.ToString("00");
    }
}
