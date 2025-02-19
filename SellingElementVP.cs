using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using YG;

public class SellingElementVP : MonoBehaviour
{

    [SerializeField] private GameObject ElementObject;
    [SerializeField] private WorldState worldState;
    [SerializeField] private VpSellAnimation vpSellAnimation;
    public static event System.Action<int> OnSellingElement;
    private Vector3 originalScale;
    private Image buttonImage;

    void Start()
    {
        buttonImage = GetComponent<Image>();
        originalScale = transform.localScale;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Element")
        {
            vpSellAnimation.OnEnergyButtonClicked();
            transform.DOScale(originalScale * 1.3f, 0.4f)
            .SetEase(Ease.OutQuad)
            .OnComplete(() =>
                transform.DOScale(originalScale, 0.4f).SetEase(Ease.InQuad));

            if (YandexGame.EnvironmentData.language == "ru")
            {
                worldState.AddEvent("\"Этот элемент преобразован в эфир, его энергия наполнит будущие миры.\"");
            }
            if (YandexGame.EnvironmentData.language == "en")
            {
                worldState.AddEvent("\"This element has been transformed into Ether, its energy will fill future worlds.\"");
            }
            if (YandexGame.EnvironmentData.language == "tr")
            {
                worldState.AddEvent("\"Bu element Eter'e dönüştü, enerjisi gelecekteki dünyaları dolduracak.\"");
            }

            ElementObject = collision.gameObject;
            ElementSO_Holder elementSO_Holder = ElementObject.GetComponent<ElementSO_Holder>();
            OnSellingElement?.Invoke(elementSO_Holder.elementSO.vpCount);
            AudioManager.Instance.PlaySFX("sellElement");
            Destroy(ElementObject);
            ElementObject = null;

        }

    }


}
