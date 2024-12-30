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
        //�ƶ�����
        if(IsSiking)
        {
            transform.Translate(-transform.up*2f*Time.deltaTime);
        }
    }

    public void TakeDamage(int amount,Vector3 hitPoint)
    {
        //�ж�����������������ֱ��return

        if (IsDeath == true)
            return;
        

        //�ܻ���Ч
        enemyAudioSource.Play();

        //�ܻ���Ч
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

        //������������
        enemyAnimator.SetTrigger("Death");
        enemyCapsuleCollider.isTrigger = true;
        //����������Ч
        enemyAudioSource.clip = DeathClip;
        enemyAudioSource.Play();

        //���õ���
        GetComponent<NavMeshAgent>().enabled = false;
        
        GetComponent<Rigidbody>().isKinematic = true;

    }

    public void SartSinking()
    {
        IsSiking = true;

        Destroy(gameObject, 2f);
    }
}
