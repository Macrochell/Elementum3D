using UnityEngine;

public class ShowTimerAds : MonoBehaviour
{
    [SerializeField]
    private GameObject ground;

    public void Show()
    {

        Time.timeScale = 0f;
        int LayerIgnoreRaycast = LayerMask.NameToLayer("Default");
        ground.layer = LayerIgnoreRaycast;

    }

    public void ShowEnd()
    {

        int LayerIgnoreRaycast = LayerMask.NameToLayer("Ground");
        ground.layer = LayerIgnoreRaycast;
    }
}
