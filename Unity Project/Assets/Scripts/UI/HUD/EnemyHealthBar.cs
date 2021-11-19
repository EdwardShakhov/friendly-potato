using System;
using Player;
using UnityEngine;
using UnityEngine.UI;

    public class EnemyHealthBar : MonoBehaviour
    {
        [SerializeField] private Slider slider;

        public void SetMaxHealth(int health)
        {
            slider.maxValue = health;
            slider.value = health;
        }

        public void SetHealth(int health)
        {
            if (Mathf.Abs(slider.value - health) < 0.001f)
            {
                return;
            }
            slider.value = health;
        }

        public void On()
        {
            gameObject.SetActive(true);
            Invoke(nameof(Off), 4f);
        }

        public void Off()
        {
            gameObject.SetActive(false);
        }

        protected void LateUpdate()
        {
            transform.LookAt(GameManager.Instance.Player.GetComponent<PlayerController>().GameCamera.transform);
            transform.Rotate(0, 180,0);
        }
    }