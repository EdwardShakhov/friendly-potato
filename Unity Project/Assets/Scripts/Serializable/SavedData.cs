using Player;
using UnityEngine.Assertions.Must;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

[System.Serializable]
public class SavedData
{
    public int SavedPlayerHealth;
    public float[] SavedPosition;
    public string SavedActiveScene;
    
    public SavedData(PlayerController player)
    {
        SavedPlayerHealth = player.PlayerHealth;
            
        SavedPosition = new float[3];
        SavedPosition[0] = player.transform.position.x;
        SavedPosition[1] = player.transform.position.y;
        SavedPosition[2] = player.transform.position.z;

        SavedActiveScene = SceneManager.GetActiveScene().name;
    }
}