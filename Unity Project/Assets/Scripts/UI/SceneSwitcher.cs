using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void ChangeScene(int numberScenes)
    {
        SceneManager.LoadScene(numberScenes);
        Time.timeScale = 1f;
    }

    public void Exit()
    {
        Application.Quit();
    }
}
