using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void NewGame(int numberScenes)
    {
        SceneManager.LoadScene(numberScenes);
        Time.timeScale = 1f;
        
        GameObject.Find("Canvas").GetComponent<UISound>().Click();
    }

    public void Exit()
    {
        Application.Quit();
        
        GameObject.Find("Canvas").GetComponent<UISound>().Click();
    }
    
    public void LoadGame()
    {
        var savedData = SaveSystem.LoadGame();
        
        GameObject.Find("Canvas").GetComponent<UISound>().Click();
        
        SceneManager.LoadScene(savedData.SavedActiveScene);
    }
}
