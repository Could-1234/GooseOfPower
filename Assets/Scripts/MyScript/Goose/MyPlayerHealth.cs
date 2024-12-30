using UnityEngine;
using UnityEngine.SceneManagement;

public class MyPlayerHealth : MonoBehaviour
{

    //ÕÊº“—™¡ø
    public int PlayerStartingHealth = 100;
    //ÕÊº“ «∑ÒÀ¿Õˆ
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

        // ‹…À“Ù–ß
        playerAudio.Play();

        PlayerStartingHealth -= amount;
        if (PlayerStartingHealth <= 0)
            Death();

    }

    void Death()
    {

        IsPlayerDeath = true;


        playerAudio.clip = PlayerDeathClip;//À¿Õˆ“Ù–ß≤•∑≈
        playerAudio.Play();

        //≤•∑≈À¿Õˆ∂Øª≠
        playerAnim.SetTrigger("Die");

        //Ω˚÷π“∆∂Ø …‰ª˜
        playerMovement.enabled = false;
        myGrooseShooting.enabled = false;

    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(0);


    }



}
