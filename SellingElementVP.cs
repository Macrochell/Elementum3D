using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class SellingElementVP : MonoBehaviour
{
    [SerializeField] private Transform VfxSellElement;
    [SerializeField] private GameObject ElementObject;
    [SerializeField] private WorldState worldState;
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
            transform.DOScale(originalScale * 1.3f, 0.4f) 
            .SetEase(Ease.OutQuad)
            .OnComplete(() =>
                transform.DOScale(originalScale, 0.4f).SetEase(Ease.InQuad));
            worldState.AddEvent("\"Этот элемент преобразован в эфир, его энергия наполнит будущие миры.\"");
            ElementObject = collision.gameObject;
            ElementSO_Holder elementSO_Holder = ElementObject.GetComponent<ElementSO_Holder>();
            OnSellingElement?.Invoke(elementSO_Holder.elementSO.vpCount);
            //AddShield();
            //AudioManager.Instance.PlaySFX("MergeWeapon");
            //Instantiate(vfxdestroy, robotObject.transform.position, robotObject.transform.rotation);
            Destroy(ElementObject);
            ElementObject = null;

        }

    }

    //public void AddShield()
    //{
    //    RobotPowerInfo robotPower = robotObject.GetComponent<RobotPowerInfo>();
    //    addShieldText.text = "+" + robotPower.powerRobot.ToString();
    //    showaddShield.SetActive(true);
    //    StartCoroutine(nameof(TimerAddShield));
    //}

    //IEnumerator TimerAddShield()
    //{
    //    yield return new WaitForSeconds(2.5f);
    //    showaddShield.SetActive(false);
    //}
}
