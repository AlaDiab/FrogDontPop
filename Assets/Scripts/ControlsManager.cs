using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public static class ControlsManager
{
    private static InputActionAsset inputActions;

    public static Vector2 Stick(int index)
    {
        if (index == 0)
        {
            // Left Stick
            return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        }
        else if (index == 1)
        {
            // Right Stick
            return new Vector2(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"));
        }
        else if (index == 2)
        {
            // DPad
            return new Vector2(Input.GetAxis("DHorizontal"), Input.GetAxis("DVertical"));
        }
        else if (index == 3)
        {
            // ZR and ZL
            return new Vector2(Input.GetAxis("ZR"), Input.GetAxis("ZL"));
        }
        return Vector2.zero;
    }

    public static void Button()
    {
        string[] allButtons = new string[] { "A", "B", "X", "Y", "R", "L", "ZR", "ZL", "Start", "Select", "LeftStickButton", "RightStickButton", "Up" };
        for (int i = 0; i < allButtons.Length; i++)
        {
            if (Button(allButtons[i]))
            {
                Debug.Log(allButtons[i] + " Pressed");
                return;
            }
        }

        Debug.Log("No Button Pressed");
    }

    public static bool Button(string name)
    {
        return Input.GetButton(name);
    }

    public static bool ButtonDown(string name)
    {
        return Input.GetButtonDown(name);
    }

    public static bool ButtonUp(string name)
    {
        return Input.GetButtonUp(name);
    }
}
