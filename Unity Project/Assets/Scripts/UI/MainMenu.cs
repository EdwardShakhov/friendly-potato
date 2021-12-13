using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void NewGame(int numberScenes)
    {
        GameObject.Find("Canvas").GetComponent<UISound>().Click();
        
        SceneManager.LoadScene(numberScenes);
        Time.timeScale = 1f;
    }

    public void Exit()
    {
        GameObject.Find("Canvas").GetComponent<UISound>().Click();
        
        Application.Quit();
    }
    
    public void LoadGame()
    {
        GameObject.Find("Canvas").GetComponent<UISound>().Click();
        
        var savedData = SaveSystem.LoadGame();
        SceneManager.LoadScene(savedData.SavedActiveScene);
        Time.timeScale = 1f;
    }
}
