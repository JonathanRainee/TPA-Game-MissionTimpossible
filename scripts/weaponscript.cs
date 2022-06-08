using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponscript : MonoBehaviour
{
    class bullet
    {
        public float time;
        public Vector3 startpos;
        public Vector3 startvelo;
        public TrailRenderer trace;
        public int bounce;
    }

    public activeweapon.weaponslot weaponSlot;
    public float bulletspeed = 1000.0f;
    public float bulletdrop = 0.0f;
    public int maxbounce = 0;
    public bool firing = false;
    public int firerate = 15;
    public ParticleSystem[] muzzle;
    public string type;
    public Transform RayCast;
    public AnimationClip weaponAnim;
    public TrailRenderer tracerEffect;
    public Transform raycastDest;
    public ParticleSystem hiteffect;
    Ray ray;
    RaycastHit hitinfo;
    float sumTime;
    List<bullet> bullets = new List<bullet>();
    float lifetime = 3.0f;

    public GameObject magazine;
    public int ammocount;
    public int magzsize;
    public int currammo;

    Vector3 getpos(bullet bullet1)
    {
        //pos + velo * time + 0.5*gravity*time*time
        Vector3 gravity = Vector3.down * bulletdrop;
        return (bullet1.startpos) + (bullet1.startvelo * bullet1.time) + (0.5f * gravity * bullet1.time * bullet1.time);

    }

    bullet create(Vector3 position, Vector3 velocity)
    {
        bullet bullet1 = new bullet();
        bullet1.startpos = position;
        bullet1.startvelo = velocity;
        bullet1.time = 0.0f;
        bullet1.bounce = maxbounce;
        bullet1.trace = Instantiate(tracerEffect, position, Quaternion.identity);
        bullet1.trace.AddPosition(position); 
        return bullet1;
    }

    public void startfire()
    {
        firing = true;
        sumTime = 0.0f;
        fire();
        
    }

    public void UpdateFire(float deltaTime)
    {
        sumTime += deltaTime;
        float interval = 1.0f / firerate;
        while(sumTime >= 0.0f)
        {
            fire();
            sumTime -= interval;
        }
        
    }

    public void updatebullet(float deltaTime)
    {
        simulatebullet(deltaTime);
        destroybullet();
    }

    void simulatebullet(float deltaTime)
    {
        bullets.ForEach(bullet =>
        {
            Vector3 posStart = getpos(bullet);
            bullet.time += deltaTime;
            Vector3 posUp = getpos(bullet);
            raycastseg(posStart, posUp, bullet);
        });
    }

    void destroybullet()
    {
        bullets.RemoveAll(bullet => bullet.time >= lifetime);
    }

    void raycastseg(Vector3 start, Vector3 end, bullet bullet2)
    {
        Vector3 direction = end - start;
        float distance = direction.magnitude;
        ray.origin = start;
        ray.direction = direction;

        if (Physics.Raycast(ray, out hitinfo, distance))
        {
            hiteffect.transform.position = hitinfo.point;
            hiteffect.transform.forward = hitinfo.normal;
            hiteffect.Emit(1);

            bullet2.trace.transform.position = hitinfo.point;
            bullet2.time = lifetime;

            //if(bullet2.bounce > 0)
            //{
            //    bullet2.time = 0;
            //    bullet2.startpos = hitinfo.point;
            //    bullet2.startvelo = Vector3.Reflect(bullet2.startvelo, hitinfo.normal);
            //}

            var rb2d = hitinfo.collider.GetComponent<Rigidbody>();
            if (rb2d)
            {
                rb2d.AddForceAtPosition(ray.direction * 20, hitinfo.point, ForceMode.Impulse);
                //                           impulse bs gnti force / acceleration
            }

        }
        else
        {
            bullet2.trace.transform.position = end;
        }
    }

    private void fire()
    {

        //if (currammo <= 0)
        //{
        //    return;
        //}
        //currammo--;

        foreach (var particle in muzzle)
        {
            particle.Emit(1);
        }

        Vector3 velocity = (raycastDest.position - RayCast.position).normalized * bulletspeed;
        var bullet1 = create(RayCast.position, velocity);
        bullets.Add(bullet1);

        //ray.origin = RayCast.position;
        //ray.direction = raycastDest.position - RayCast.position;
        //var tracer = Instantiate(tracerEffect, ray.origin, Quaternion.identity);
        //tracer.AddPosition(ray.origin);


        //if (Physics.Raycast(ray, out hitinfo))
        //{
        //    //Debug.DrawLine(ray.origin, hitinfo.point, Color.red, 1.0f);
        //    hiteffect.transform.position = hitinfo.point;
        //    hiteffect.transform.forward = hitinfo.normal;
        //    hiteffect.Emit(1);

        //    tracer.transform.position = hitinfo.point;
        //}
    }

    public void stopfire()
    {
        firing = false;
    }
}
