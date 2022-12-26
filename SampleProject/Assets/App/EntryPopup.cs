using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EntryPopup : MonoBehaviour
{
    public TMP_InputField titleText;
    public TMP_InputField dateText;
    public TMP_InputField descriptionText;

    public PinManager pinManager;

    private string defaultTitle = "Title";
    private string defaultDate = "Date";
    private string defaultDesc = "Enter your Description here";

    // Start is called before the first frame update
    void Start()
    {
        //gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Complete()
    {
        pinManager.CreatePinAtCenter(titleText.text, dateText.text, descriptionText.text);
        Cancel();
    }

    public void Cancel()
    {
        titleText.text = defaultTitle;
        dateText.text = defaultDate;
        descriptionText.text = defaultDesc;
        gameObject.SetActive(false);
    }
}
