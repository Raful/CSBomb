using UnityEngine;
using System.Collections;
using System;

public class InputListener : MonoBehaviour
{
    public static bool MainButtonIsDown { get; private set; }
    
    /// <summary>
    /// Called when the button gets pressed/released
    /// </summary>
    public static Action ButtonDown, ButtonUp;

    public static void InterruptMainButton()
    {
        OnButtonUpInternal();
    }

    /// <summary>
    /// Should be called from Unity's UI system
    /// </summary>
    public void OnButtonDown()
    {
        Debug.Log("OnButtonDown Listener");
        MainButtonIsDown = true;
        if (ButtonDown != null) ButtonDown();
    }

    /// <summary>
    /// Should be called from Unity's UI system
    /// </summary>
    public void OnButtonUp()
    {
        Debug.Log("OnButtonUp Listener");
        OnButtonUpInternal();
    }

    private static void OnButtonUpInternal()
    {
        MainButtonIsDown = false;
        if (ButtonUp != null) ButtonUp();
    }
}
