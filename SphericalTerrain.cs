using UnityEngine;

public class SphericalTerrain : MonoBehaviour
{
    
    public float rotationSpeed;
    [SerializeField] private GameObject planet;


    

    //private void Start()
    //{

    //    Mesh mesh = GetComponent<MeshFilter>().mesh;
    //    Vector3[] vertices = mesh.vertices; 
    //    Vector3[] newVertices = new Vector3[vertices.Length];

    //    for (int i = 0; i < vertices.Length; i++)
    //    {
    //        Vector3 vertex = vertices[i];


    //        float noise = Mathf.PerlinNoise(vertex.x * noiseScale, vertex.z * noiseScale);
    //        vertex += vertex.normalized * noise * heightMultiplier;


    //        newVertices[i] = vertex;
    //    }


    //    mesh.vertices = newVertices;


    //    mesh.RecalculateNormals();


    //    MeshCollider meshCollider = GetComponent<MeshCollider>();
    //    if (meshCollider != null)
    //    {
    //        meshCollider.sharedMesh = mesh; 
    //    }
    //}

    private void Update()
    {
        
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == planet)
                {
                    planet.SetActive(false);
                }
            }
        }
    }

    public void ShowPlanet()
    {
        planet.SetActive(!planet.activeSelf);
        AudioManager.Instance.PlaySFX("click");
    }
}
