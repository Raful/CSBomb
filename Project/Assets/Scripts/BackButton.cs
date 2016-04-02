using UnityEngine;
using System.Collections;

public class BackButton : MonoBehaviour
{
    public void OnClick()
    {
        MenuManager.ShowMenu(MenuManager.Menu.Game);
    }
}
