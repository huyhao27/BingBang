using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public string chooseLevel;
    public string optionScene;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartGame()
    {
        SceneManager.LoadScene(chooseLevel);
    }
    public void OpenOptions()
    {
        SceneManager.LoadScene(optionScene);
    }
    public void CloseOptions()
    {

    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
