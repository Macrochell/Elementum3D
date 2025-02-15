using UnityEngine;

public class FaqElements : MonoBehaviour
{
    public GameObject targetObject; 

    public void ToggleActiveState()
    {
        
            targetObject.SetActive(!targetObject.activeSelf);
        
    }
}