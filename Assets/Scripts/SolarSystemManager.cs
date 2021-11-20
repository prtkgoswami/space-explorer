using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarSystemManager : MonoBehaviour
{
    readonly float G = 100f;
    GameObject[] Celestials;

    private void Start()
    {
        Celestials = GameObject.FindGameObjectsWithTag("Celestials");
        ApplyInitialVelocity();
    }

    private void FixedUpdate()
    {
        ApplyGravity();
    }

    void ApplyGravity()
    {
        foreach (GameObject currCelestial in Celestials)
        {
            foreach (GameObject celestial in Celestials)
            {
                if (!currCelestial.Equals(celestial))
                {
                    float m1 = currCelestial.GetComponent<Rigidbody>().mass;
                    float m2 = celestial.GetComponent<Rigidbody>().mass;
                    float r = Vector3.Distance(currCelestial.transform.position, celestial.transform.position);
                    float force = G * m1 * m2 / Mathf.Pow(r, 2);
                    Vector3 forceDir = (celestial.transform.position - currCelestial.transform.position).normalized;

                    currCelestial.GetComponent<Rigidbody>().AddForce(forceDir * force);
                }
            }
        }
    }

    void ApplyInitialVelocity()
    {
        foreach (GameObject currCelestial in Celestials)
        {
            foreach (GameObject celestial in Celestials)
            {
                if (!currCelestial.Equals(celestial))
                {
                    float m1 = currCelestial.GetComponent<Rigidbody>().mass;
                    float m2 = celestial.GetComponent<Rigidbody>().mass;
                    float r = Vector3.Distance(currCelestial.transform.position, celestial.transform.position);
                    currCelestial.transform.LookAt(celestial.transform);

                    currCelestial.GetComponent<Rigidbody>().velocity += currCelestial.transform.right * Mathf.Sqrt((G * m2) / r);
                }
            }
        }
    }
}
