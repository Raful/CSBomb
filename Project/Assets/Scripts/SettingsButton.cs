using UnityEngine;

public class SettingsButton : MonoBehaviour
{
    [SerializeField]
    GameObject m_gameCanvas;

    public void OnClick()
    {
        MenuManager.ShowMenu(MenuManager.Menu.Settings);
    }
}
