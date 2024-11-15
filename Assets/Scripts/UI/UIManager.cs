using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set;}

    [SerializeField] private GameObject 
        MainMenu,
        GameMenu,
        WinMenu,
        LoseMenu;

    [SerializeField] private PlayerUIControl playerUIControl;
    void Awake(){
        Instance = this;
    }

    public void ShowMainMenu(){
        MainMenu.SetActive(true);
        playerUIControl.Init();
    }

    public void HideMainMenu(){
        MainMenu.SetActive(false);
    }

    public void ShowGameMenu(){
        GameMenu.SetActive(true);
    }
    public void HideGameMenu(){
        GameMenu.SetActive(false);
    }

    public void ShowWinMenu(){
        WinMenu.SetActive(true);
    }

    public void HideWinMenu(){
        WinMenu.SetActive(false);
    }

    public void ShowLoseMenu(){
        LoseMenu.SetActive(true);
    }

    public void HideLoseMenu(){
        LoseMenu.SetActive(false);
    }
}
