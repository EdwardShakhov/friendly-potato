using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    [SerializeField] private AudioClip[] _soundSteps;
    [SerializeField] private AudioClip[] _soundFire;
    [SerializeField] private AudioClip[] _soundHit;
    [SerializeField] private AudioClip[] _soundFall;
    [SerializeField] private AudioClip[] _soundLoot;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void Footstep() 
        //triggering by animation event
    {
        var clip = GetRandomStep();
        _audioSource.PlayOneShot(clip);
    }

    public void Shoot()
    {
        var clip = GetRandomShoot();
        _audioSource.PlayOneShot(clip);
    }

    public void HitEnemy()
    {
        var clip = GetRandomHit();
        _audioSource.PlayOneShot(clip);
    }
    
    //triggering by animation event
    public void Fall()
    {
        var clip = GetRandomFall();
        _audioSource.PlayOneShot(clip);
    }
    
    public void Loot()
    {
        var clip = GetRandomLoot();
        _audioSource.PlayOneShot(clip);
    }

    private AudioClip GetRandomStep()
    {
        return _soundSteps[Random.Range(0, _soundSteps.Length)];
    }

    private AudioClip GetRandomShoot()
    {
        return _soundFire[Random.Range(0, _soundFire.Length)];
    }

    private AudioClip GetRandomHit()
    {
        return _soundHit[Random.Range(0, _soundHit.Length)];
    }
    
    private AudioClip GetRandomFall()
    {
        return _soundFall[Random.Range(0, _soundFall.Length)];
    }
    
    private AudioClip GetRandomLoot()
    {
        return _soundLoot[Random.Range(0, _soundLoot.Length)];
    }
}