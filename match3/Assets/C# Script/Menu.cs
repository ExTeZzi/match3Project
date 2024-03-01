using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject buttonsNewGame;
    public GameObject buttonsOptions;
    public GameObject buttonsExit;
    public GameObject buttonsOption;
    public GameObject buttonsBackInMenu;



    public void BackInMenu()
    {
        buttonsExit.SetActive(true);
        buttonsOptions.SetActive(true);
        buttonsNewGame.SetActive(true);
        buttonsOption.SetActive(false);
        buttonsBackInMenu.SetActive(false);

    }

    public void ShowExitButtons()
    {
        buttonsExit.SetActive(false);
        buttonsOptions.SetActive(false);
        buttonsOption.SetActive(true);
        buttonsNewGame.SetActive(false);
        buttonsBackInMenu.SetActive(true);

    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
