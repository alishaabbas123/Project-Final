using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void PlayGame(){
        SceneManager.LoadSceneAsync("Project");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void QuitGame(){
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
