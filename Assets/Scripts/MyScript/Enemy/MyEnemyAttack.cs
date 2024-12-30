using System;
using UnityEngine;

public class MyEnemyAttack : MonoBehaviour
{

    public float EnemyAttackDamaged = 10;
 
    private GameObject player;
    private bool PlayerInRange = false;
    private float timer = 0;

    private MyPlayerHealth myPlayerHealth;
    private Animator enemyAnim;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        myPlayerHealth = player.GetComponent<MyPlayerHealth>();
        enemyAnim = GetComponent<Animator>();
    }
    
    
    
    
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;



        if(!myPlayerHealth.IsPlayerDeath && PlayerInRange && timer > 0.5f)
        {
            //如果离敌人很近就造成伤害
            Attack();
        }

        if (myPlayerHealth.IsPlayerDeath)
            enemyAnim.SetTrigger("PlayerDeath");//播放结算动画
    }

    
    
    private void Attack()
    {
        timer = 0;

        //获取玩家血量组件
        myPlayerHealth.TakeDamage(10);

    }



    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject==player)
        {
            PlayerInRange = true;
            //Debug.Log("触发器被触发");
        }
    }

    
    
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject== player)
        {
            PlayerInRange = false;
           // Debug.Log("玩家已离开");
        }
    }

}
