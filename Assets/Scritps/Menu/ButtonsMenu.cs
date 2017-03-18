using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsMenu : MonoBehaviour {
    public void Game()
    {
        Application.LoadLevel("temp");
    }
    public void exit()
    {
        Application.Quit();
    }
}
