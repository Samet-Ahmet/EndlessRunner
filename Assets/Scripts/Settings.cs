using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public GameObject MenuUI;
    public GameObject SettingsUI;

    void Start()
    {
        MenuUI.SetActive(true);
        SettingsUI.SetActive(false);
    }

    public void OpenSettings()
    {
        MenuUI.SetActive(false);
        SettingsUI.SetActive(true);
    }

    public void Back()
    {
        MenuUI.SetActive(true);
        SettingsUI.SetActive(false);
    }
}
