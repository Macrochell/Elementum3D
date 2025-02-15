using System.Globalization;
using TMPro;
using UnityEngine;


public class EthereumManager : MonoBehaviour
{
    public float ethereumCount;
    public float ethereumForTime;
    [SerializeField]
    TextMeshProUGUI ethereumCountDisplay;



    private void Update()
    {
        ethereumCount += ethereumForTime * Time.deltaTime;

        EthereumDisplay();
    }

    public void EthereumDisplay()
    {
        ethereumCountDisplay.text = ethereumCount.ToString("F1", CultureInfo.InvariantCulture);
    }
    private void OnEnable()
    {
        EthereumTapButton.OnTapEthButton += AddEthereum;
        BuyBasicElements.OnBuyBasicElements += RemoveEthereum;
    }

    private void OnDisable()
    {
        EthereumTapButton.OnTapEthButton -= AddEthereum;
        BuyBasicElements.OnBuyBasicElements -= RemoveEthereum;
    }

    private void AddEthereum(float amount)
    {
        ethereumCount += amount;
        EthereumDisplay();
    }

    private void RemoveEthereum(float amount)
    {

        ethereumCount -= amount;
        EthereumDisplay();
    }
}
