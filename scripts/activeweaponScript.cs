using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//using UnityEditor.Animations;

public class activeweapon : MonoBehaviour
{

    public enum weaponslot
    {
        Primary = 0,
        Secondary = 1
    }

    //[SerializeField] GameObject weaponparent;
    //weaponscript[] playerweapon = new weaponscript[2];
    weaponscript[] equipped_weapon = new weaponscript[2];
    //List<weaponscript> playerweapon = new List<weaponscript>();
    int activeweaponidx;

    weaponscript getweapon(int idx)
    {
        if(idx < 0 || idx >= 2)
        {
            return null;
        }
        return equipped_weapon[idx];
    }

    //public recoil recoilscript;
    //public recoilTEST testingrecoil;
    [SerializeField] public LayerMask pistolammolayer;
    [SerializeField] public LayerMask riffleammolayer;
    [SerializeField] public LayerMask soldierlayer;
    [SerializeField] public LayerMask soldiertelelayer;
    [SerializeField] public LayerMask bosslayer;
    [SerializeField] GameObject ammocanvas;

    public GameObject pistolcounterparent;
    public GameObject rifflecounterparent;
    public TextMeshProUGUI pistolcountertext;
    public TextMeshProUGUI rifflecountertext;

    public Cinemachine.CinemachineFreeLook playercam;
    public Transform crosshairtarget;
    public Transform[] weaponparent;
    public UnityEngine.Animations.Rigging.Rig handIK;
    public Transform leftgrip;
    public Transform rightgrip;
    //Animator anim;
    public Animator rigidcontroller;
    [SerializeField] GameObject pistolcanvas;
    [SerializeField] GameObject rifflecanvas;
    public bool holster = false;

    [SerializeField] quest questscript;
    [SerializeField] Camera weaponcam;
    [SerializeField] public LayerMask layertarget;
    public float distance = 2.0f;

    public AudioSource pistolsound;
    public AudioSource rifflesound;
    public AudioClip pistolclip;
    public AudioClip riffleclip;

    public int pistolammocount = 14;
    public int pistolmagzsize = 7;
    public int pistolcurrammo = 7;

    public int riffleammocount = 60;
    public int rifflemagzsize = 30;
    public int rifflecurrammo = 30;

    public ammowidget widget;

    public GameObject fireLightEffect;

    public healthboss hb;

    [SerializeField] private healthsodier hs;

    //AnimatorOverrideController overrides;


    // Start is called before the first frame update
    void Start()
    {
        //testingrecoil = GetComponent<recoilTEST>();
        //recoilscript = transform.Find("camerarecoil").GetComponent<recoil>();
        //anim = GetComponent<Animator>();
        //overrides = anim.runtimeAnimatorController as AnimatorOverrideController;

        //weaponscript existingweapon = GetComponentInChildren<weaponscript>();
        //if (existingweapon)
        //{
        //    equip(existingweapon);
        //}

        //playerweapon = GetComponentInChildren<weaponscript>();
        //pistolsound = GetComponent<AudioSource>();
        //pistolsound.Play();
        //rifflesound = GetComponent<AudioSource>(); 
        //if (playerweapon)
        //    equip(playerweapon);
        //}

        //buildpistolclip(pistolclip);
        //buildriffleclip(riffleclip);
    }

    public weaponscript getactiveweapon()
    {
        return getweapon(activeweaponidx);
    }

    public void buildpistolclip(AudioClip clip)
    {
        pistolsound = gameObject.AddComponent<AudioSource>();
        pistolsound.clip = clip;
    }

