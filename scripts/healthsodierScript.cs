using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthsodier : MonoBehaviour
{

    [SerializeField] GameObject riffleammo;
    [SerializeField] GameObject pistolammo;
    public int maxhealth = 100;
    public int currhealth;
    public healthbar bar;

    private bool flag = false;

    private Transform _transform;

    // Start is called before the first frame update
    void Start()
    {
        currhealth = maxhealth;
        //currhealth = 0;
        bar.setmaxhealth(maxhealth);
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    takedmg(10);
        //}

        //currhealth -= (int)Time.deltaTime;
            
        if(currhealth <= 0)
        {
            

            if(flag == false)
            {
                if (this.transform.position.y <= -3)
                {
                    Destroy(this.gameObject);
                }

           /*     Vector3 position = transform.position;
                position.y = Terrain.activeTerrain.SampleHeight(transform.position) + 0.1f;
*/
                int droprate = Random.Range(0, 2);
                Debug.Log("ngedrop " +droprate);

                if (droprate == 0)
                {
                    int droprate2 = Random.Range(0, 2);
                    Debug.Log(droprate2);
                    Debug.Log("kbuat ammo");

                    if (droprate2 == 0)
                    {
                        Debug.Log("kbuat riffle");

                        Instantiate(riffleammo, transform.position, Quaternion.identity);
                    }
                    else
                    {
                        Debug.Log(droprate2);
                        Debug.Log("kbuat pistol");

                        Instantiate(pistolammo, transform.position, Quaternion.identity);
                    }
                }
                
                flag = true;
            }
            transform.position += Vector3.down * Time.deltaTime;
        }
    }

    public void takedmg(int dmg)
    {
        currhealth -= dmg;
        bar.sethealth(currhealth);
    }
}
