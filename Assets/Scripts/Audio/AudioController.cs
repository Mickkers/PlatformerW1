using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController Instance;

    [SerializeField] private AudioSource bgm;
    [SerializeField] private AudioSource hitSfx;
    [SerializeField] private AudioSource attackSfx;
    [SerializeField] private AudioSource healthSfx;
    [SerializeField] private AudioSource deathSfx;
    [SerializeField] private AudioSource jumpSfx;
    [SerializeField] private AudioSource enemyMeleeSfx;
    [SerializeField] private AudioSource enemyRangeSfx;
    [SerializeField] private AudioSource projectileSfx;

    // Start is called before the first frame update
    void Start()
    {
        if (AudioController.Instance is null)
        {
            return;
        }
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DamageSFX()
    {
        hitSfx.Play();
    }

    public void AttackSFX()
    {
        attackSfx.Play();
    }

    public void HealSFX()
    {
        healthSfx.Play();
    }
    
    public void DeathSFX()
    {
        deathSfx.Play();
    }

    public void JumpSFX()
    {
        jumpSfx.Play();
    }

    public void EnemyMeleeSFX()
    {
        enemyMeleeSfx.Play();
    }
    public void EnemyRangeSFX()
    {
        enemyRangeSfx.Play();
    }

    public void ProjectileSFX()
    {
        projectileSfx.Play();
    }
}
