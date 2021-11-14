using Player;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void NewGame(int numberScenes)
    {
        SceneManager.LoadScene(numberScenes);
        Time.timeScale = 1f;
    }

    public void Exit()
    {
        Application.Quit();
    }
    
    public void LoadGame()
    {
        var savedData = SaveSystem.LoadGame();
        
        SceneManager.LoadScene(savedData.SavedActiveScene);
    }
}
