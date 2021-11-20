using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpaceCraftMovement : MonoBehaviour
{
    [SerializeField] float MouseSensitivity = 50;
    [SerializeField] float TurnSpeed = 10f;
    [SerializeField] float MoveSpeed = 20f;
    [SerializeField] float BoostMultiplier = 5f;
    [SerializeField] TMP_Text SpeedText;
    [SerializeField] TMP_Text DistanceText;
    [SerializeField] Camera ThirdPersonCam;
    [SerializeField] Camera FirstPersonCam;
    [SerializeField] float LinkDistance = 100f;
    [SerializeField] LayerMask CelestialLayer;
    [SerializeField] bool showDistances = true;
    [SerializeField] TrailRenderer[] trails;

    GameManager gm;
    Rigidbody rb;
    GameObject[] Celestials;
    bool firstPerson;

    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        rb = GetComponent<Rigidbody>();
        Celestials = GameObject.FindGameObjectsWithTag("Celestials");
        firstPerson = false;
        showDistances = true;
    }

    private void Update()
    {
        if (gm.IsLive())
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                ChangePerspective();
            }

            if (showDistances)
            {
                CalculateDistances();
            }
        }
    }

    void FixedUpdate()
    {
        if (gm.IsLive())
        {
            if (transform.parent != null)
            {
                rb.velocity = transform.parent.GetComponent<Rigidbody>().velocity;
                rb.angularVelocity = transform.parent.GetComponent<Rigidbody>().angularVelocity;
            }

            float xRot = Input.GetAxis("Mouse X");
            float yRot = Input.GetAxis("Mouse Y");
            transform.Rotate(new Vector3(-yRot, xRot, 0f) * TurnSpeed * Time.deltaTime);

            float forward = Input.GetAxis("Vertical");
            float strafe = Input.GetAxis("Horizontal");
            rb.AddForce((transform.forward * forward + transform.right * strafe) * MoveSpeed * Time.deltaTime);

            if (Input.GetKey(KeyCode.Q))
            {
                rb.AddForce(transform.up * MoveSpeed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.E))
            {
                rb.AddForce(-transform.up * MoveSpeed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.LeftShift))
            {
                foreach (TrailRenderer trail in trails)
                    trail.emitting = true;
                rb.AddForce(transform.forward * BoostMultiplier * Time.deltaTime);
                GameObject.FindObjectOfType<AudioManager>().Play("Afterburner");
            }
            else
            {
                foreach (TrailRenderer trail in trails)
                    trail.emitting = false;
                GameObject.FindObjectOfType<AudioManager>().Stop("Afterburner");
            }

            SpeedText.text = "Speed: " + (rb.velocity.magnitude * 100f).ToString("0.00");

            Collider[] nearObjects = Physics.OverlapSphere(transform.position, LinkDistance, CelestialLayer);
            if (nearObjects.Length > 0)
            {
                transform.parent = nearObjects[0].transform;
                rb.drag = 0;
            }
            else
            {
                transform.parent = null;
                rb.drag = 3;
                rb.angularVelocity = Vector3.zero;
            }
        }
    }

    void CalculateDistances()
    {
        string text = "";
        SortedDictionary<float, string> distances = new SortedDictionary<float, string>();

        foreach (GameObject celestial in Celestials) {
            Vector3 vectorTo = celestial.transform.position - transform.position;
            float distanceTo = vectorTo.magnitude;
            distances.Add(distanceTo, celestial.name);
        }

        foreach (KeyValuePair<float, string> celestial in distances)
        {
            text += celestial.Value + ": " + celestial.Key.ToString("0.00") + "\n";
        }
        DistanceText.text = text;
    }

    void ChangePerspective()
    {
        firstPerson = !firstPerson;

        if (firstPerson)
        {
            FirstPersonCam.enabled = true;
            ThirdPersonCam.enabled = false;
        }
        else
        {
            FirstPersonCam.enabled = false;
            ThirdPersonCam.enabled = true;
        }
    }

    public void EnableTrail(bool enableFlag)
    {
        foreach (TrailRenderer trail in trails)
            trail.gameObject.SetActive(enableFlag);
    }

    public void EnableDistances(bool enableFlag)
    {
        showDistances = enableFlag;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(transform.position, LinkDistance);
    }
}
