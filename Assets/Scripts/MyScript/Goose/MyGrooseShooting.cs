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

    //��ǹ�������ߵ���ر���
    private Ray shootRay;
    private RaycastHit shootHit;
    private int shootMask;

    
    private void Awake()
    {
        gunAudio = GetComponent<AudioSource>();
        gunLight = GetComponent<Light>();
        gunLine = GetComponent<LineRenderer>();
        gunParticle = GetComponent<ParticleSystem>();

        //��ȡ���߼���ͼ��
        shootMask = LayerMask.GetMask("Shootable");
    }

    
    
    // Update is called once per frame
    void Update()
    {
        time = time + Time.deltaTime;

     if (Input.GetButton("Fire1")&& time>=timeBetweenBullets) //��ȡ�����
     {
            Shoot();  //���
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

        gunLight.enabled = true;//���õƹ�

        gunAudio.Play();//��������

        //��������
        gunLine.SetPosition(0, transform.position);
        //gunLine.SetPosition(1, transform.position + transform.forward * 100);
        gunLine.enabled = true;

        //�����������
        gunParticle.Play();

        //�������߼����û������
        //����һ��ray ����һ��Mask ����Hit
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
