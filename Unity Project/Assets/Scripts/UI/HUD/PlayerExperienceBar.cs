using Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerExperienceBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    public TextMeshProUGUI XPLevel;

    protected void Update()
    {
        SetExperience(GameManager.Instance.Player.GetComponent<PlayerController>().PlayerExperience);
        SetLevel(GameManager.Instance.Player.GetComponent<PlayerController>().PlayerLevel);

    }

    private void SetExperience(int experience)
    {
        if (Mathf.Abs(slider.value - experience) < 0.001f)
        {
            return;
        }
        slider.value = experience;
    }

    private void SetLevel(int level)
    {
        XPLevel.text = "Level " + level;
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
}