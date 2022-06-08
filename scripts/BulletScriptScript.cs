using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    // TEMPLATE DATA
    private string sourcePlayer, sourcePatrol, sourceGuard, sourceBoss;
    private string typePistol, typeRifle;
    private string targetTraining;
    private float damagePlayerPistol, damagePlayerRifle, damagePatrol, damageGuard, damageBoss;
    private float speedPlayerPistol, speedPlayerRifle , speedPatrol, speedGuard, speedBoss;
    // Atribute
    private string source;
    private string type;
    private float damage;
    private float lifeTime;
    

    private void Awake() {

        sourcePlayer = "Player";
        sourcePatrol = "Patrol";
        sourceGuard = "Guard";
        sourceBoss = "Boss";
        targetTraining = "Training";

        typePistol = "Pistol";
        typeRifle = "Rifle";

        speedPlayerPistol = 600f;
        speedPlayerRifle = 500f;
        speedPatrol = 100f;
        speedGuard = 100f;
        speedBoss = 400f;
        
        damagePlayerRifle = 35;
        damagePlayerPistol = 70;
        damagePatrol = 1;
        damageGuard = 1;
        damageBoss = 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Init(string source, float damage, float speed, Transform caller){
        this.source = source;
        this.damage = damage;

        Rigidbody rigidBody = GetComponent<Rigidbody>();
        rigidBody.AddForce(caller.forward  * speed, ForceMode.Impulse);

    }
    public void InitPlayerPistol(Transform caller){
        this.type = typePistol;
        Init(sourcePlayer, damagePlayerPistol, speedPlayerRifle, caller);
    }
    public void InitPlayerRifle(Transform caller){
        this.type = typeRifle;
        Init(sourcePlayer, damagePlayerRifle, speedPlayerPistol, caller);
    }
    public void InitPatrol(Transform caller){
        Init(sourcePatrol, damagePatrol, speedPatrol, caller);
    }
    public void InitGuard(Transform caller){
        Init(sourceGuard, damageGuard, speedGuard, caller);
    }
    public void InitBoss(Transform caller){
        Init(sourceBoss, damageBoss, speedBoss, caller);
    }

    static public void DestroyBulletByTime(){

    }


    private void OnCollisionEnter(Collision Collided) {
        Destroy(this.gameObject);
        Debug.Log("bullet collided with " + Collided.transform.tag);

        // pistol to training target
        if(source == sourcePlayer && Collided.transform.tag == targetTraining){
            Debug.Log("training hit");
            if(type == typePistol){
                // global object count pistol
                //GameStateScript.AddTrainPistolHit();
            }else if(type == typeRifle) {
                // global object count rifle
                //GameStateScript.AddTrainRifleHit();
            }
        }

        // player to patrol
        if(source == sourcePlayer && Collided.transform.tag == sourcePatrol){
            PatrolScript patrol = Collided.transform.GetComponent<PatrolScript>();
            patrol.TakeDamage(damage);
        }
        // player to guard
        if(source == sourcePlayer && Collided.transform.tag == sourceGuard){
            //GuardScript guard = Collided.transform.GetComponent<GuardScript>();
            //guard.TakeDamage(damage);
        }
        // player to boss
        if(source == sourcePlayer && Collided.transform.tag == sourceBoss){
            //BossScript boss = Collided.transform.GetComponent<BossScript>();
            //boss.TakeDamage(damage);
        }
        // enemy to player
        if(source != sourcePlayer && Collided.transform.tag == sourcePlayer){
            Debug.Log("player kena dmg");
            healthmanagement player = Collided.transform.GetComponent<healthmanagement>();
            player.takedmg(1);
        }
    }

}
