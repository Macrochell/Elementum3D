using UnityEngine;

public class ChangeMaterialElement : MonoBehaviour
{
    public Material materialElement;
    void Start()
    {
        Renderer elementRenderer = GetComponent<Renderer>();
        elementRenderer.material = materialElement;

    }

}