    public void buildriffleclip(AudioClip clip)
    {
        rifflesound = gameObject.AddComponent<AudioSource>();
        rifflesound.clip = clip;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    Debug.Log("total guard kill: "+)
        //}

            var playerweapon = getweapon(activeweaponidx);
        //playerweapon = GetComponentInChildren<weaponscript>();
        RaycastHit hit;
        Ray ray = weaponcam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));

        if (Physics.Raycast(ray, out hit, distance, pistolammolayer) && questscript.qidx >= 1)
        {
            ammocanvas.SetActive(true);
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (hit.transform.tag == "ammopistol")
                {
                    Destroy(hit.transform.gameObject);
                    //weapon.ammocount += weapon.magzsize;
                    //if (pistolcurrammo == 0)
                    //{
                    //    activeweaponscript.pistolcurrammo = activeweaponscript.pistolmagzsize;
                    //}
                    pistolammocount += pistolmagzsize;
                    //widget.refresh(weapon.currammo, weapon.ammocount);
                    refreshpistol(pistolcurrammo, pistolammocount);
                }
            }

        }
        else if (Physics.Raycast(ray, out hit, distance, riffleammolayer) && questscript.qidx >= 3)
        {
            ammocanvas.SetActive(true);
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (hit.transform.tag == "ammoriffle")
                {
                    Destroy(hit.transform.gameObject);
                    //weapon.ammocount += weapon.magzsize;
                    //if (activeweaponscript.rifflecurrammo == 0)
                    //{
                    //    activeweaponscript.rifflecurrammo = activeweaponscript.rifflemagzsize;
                    //}
                    riffleammocount += rifflemagzsize;
                    //widget.refresh(weapon.currammo, weapon.ammocount);
                    refreshriffle(rifflecurrammo, riffleammocount);
                }
            }
        }
        else
        {
            ammocanvas.SetActive(false);
        }

        if (playerweapon)
        {

           

            if (playerweapon.type == "pistol")
            {
                pistolcanvas.SetActive(false);

                if (pistolcurrammo >= 0) {
                    Debug.Log("curr ammo "+pistolcurrammo);
                }

                    if (Input.GetMouseButtonDown(0))
                    {
                    if(holster == false && pistolcurrammo > 0)
                    {

                        fireLightEffect.SetActive(true);
                        pistolcurrammo--;    
                        //testingrecoil.recoilfire();
                        playerweapon.startfire();
                        //recoilscript.recoilfire();
                        pistolsound.Play();
                        refreshpistol(pistolcurrammo, pistolammocount);

                        if (pistolsound.isPlaying)
                        {
                            Debug.Log("ada suara pistol");
                        }

                        if (Physics.Raycast(ray, out hit, distance, layertarget))
                        {
                            questscript.shootwpistol++;
                            Debug.Log(questscript.shootwpistol);
                            Debug.Log("kena target");

                            if(questscript.shootwpistol > 10)
                            {
                                questscript.shootwpistol = 10;
                            }
                            
                        }
                        if (Physics.Raycast(ray, out hit, distance, soldierlayer))
                        {
                            Debug.Log("kena soldier");
                            Debug.Log("kena dmg 70");
                            hit.transform.gameObject.GetComponentInChildren<healthsodier>().takedmg(70);

                            if(hit.transform.gameObject.GetComponentInChildren<healthsodier>().currhealth <= 0)
                            {
                                questscript.soldierkill++;
                            }
                            //hs.takedmg(33);
                        }

                        if (Physics.Raycast(ray, out hit, distance, soldiertelelayer))
                        {
                            Debug.Log("kena soldier tele");
                            Debug.Log("kena dmg 70");
                            hit.transform.gameObject.GetComponentInChildren<healthsodier>().takedmg(70);

                            if (hit.transform.gameObject.GetComponentInChildren<healthsodier>().currhealth <= 0)
                            {
                                questscript.guardkill++;
                            }
                            //hs.takedmg(33);
                        }



                        if (Physics.Raycast(ray, out hit, distance, bosslayer))
                        {
                            Debug.Log("kena boss");
                            Debug.Log("kena dmg 70");
                            hb.takedmg(70);
                            //hit.transform.gameObject.GetComponentInChildren<healthboss>().takedmg(70);
                            //if (hit.transform.gameObject.GetComponentInChildren<healthboss>().currhealth <= 0)
                            //{
                                //questscript.soldierkill++;
                            //}
                            //hs.takedmg(33);
                        }


                    }
                    //ammo.currammo--;
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    fireLightEffect.SetActive(false);
                    playerweapon.stopfire();
                }

                //if (playerweapon.firing)
                //{
                //    playerweapon.UpdateFire(Time.deltaTime);
                //}

                playerweapon.updatebullet(Time.deltaTime);

                //if (Input.GetKeyDown(KeyCode.X))
                //{
                //    Debug.Log("pencet x");
                //    bool weaponpicking = rigidcontroller.GetBool("pickingweapon");
                //    Debug.Log("masuk1");
                //    rigidcontroller.SetBool("pickingweapon", !weaponpicking);
                //    Debug.Log("masuk2");
                //}

                if (Input.GetKeyDown(KeyCode.R) /*|| pistolcurrammo <= 0*/)
                {
                    
                    if (pistolcurrammo >= pistolmagzsize)
                    {

                        int ammotoreload = pistolmagzsize - pistolcurrammo;
                        pistolammocount -= ammotoreload;
                        pistolcurrammo += ammotoreload;
                        //Debug.Log(weapon.ammocount);
                    }
                    else
                    {
                        if (pistolcurrammo + pistolammocount > pistolmagzsize)
                        {
                            int ammoleft = pistolammocount + pistolcurrammo - pistolmagzsize;
                            pistolammocount = ammoleft;
                            pistolcurrammo = pistolmagzsize;


                        }
                        else
                        {
                            pistolcurrammo += pistolammocount;
                            pistolammocount = 0;

                        }
                    }
                    refreshpistol(pistolcurrammo, pistolammocount);
                }
            }
            else if(playerweapon.type == "riffle")
            {
              /*  rifflecanvas.SetActive(false);*/
                if (Input.GetMouseButtonDown(0))
                {
                    
                    Debug.Log("curr ammo before" + rifflecurrammo);
                    if (holster == false && rifflecurrammo > 0)
                    {
                        Debug.Log("curr ammo aft" + rifflecurrammo);
                        fireLightEffect.SetActive(true);
                        
                        playerweapon.startfire();
                        rifflesound.Play();

                        rifflecurrammo--;
                        refreshriffle(rifflecurrammo, riffleammocount);
                        if (rifflesound.isPlaying)
                        {
                            Debug.Log("ada suara riffle");
                        }

                        if (Physics.Raycast(ray, out hit, distance, layertarget))
                        {
                            questscript.shootwriffle++;
                            Debug.Log(questscript.shootwriffle);
                            Debug.Log("kena target");

                            if (questscript.shootwriffle > 50)
                            {
                                questscript.shootwriffle = 50;
                            }

                        }
                        if (Physics.Raycast(ray, out hit, distance, soldierlayer))
                        {
                            Debug.Log("kena dmg 35");
                            Debug.Log("kena soldier");
                            hit.transform.gameObject.GetComponentInChildren<healthsodier>().takedmg(35);
                            if (hit.transform.gameObject.GetComponentInChildren<healthsodier>().currhealth <= 0)
                            {
                                questscript.soldierkill++;
                            }
                            //hs.takedmg(33);
                        }

                        if (Physics.Raycast(ray, out hit, distance, soldiertelelayer))
                        {
                            Debug.Log("kena dmg 35");
                            Debug.Log("kena soldier");
                            hit.transform.gameObject.GetComponentInChildren<healthsodier>().takedmg(35);
                            if (hit.transform.gameObject.GetComponentInChildren<healthsodier>().currhealth <= 0)
                            {
                                questscript.guardkill++;
                            }
                            //hs.takedmg(33);
                        }

                        if (Physics.Raycast(ray, out hit, distance, bosslayer))
                        {
                            Debug.Log("kena dmg 35");
                            Debug.Log("kena boss");
                            hb.takedmg(70);
                            //hit.transform.gameObject.GetComponentInChildren<healthboss>().takedmg(35);
                            //if (hit.transform.gameObject.GetComponentInChildren<healthboss>().currhealth <= 0)
                            //{
                            //    //questscript.soldierkill++;
                            //}
                            //hs.takedmg(33);
                        }
                    }
                    
                    //ammo.currammo--;
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    fireLightEffect.SetActive(false);
                    playerweapon.stopfire();
                    rifflesound.Stop();
                }

                if (playerweapon.firing)
                {
                    //rifflecurrammo--;
                    //rifflesound.Play();
                    //recoilscript.recoilfire();
                    playerweapon.UpdateFire(Time.deltaTime);
                    //testingrecoil.recoilfire();

                    //if (Physics.Raycast(ray, out hit, distance, layertarget))
                    //{
                    //    questscript.shootwriffle++;
                    //    Debug.Log(questscript.shootwriffle);
                    //    Debug.Log("kena target");

                    //    if (questscript.shootwriffle > 50)
                    //    {
                    //        questscript.shootwriffle = 50;
                    //    }

                    //}
                }

                if (Input.GetKeyDown(KeyCode.R))
                {
                    if (rifflecurrammo >= rifflemagzsize)
                    {

                        int ammotoreload = rifflemagzsize - rifflecurrammo;
                        riffleammocount -= ammotoreload;
                        rifflecurrammo += ammotoreload;
                        //Debug.Log(weapon.ammocount);
                    }
                    else
                    {
                        if (rifflecurrammo + riffleammocount > rifflemagzsize)
                        {
                            int ammoleft = riffleammocount + rifflecurrammo - rifflemagzsize;
                            riffleammocount = ammoleft;
                            rifflecurrammo = rifflemagzsize;


                        }
                        else 
                        {
                            rifflecurrammo += riffleammocount;
                            riffleammocount = 0;

                        }
                    }
                    refreshriffle(rifflecurrammo, riffleammocount);
                }

                playerweapon.updatebullet(Time.deltaTime);

                //if (Input.GetKeyDown(KeyCode.X))
                //{
                //    Debug.Log("pencet x");
                //    bool weaponpicking = rigidcontroller.GetBool("pickingweapon");
                //    Debug.Log("masuk1");
                //    rigidcontroller.SetBool("pickingweapon", !weaponpicking);
                //    Debug.Log("masuk2");
                //}
            }

            if (Input.GetKeyDown(KeyCode.X))
            {
                //Debug.Log("pencet x");
                //bool weaponpicking = rigidcontroller.GetBool("pickingweapon");
                //Debug.Log("masuk1");
                //rigidcontroller.SetBool("pickingweapon", !weaponpicking);
                holster = !holster;
                //Debug.Log("masuk2");
                ToggleActiveWeapon(); 
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                Debug.Log("reloadinf weapon skrg");
                rigidcontroller.SetTrigger("reload_weapon");
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                rifflecounterparent.SetActive(true);
                pistolcounterparent.SetActive(false);
                setactiveweapon(weaponslot.Primary);
                holster = false;
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                rifflecounterparent.SetActive(false);
                pistolcounterparent.SetActive(true);
                setactiveweapon(weaponslot.Secondary);
                holster = false;
            }

        }
        //else
        //{
        //    handIK.weight = 0;
        //    anim.SetLayerWeight(1, 0.0f);
        //}
        //if (Input.GetMouseButtonDown(0))
        //{
        //    playerweapon.startfire();
        //    //ammo.currammo--;
        //}
        //else if (Input.GetMouseButtonUp(0))
        //{
        //    playerweapon.stopfire();
        //}

        //if (playerweapon.firing)
        //{
        //    playerweapon.UpdateFire(Time.deltaTime);
        //}
        ////if (ammo.currammo <= 0)
        ////{
        ////    playerweapon.stopfire();
        ////}

        //playerweapon.updatebullet(Time.deltaTime);
    }

    private void LateUpdate()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    playerweapon.startfire();
        //    //ammo.currammo--;
        //}
        //else if (Input.GetMouseButtonUp(0))
        //{
        //    playerweapon.stopfire();
        //}

        //if (playerweapon.firing)
        //{
        //    playerweapon.UpdateFire(Time.deltaTime);
        //}
        ////if (ammo.currammo <= 0)
        ////{
        ////    playerweapon.stopfire();
        ////}

        //playerweapon.updatebullet(Time.deltaTime);
    }

    public void equip(weaponscript newweapon)
    {
        int weaponidx = (int)newweapon.weaponSlot;
        Debug.Log("test");
        var playerweapon = getweapon(weaponidx);

        //biar bs holster uncomment ii
        //if (playerweapon)
        //{
        //    Destroy(playerweapon.gameObject);
        //}

        playerweapon = newweapon;
        playerweapon.raycastDest = crosshairtarget;
        //playerweapon.transform.parent = weaponparent[weaponidx];
        playerweapon.transform.SetParent(weaponparent[weaponidx], false);
        playerweapon.transform.localPosition = Vector3.zero;
        playerweapon.transform.localRotation = Quaternion.identity;
        //rigidcontroller.Play("equip_" + playerweapon.type);

        //weapon = newweapon;
        //weapon.raycastDest = crosshairtarget;
        //weapon.transform.parent = weaponparent[weaponidx];
        //weapon.transform.localPosition = Vector3.zero;
        //weapon.transform.localRotation = Quaternion.identity;
        //playerweapon.Play("equip_" + playerweapon.type);

        handIK.weight = Mathf.Lerp(handIK.weight, 2.5f, 10f * Time.deltaTime);
        //handIK.weight = 1f;

        equipped_weapon[weaponidx] = playerweapon;
        setactiveweapon(newweapon.weaponSlot);
        //widget.refresh(playerweapon.currammo, playerweapon.ammocount);


        //handIK.weight = 1.0f;
        //anim.SetLayerWeight(1, 1.0f);
        //Invoke(nameof(SetAnimationDelayed), 0.001f);

    }

    void ToggleActiveWeapon()
    {
        bool weaponpicking = rigidcontroller.GetBool("pickingweapon");
        if (weaponpicking)
        {
            StartCoroutine(activateweapon(activeweaponidx));
        }
        else
        {
            StartCoroutine(Holsterweapon(activeweaponidx));
        }
    }

    void setactiveweapon(weaponslot slot)
    {
        int holsteridx = activeweaponidx;
        int activateidx = (int)slot;

        if(holsteridx == activateidx)
        {
            holsteridx = -1;
        }

        StartCoroutine(Switchweapon(holsteridx, activateidx));
    }

    IEnumerator Switchweapon(int holsteridx, int activateidx)
    {
        yield return StartCoroutine(Holsterweapon(holsteridx));
        yield return StartCoroutine(activateweapon(activateidx));
        activeweaponidx = activateidx;
    }

    IEnumerator Holsterweapon(int idx)
    {
        var weapon = getweapon(idx);

        if (weapon)
        {
            rigidcontroller.SetBool("pickingweapon", true);
            do
            {
                yield return new WaitForEndOfFrame();
            }while(rigidcontroller.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f);
        }
    }

    IEnumerator activateweapon(int idx)
    {
        

        var weapon = getweapon(idx);

        if (weapon)
        {
            rigidcontroller.SetBool("pickingweapon", false);
            rigidcontroller.Play("equip_" + weapon.type);
            do
            {
                yield return new WaitForEndOfFrame();
            } while (rigidcontroller.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f);
        }
    }

    public void refreshpistol(int currammo, int sumammo)
    {
        string curramonow = pistolcurrammo.ToString();
        string sumammonow = pistolammocount.ToString();
        pistolcountertext.text = curramonow + " / " + sumammonow;
    }

    public void refreshriffle(int currammo, int sumammo)
    {
        string curramonow = rifflecurrammo.ToString();
        string sumammonow = riffleammocount.ToString();
        rifflecountertext.text = curramonow + " / " + sumammonow;
    }

    //void SetAnimationDelayed()
    //{
    //    overrides["weapon animation empty"] = playerweapon.weaponAnim;
    //}

    //[ContextMenu("saveweapon pose")]
    //void saveweaponpos()
    //{
    //    GameObjectRecorder recorder = new GameObjectRecorder(gameObject);
    //    recorder.BindComponentsOfType<Transform>(weaponparent.gameObject, false);
    //    recorder.BindComponentsOfType<Transform>(leftgrip.gameObject, false);
    //    recorder.BindComponentsOfType<Transform>(rightgrip.gameObject, false);
    //    recorder.TakeSnapshot(0.0f);
    //    recorder.SaveToClip(playerweapon.weaponAnim);
    //    UnityEditor.AssetDatabase.SaveAssets();
    //}
}
