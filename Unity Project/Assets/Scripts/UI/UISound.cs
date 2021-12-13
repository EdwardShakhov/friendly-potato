using UnityEngine;

public class UISound : MonoBehaviour
{
    [SerializeField] private AudioClip[] _soundClick;
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    
    public void Click()
    {
        var clip = GetRandomClick();
        _audioSource.PlayOneShot(clip);
    }


    private AudioClip GetRandomClick()
    {
        return _soundClick[Random.Range(0, _soundClick.Length)];
    }
}