using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EthereumTapButton : MonoBehaviour
{
    private int ethCount = 1;
    public static event System.Action<float> OnTapEthButton;
    private Vector3 originalScale;
    private Image buttonImage;
    void Start()
    {
        buttonImage = GetComponent<Image>();
        originalScale = transform.localScale;
    }

    public void TapEthButton()
    {
        transform.DOScale(originalScale * 0.9f, 0.1f)
            .OnComplete(() => transform.DOScale(originalScale, 0.1f));

        OnTapEthButton?.Invoke(ethCount);
    }
}
