using UnityEngine;

public class EnemySound : MonoBehaviour
{
    [SerializeField] private AudioClip[] _soundHit;
    [SerializeField] private AudioClip[] _soundFall;
    [SerializeField] private AudioClip[] _soundInstantiation;
    [SerializeField] private int _soundDistanceToPlayer;
    private AudioSource _audioSource;

    public int SoundDistanceToPlayer => _soundDistanceToPlayer;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    
    // triggering by animation event
    public void HitPlayer()
    {
        var clip = GetRandomHitPlayer();
        _audioSource.PlayOneShot(clip);
    }
    
    // triggering by animation event
    public void Fall()
    {
        var clip = GetRandomFall();
        _audioSource.PlayOneShot(clip);
    }
    
    public void Instantiation()
    {
        var clip = GetRandomInstantiation();
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
    
    private AudioClip GetRandomInstantiation()
    {
        return _soundInstantiation[Random.Range(0, _soundInstantiation.Length)];
    }
}