using System;
using UnityEngine;
using UnityEngine.AI;

public class MyEnemyHealth : MonoBehaviour
{
    public AudioClip DeathClip;
    public  int StartingHealth = 100;


    private AudioSource enemyAudioSource;
    private ParticleSystem enemyParticles;
    private Animator enemyAnimator;
    private CapsuleCollider enemyCapsuleCollider;

    public bool IsDeath = false;

    private bool IsSiking = false;

    private void Awake()
    {
        enemyAudioSource = GetComponent<AudioSource>();
        enemyParticles = GetComponentInChildren<ParticleSystem>();
        enemyAnimator = GetComponent<Animator>();
        enemyCapsuleCollider = GetComponent<CapsuleCollider>();
    }


    // Update is called once per frame
    void Update()
    {
        //移动物体
        if(IsSiking)
        {
            transform.Translate(-transform.up*2f*Time.deltaTime);
        }
    }

    public void TakeDamage(int amount,Vector3 hitPoint)
    {
        //判断死亡与否，如果死亡，直接return

        if (IsDeath == true)
            return;
        

        //受击音效
        enemyAudioSource.Play();

        //受击特效
        enemyParticles.transform.position = hitPoint;
        enemyParticles.Play();

        
        StartingHealth -= amount;
       if(StartingHealth <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        IsDeath = true;

        //播放死亡动画
        enemyAnimator.SetTrigger("Death");
        enemyCapsuleCollider.isTrigger = true;
        //播放死亡音效
        enemyAudioSource.clip = DeathClip;
        enemyAudioSource.Play();

        //禁用导航
        GetComponent<NavMeshAgent>().enabled = false;
        
        GetComponent<Rigidbody>().isKinematic = true;

    }

    public void SartSinking()
    {
        IsSiking = true;

        Destroy(gameObject, 2f);
    }
}
