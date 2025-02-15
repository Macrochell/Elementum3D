using UnityEngine;

public class ChangeMaterialElement : MonoBehaviour
{
    [SerializeField] private Material MaterialElement;
    void Start()
    {
        Renderer elementRenderer = GetComponent<Renderer>();
        elementRenderer.material = MaterialElement;

    }


}
