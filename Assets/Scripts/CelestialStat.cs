using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelestialStat : MonoBehaviour
{
    [Header("Options")]
    [SerializeField] bool CountRevolutions = true;
    [SerializeField] bool ShowAxisLine = true;
    [SerializeField] bool ShowTrail = true;

    [Header("Stats")]
    [SerializeField] int RotationCount;
    [SerializeField] int RevolutionCount = 0;
    [SerializeField] float angle;

    [Header("Properties")]
    public Transform Parent;
    [SerializeField] TrailRenderer trail;
    [SerializeField] LineRenderer axisLine;
    [SerializeField] Transform Graphics;

    Vector3 initialVector, currentVector;
    float prevAngle = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (Parent != null)
            initialVector = (transform.position - Parent.position).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        if (CountRevolutions)
        {
            currentVector = (transform.position - Parent.position).normalized;
            //Debug.DrawRay(Parent.position, initialVector * 1000, Color.red);
            //Debug.DrawRay(Parent.position, currentVector * 1000, Color.yellow);

            angle = Vector3.SignedAngle(initialVector, currentVector, Vector3.up);
            if (prevAngle > 0 && angle < 0)
                RevolutionCount++;

            prevAngle = angle;
        }

        if (trail.emitting != ShowTrail)
            trail.emitting = ShowTrail;
        if (axisLine.enabled != ShowAxisLine)
            axisLine.enabled = ShowAxisLine;
    }

    private void FixedUpdate()
    {
        //Graphics.RotateAround(Graphics.up, 360);
    }

    public void EnableAxis(bool enableFlag)
    {
        ShowAxisLine = enableFlag;
    }

    public void EnableOrbits(bool enableFlag)
    {
        ShowTrail = enableFlag;
    }
}
