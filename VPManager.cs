using TMPro;
using UnityEngine;

public class VPManager : MonoBehaviour
{
    public int vpCount;
    [SerializeField]
    TextMeshProUGUI vpCountDisplay;

    private void Start()
    {
        VPDisplay();
    }
    public void VPDisplay()
    {
        vpCountDisplay.text = vpCount.ToString();
    }
    private void OnEnable()
    {
        SellingElementVP.OnSellingElement += AddVP;
        RecipeUIManager.OnOpenElement += AddVP;
    }

    private void OnDisable()
    {
        SellingElementVP.OnSellingElement -= AddVP;
        RecipeUIManager.OnOpenElement -= AddVP;
    }

    private void AddVP(int amount)
    {
        vpCount += amount;
        VPDisplay();
    }

    private void RemoveVP(int amount)
    {

        vpCount -= amount;
        VPDisplay();
    }
}