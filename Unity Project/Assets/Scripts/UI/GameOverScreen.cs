using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public void SetActive()
    {
        gameObject.SetActive(true);
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
        GameObject.Find("Canvas").GetComponent<UISound>().Click();
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
        
        GameObject.Find("Canvas").GetComponent<UISound>().Click();
    }
}