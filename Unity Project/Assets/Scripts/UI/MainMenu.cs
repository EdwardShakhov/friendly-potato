using Player;
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
