using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    //funciones para cargar escenas con int
    public void StartGame(int _level)
    {
        SceneManager.LoadScene(_level);
    }
    

    //funciones para cargar escenas con string
    public void StartGame(string _level)
    {
        SceneManager.LoadScene(_level);
    }

    public void LoadLevelAsync(int _level)
    {
        SceneManager.LoadSceneAsync(_level);
    }
    public void LoadLevelAsync(string _level)
    {
        SceneManager.LoadSceneAsync(_level);
    }
    #region Quit
    public void Quit()
    {
        Debug.LogWarning("salir");
        Application.Quit();
    }
    #endregion
}

