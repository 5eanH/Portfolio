using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public Button playButton;

    void Start()
    {
        playButton.onClick.AddListener(LoadMainScene);
    }

    void LoadMainScene()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.ResetGameValues();
        }

        SceneManager.LoadScene(1); 
    }
}