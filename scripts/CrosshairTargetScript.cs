using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairTarget : MonoBehaviour
{
    Camera maincam;
    Ray ray;
    RaycastHit hitinfo;
    
    // Start is called before the first frame update
    void Start()
    {
        maincam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        ray.origin = maincam.transform.position;
        ray.direction = maincam.transform.forward;
        Physics.Raycast(ray, out hitinfo);
        transform.position = hitinfo.point;
    }
}
