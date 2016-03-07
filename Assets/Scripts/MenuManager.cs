using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour
{
    public enum Menu
    {
        Settings,
        Game
    }

    [SerializeField]
    GameObject 
        m_gameMenu,
        m_settingsMenu;

    private static MenuManager instance;

    public static void ShowMenu(Menu menu)
    {
        instance.m_gameMenu.SetActive(false);
        instance.m_settingsMenu.SetActive(false);

        switch (menu)
        {
            case Menu.Game:
                instance.m_gameMenu.SetActive(true);
                break;
            case Menu.Settings:
                instance.m_settingsMenu.SetActive(true);
                break;
        }
    }

    void Awake()
    {
        Debug.Assert(instance == null, "Multiple instances of MenuManager found.");

        instance = this;
    }

    void Start()
    {
        ShowMenu(Menu.Game);
    }
}
