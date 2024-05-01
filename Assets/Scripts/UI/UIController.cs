using System;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static GameSceneData sceneData;
    private static UIController _instance;
    public static UIController Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("UIController instance is not initialized");
            }
            return _instance;
        }
    }

    [SerializeField] private HUDController hud;
    [SerializeField] private GUIController gui;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Debug.LogError("Another instance of UIController already exists!");
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    // Static properties to access HUD and GUI
    public static HUDController HUD
    {
        get
        {
            if (Instance == null)
            {
                return null;
            }
            return Instance.hud;
        }
    }

    public static GUIController GUI
    {
        get
        {
            if (Instance == null)
            {
                return null;
            }
            return Instance.gui;
        }
    }


}
