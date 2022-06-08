using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolWeapon : MonoBehaviour
{
    
    [Header("weapon, bullet, and shooting")]
    private float fireRate;
    private float lifeTime;
    private float lastShoot;
    // Start is called before the first frame update

    
    [Header("Bullet")]
    [SerializeField] BulletScript bullet;
    [SerializeField] Transform barrelPos;

    [Header("Player")]
    [SerializeField] Transform playerHitTarget;

    [Header("Misc")] 
    RaycastHit hit;

    public void ShootToPlayer(){
        if(Time.time > lastShoot + 1f/fireRate){
            lastShoot = Time.time;
            barrelPos.LookAt(playerHitTarget);
            BulletScript currBullet = Instantiate(bullet, barrelPos.position, barrelPos.rotation);
            currBullet.InitPatrol(barrelPos);
        }
    }

    void Start()
    {
        fireRate = 10;
        lastShoot = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
