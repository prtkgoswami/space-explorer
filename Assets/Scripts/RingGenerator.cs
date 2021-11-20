using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingGenerator : MonoBehaviour
{
    [Range(3, 360)]
    [SerializeField] int segmentCount = 3;
    [SerializeField] float innerRadius;
    [SerializeField] float thickness;
    [SerializeField] Material RingMaterial;

    GameObject ring;
    MeshFilter ringMF;
    Mesh ringMesh;
    MeshRenderer ringMR;

    // Start is called before the first frame update
    void Start()
    {
        ring = new GameObject(name + "Ring");
        ring.transform.parent = transform;
        ring.transform.localPosition = Vector3.zero;
        ring.transform.localScale = Vector3.one;
        ring.transform.localRotation = Quaternion.identity;
        ringMF = ring.AddComponent<MeshFilter>();
        ringMesh = ringMF.mesh;
        ringMR = ring.AddComponent<MeshRenderer>();
        ringMR.material = RingMaterial;

        Vector3[] vertices = new Vector3[(segmentCount + 1) * 2 * 2];
        int[] triangles = new int[segmentCount * 6 * 2];
        Vector2[] uv = new Vector2[(segmentCount + 1) * 2 * 2];
        int halfway = (segmentCount + 1) * 2;

        for (int i = 0; i < segmentCount + 1; i++)
        {
            float progress = (float)i / (float)segmentCount;
            float angle = progress * 360 * Mathf.Deg2Rad;
            float x = Mathf.Sin(angle);
            float z = Mathf.Cos(angle);

            vertices[i * 2] = vertices[i * 2 + halfway] = new Vector3(x, 0f, z) * (innerRadius + thickness);
            vertices[i * 2 + 1] = vertices[i * 2 + 1 + halfway] = new Vector3(x, 0f, z) * innerRadius;

            uv[i * 2] = uv[i * 2 + halfway] = new Vector2(progress, 0f);
            uv[i * 2 + 1] = uv[i * 2 + 1 + halfway] = new Vector2(progress, 1f);

            if (i != segmentCount)
            {
                triangles[i * 12] = i * 2;
                triangles[i * 12 + 1] = triangles[i * 12 + 4] = (i + 1) * 2;
                triangles[i * 12 + 2] = triangles[i * 12 + 3] = i * 2 + 1;
                triangles[i * 12 + 5] = (i + 1) * 2 + 1;

                triangles[i * 12 + 6] = i * 2 + halfway;
                triangles[i * 12 + 7] = triangles[i * 12 + 10] = i * 2 + 1 + halfway; 
                triangles[i * 12 + 8] = triangles[i * 12 + 9] = (i + 1) * 2 + halfway;
                triangles[i * 12 + 11] = (i + 1) * 2 + 1 + halfway;
            }
        }

        ringMesh.vertices = vertices;
        ringMesh.triangles = triangles;
        ringMesh.uv = uv;
        ringMesh.RecalculateNormals();
    }
}
