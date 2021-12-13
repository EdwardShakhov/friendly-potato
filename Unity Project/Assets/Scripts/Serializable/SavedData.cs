using Player;
using UnityEngine.SceneManagement;

[System.Serializable]
public class SavedData
{
    public string SavedActiveScene;
    public float[] SavedPosition;
    public int SavedPlayerHealth;
    public int SavedPlayerLevel;
    public int SavedPlayerExperience;
    public int SavedPlayerAmmoPistol;
    public int SavedPlayerAmmoShotgun;

    public SavedData(PlayerController player)
    {
        SavedActiveScene = SceneManager.GetActiveScene().name;
        
        SavedPosition = new float[3];
        SavedPosition[0] = player.transform.position.x;
        SavedPosition[1] = player.transform.position.y;
        SavedPosition[2] = player.transform.position.z;

        SavedPlayerHealth = player.PlayerHealth;
        SavedPlayerLevel = player.PlayerLevel;
        SavedPlayerExperience = player.PlayerExperience;

        var weapons = GameManager.Instance.Player.GetComponent<PlayerController>().Weapons;
        foreach (var weapon in weapons)
        {
            if (weapon.GetComponent<Weapon>().WeaponName == "Pistol")
            {
                SavedPlayerAmmoPistol = weapon.GetComponent<Weapon>().NumberOfBullets;
            }
            if (weapon.GetComponent<Weapon>().WeaponName == "Shotgun")
            {
                SavedPlayerAmmoShotgun = weapon.GetComponent<Weapon>().NumberOfBullets;
            }
        }
        
    }
}