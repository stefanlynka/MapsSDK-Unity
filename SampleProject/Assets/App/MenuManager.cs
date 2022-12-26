using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject newPinButton;
    public GameObject homePage;
    public GameObject reticle;

    private void Start()
    {
        LoadHome();
    }

    public void LoadMap()
    {
        newPinButton.SetActive(true);
        reticle.SetActive(true);
        homePage.SetActive(false);
    }
    public void LoadHome()
    {
        newPinButton.SetActive(false);
        reticle.SetActive(false);
        homePage.SetActive(true);
    }
}
