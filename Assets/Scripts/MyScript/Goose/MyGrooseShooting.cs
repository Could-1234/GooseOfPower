using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class MyGrooseShooting : MonoBehaviour
{
    public float timeBetweenBullets = 0.15f;
    
    private float time = 0f;
    private float effctsDisplayTime = 0.2f;
    private AudioSource gunAudio;
    private Light gunLight;
    private LineRenderer gunLine;
    private ParticleSystem gunParticle;

    //开枪发射射线的相关变量
    private Ray shootRay;
    private RaycastHit shootHit;
    private int shootMask;

    
    private void Awake()
    {
        gunAudio = GetComponent<AudioSource>();
        gunLight = GetComponent<Light>();
        gunLine = GetComponent<LineRenderer>();
        gunParticle = GetComponent<ParticleSystem>();

        //获取射线检测的图层
        shootMask = LayerMask.GetMask("Shootable");
    }

    
    
    // Update is called once per frame
    void Update()
    {
        time = time + Time.deltaTime;

     if (Input.GetButton("Fire1")&& time>=timeBetweenBullets) //获取开火键
     {
            Shoot();  //射击
     }

     if(time>=timeBetweenBullets*effctsDisplayTime)
        {
            gunLight.enabled = false;
            gunLine.enabled = false;
        }
           
    }

    void Shoot()
    {
        time =0;

        gunLight.enabled = true;//启用灯光

        gunAudio.Play();//播放声音

        //绘制线条
        gunLine.SetPosition(0, transform.position);
        //gunLine.SetPosition(1, transform.position + transform.forward * 100);
        gunLine.enabled = true;

        //播放粒子组件
        gunParticle.Play();

        //发射射线检测有没有命中
        //定义一个ray 定义一个Mask 定义Hit
        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;

        if (Physics.Raycast(shootRay, out shootHit, 100,shootMask))
        {
            gunLine.SetPosition(1, shootHit.point);

            MyEnemyHealth enemyHealth =shootHit.collider.GetComponent<MyEnemyHealth>();
            enemyHealth.TakeDamage(10, shootHit.point);

          
        }
        else
        {
            gunLine.SetPosition(1, transform.position + transform.forward * 100);
        }
    }
}
