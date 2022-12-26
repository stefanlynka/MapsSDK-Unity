using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Entry : MonoBehaviour
{
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI dateText;
    public TextMeshProUGUI descriptionText;

    public int pinID = -1;

    public void SetText(string text1, string text2, string text3)
    {
        titleText.text = text1;
        dateText.text = text2;
        descriptionText.text = text3;
    }
    public void SetPinID(int newPinID)
    {
        pinID = newPinID;
    }

    public void DestroyThis()
    {
        PinManager.instance.DeletePin(pinID);
    }
}
