using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void Menu()
    {
        SceneManager.LoadScene(0);
    }

    public void GyroMovementApp()
    {
        SceneManager.LoadScene(1);
    }

    public void CoordinatesApp()
    {
        SceneManager.LoadScene(2);
    }

    public void CameraApp()
    {
        SceneManager.LoadScene(3);
    }


}
