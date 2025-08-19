using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.SceneManagement;

public class MenuLoader : MonoBehaviour
{
    public Button menuButton;  //play button

    void Start()
    {
        menuButton.onClick.AddListener(LoadMenuScene);
    }

    void LoadMenuScene()
    {
        SceneManager.LoadScene(0); 
    }
}