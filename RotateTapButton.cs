using UnityEngine;

public class RotateTapButton : MonoBehaviour
{
    public float rotationSpeed;

    private void Update()
    {

        transform.Rotate(-1 *Vector3.forward * rotationSpeed * Time.deltaTime);
    }
}
