using UnityEngine;
using UnityEngine.SceneManagement;

public class MyPlayerHealth : MonoBehaviour
{

    //���Ѫ��
    public int PlayerStartingHealth = 100;
    //����Ƿ�����
    public bool IsPlayerDeath = false;
    public AudioClip PlayerDeathClip;

    private AudioSource playerAudio;
    private Animator playerAnim;
    private PlayerMovement playerMovement;
    private MyGrooseShooting myGrooseShooting;


    
    private void Awake()
    {
        playerAudio = GetComponent<AudioSource>();
        playerAnim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        myGrooseShooting = GetComponentInChildren<MyGrooseShooting>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int amount)
    {
        if (IsPlayerDeath)
            return;

        //������Ч
        playerAudio.Play();

        PlayerStartingHealth -= amount;
        if (PlayerStartingHealth <= 0)
            Death();

    }

    void Death()
    {

        IsPlayerDeath = true;


        playerAudio.clip = PlayerDeathClip;//������Ч����
        playerAudio.Play();

        //������������
        playerAnim.SetTrigger("Die");

        //��ֹ�ƶ� ���
        playerMovement.enabled = false;
        myGrooseShooting.enabled = false;

    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(0);


    }



}
