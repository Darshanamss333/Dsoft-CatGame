using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiButtonEvents : MonoBehaviour 
{
    public void HorizontalAxisEvent(float value)
    {
        GameManager.Instance.HorizontalAxisDown(value);
    }

    public void VerticalAxisEvent(float value)
    {
        GameManager.Instance.VerticalAxisDown(value);
    }

    public void RightJoysticDownEvent(string _value)
    {
        string button = _value[0].ToString();
        string value = _value[1].ToString();

        int returnButton = 0;
        bool returnValue = false;

        switch (button)
        {
            case "0":
                returnButton = 0;
                break;
            case "1":
                returnButton = 1;
                break;
            case "2":
                returnButton = 2;
                break;
            case "3":
                returnButton = 3;
                break;            
        }

        switch (value)
        {
            case "F":
                returnValue = false;
                break;
            case "T":
                returnValue = true;
                break;
        }

        GameManager.Instance.RightJoysticDown(returnButton, returnValue);
    }

    public void ShowMapDownEvent()
    {
        GameManager.Instance.ShowMap();
    }

    public void ShowInventoryDownEvent()
    {
        GameManager.Instance.ShowInventory();
    }

    public void ShowControlsPanelDownEvent()
    {
        GameManager.Instance.ShowControll();
    }
}
