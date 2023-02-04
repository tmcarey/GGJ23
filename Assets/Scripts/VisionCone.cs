using UnityEngine;
public class VisionSphere : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private float radius = 10f;
    [SerializeField] private int segments = 32;
    [SerializeField] private LayerMask visionLayer;

    private Mesh mesh;

    private void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
    }

    private void Update()
    {
        Vector3[] vertices = new Vector3[segments + 1];
        int[] triangles = new int[segments * 3];
        Vector3 position = transform.position;

        vertices[0] = Vector3.zero;
        for (var i = 0; i < segments; i++)
        {
            var angle = 2 * Mathf.PI * i / segments;
            var direction = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle));

            var hit = Physics.Raycast(position, direction, out var hitInfo, radius, visionLayer);
            if(hit)
            {
                vertices[i + 1] = hitInfo.point - position;
            }
            else
            {
                vertices[i + 1] = direction * radius;
            }

            triangles[i * 3] = 0;
            triangles[i * 3 + 1] = (i + 1) % segments + 1;
            triangles[i * 3 + 2] = (i + 1);
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }
}


