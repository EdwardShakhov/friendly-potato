using UnityEngine;

public class EnemySound : MonoBehaviour
{
    [SerializeField] private AudioClip[] _soundHit;
    [SerializeField] private AudioClip[] _soundFall;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    
    public void HitPlayer() 
        //triggering by animation event
    {
        var clip = GetRandomHitPlayer();
        _audioSource.PlayOneShot(clip);
    }
    
    public void Fall() 
        //triggering by animation event
    {
        var clip = GetRandomFall();
        _audioSource.PlayOneShot(clip);
    }

    private AudioClip GetRandomHitPlayer()
    {
        return _soundHit[Random.Range(0, _soundHit.Length)];
    }
    
    private AudioClip GetRandomFall()
    {
        return _soundFall[Random.Range(0, _soundFall.Length)];
    }
}