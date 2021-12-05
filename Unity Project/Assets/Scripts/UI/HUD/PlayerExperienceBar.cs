using Player;
using UnityEngine;
using UnityEngine.UI;

public class PlayerExperienceBar : MonoBehaviour
{
    [SerializeField] private Slider slider;

    protected void Update()
    {
        SetExperience(GameManager.Instance.Player.GetComponent<PlayerController>().PlayerExperience);

    }

    private void SetExperience(int experience)
    {
        if (Mathf.Abs(slider.value - experience) < 0.001f)
        {
            return;
        }
        slider.value = experience;
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
}