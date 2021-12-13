using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public void SetActive()
    {
        gameObject.SetActive(true);
    }

    public void RestartButton()
    {
        GameObject.Find("Canvas").GetComponent<UISound>().Click();
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenuButton()
    {
        GameObject.Find("Canvas").GetComponent<UISound>().Click();
        
        SceneManager.LoadScene("MainMenu");
    }
}