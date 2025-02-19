using UnityEngine;

public class FaqElements : MonoBehaviour
{
  
    public GameObject[] targetObjects;

    public void ToggleActiveState()
    {
        
        
        AudioManager.Instance.PlaySFX("click");

        foreach (GameObject obj in targetObjects)
        {
            if (obj != null)
            {
                obj.SetActive(!obj.activeSelf);
            }
        }
    }


}