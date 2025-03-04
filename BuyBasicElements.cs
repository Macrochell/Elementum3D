using UnityEngine;

public class BuyBasicElements : MonoBehaviour
{
    [SerializeField] private GameObject elementPrefab;
    [SerializeField] private Transform spawtTransformposit;
    [SerializeField] private Transform vfxSpawnitem;
    [SerializeField] private EthereumManager ethereumManager;
    [SerializeField] private string nameAudio;
    public static event System.Action<float> OnBuyBasicElements;
    public int priceElement = 10;


    public void BuyElement()
    {
        if (ethereumManager.ethereumCount >= priceElement)
        {
            Instantiate(elementPrefab, spawtTransformposit.position, spawtTransformposit.rotation);
            Instantiate(vfxSpawnitem, spawtTransformposit.position, spawtTransformposit.rotation);
            OnBuyBasicElements?.Invoke(priceElement);
            AudioManager.Instance.PlaySFX(nameAudio);
        }
      
    }
}
