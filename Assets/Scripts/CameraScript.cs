using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] Transform ObjectToFocus;
    [SerializeField] float offset;
    [SerializeField] bool inDirectionOfParent;
    [SerializeField] bool Above;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = (ObjectToFocus.GetComponent<CelestialStat>().Parent.position - ObjectToFocus.position).normalized;
        //Debug.DrawRay(ObjectToFocus.position, dir * 200);
        if (inDirectionOfParent)
            transform.position = ObjectToFocus.position + -dir * (ObjectToFocus.localScale.x + offset);
        if (Above)
            transform.position = ObjectToFocus.position + Vector3.up * (ObjectToFocus.localScale.y + offset);
        transform.LookAt(ObjectToFocus);
    }
}
