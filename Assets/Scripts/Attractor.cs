using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractor : MonoBehaviour
{
    

    // ================================== OLDER SCRIPT =======================================
    //const float G = 7;
    //public Rigidbody rb;
    //[SerializeField] bool nudge = false;
    //[SerializeField] float nudgeForce;
    //[Range(0f, 200f)]
    //[SerializeField] float Timescale = 1;

    //private void Start()
    //{
    //    if (nudge)
    //    {
    //        rb.AddForce(Vector3.right * nudgeForce, ForceMode.Acceleration);
    //    }
    //}

    //private void Update()
    //{
    //    Attractor[] attractors = FindObjectsOfType<Attractor>();
    //    foreach (Attractor attractor in attractors)
    //    {
    //        if (attractor != this)
    //        {
    //            Attract(attractor);
    //        }
    //    }
    //}

    //void Attract(Attractor objToAttract)
    //{
    //    Rigidbody rbToAttract = objToAttract.rb;
    //    Vector3 direction = objToAttract.transform.position - transform.position;
    //    float distance = direction.magnitude;
    //    if (distance == 0)
    //        return;

    //    float force = G * rb.mass * rbToAttract.mass / Mathf.Pow(distance, 2);
    //    rbToAttract.AddForce(-direction.normalized * force * Timescale * Time.fixedDeltaTime);
    //}
}
