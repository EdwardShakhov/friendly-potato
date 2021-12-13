using Player;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScreen : MonoBehaviour
{
    public void SetActive()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!GameManager.Instance.IsGamePaused)
            {
                Pause();
            }
            else
            {
                ResumeButton();
            }
        }
    }

    private void Pause()
    {
        GameObject.Find("Canvas").GetComponent<UISound>().Click();
        
        if (GameManager.Instance.IsPlayerDead) 
            return;
        gameObject.SetActive(true);
        Time.timeScale = 1e-10f;
        GameManager.Instance.IsGamePaused = true;
    }

    public void ResumeButton()
    {
        GameObject.Find("Canvas").GetComponent<UISound>().Click();
        
        gameObject.SetActive(false);
        Time.timeScale = 1f;
        GameManager.Instance.IsGamePaused = false;
    }

    public void RestartButton()
    {
        GameObject.Find("Canvas").GetComponent<UISound>().Click();
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
        GameManager.Instance.IsGamePaused = false;
    }

    public void MainMenuButton()
    {
        GameObject.Find("Canvas").GetComponent<UISound>().Click();
        
        SceneManager.LoadScene("MainMenu");
    }

    public void SaveGame()
    {
        GameObject.Find("Canvas").GetComponent<UISound>().Click();
        
        SaveSystem.SaveGame(GameManager.Instance.Player.GetComponent<PlayerController>());
    }

    public void LoadGame()
    {
        GameObject.Find("Canvas").GetComponent<UISound>().Click();
        
        var savedData = SaveSystem.LoadGame();
        //SceneManager.LoadScene(savedData.SavedActiveScene);
        //Time.timeScale = 1f;
        
        Vector3 position;
        position.x = savedData.SavedPosition[0];
        position.y = savedData.SavedPosition[1];
        position.z = savedData.SavedPosition[2];
        GameManager.Instance.Player.GetComponent<PlayerController>().transform.position = position;
        
        GameManager.Instance.Player.GetComponent<PlayerController>().PlayerHealth = savedData.SavedPlayerHealth;
        GameManager.Instance.Player.GetComponent<PlayerController>().PlayerLevel = savedData.SavedPlayerLevel;
        GameManager.Instance.Player.GetComponent<PlayerController>().PlayerExperience = savedData.SavedPlayerExperience;
        
        var weapons = GameManager.Instance.Player.GetComponent<PlayerController>().Weapons;
        foreach (var weapon in weapons)
        {
            if (weapon.GetComponent<Weapon>().WeaponName == "Pistol")
            {
                weapon.GetComponent<Weapon>().NumberOfBullets = savedData.SavedPlayerAmmoPistol;
            }
            if (weapon.GetComponent<Weapon>().WeaponName == "Shotgun")
            {
                weapon.GetComponent<Weapon>().NumberOfBullets = savedData.SavedPlayerAmmoShotgun;
            }
        }
    }
}