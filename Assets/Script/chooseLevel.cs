using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class chooseLevel : MonoBehaviour
{
    public string backToMainmenu;
    public string[] levels;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void BackToMainmenu()
    {
        SceneManager.LoadScene(backToMainmenu);
    }
    public void OpenLevel(int levelIndex)
    {
        if (levelIndex >= 0 && levelIndex < levels.Length)
        {
            SceneManager.LoadScene(levels[levelIndex]);
        }
    }
}
