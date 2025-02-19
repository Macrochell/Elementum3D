using TMPro;
using UnityEngine;
using YG;

public class ElementSO_Holder : MonoBehaviour
{
    public ElementSO elementSO;
    public TextMeshProUGUI textElementName;
    public string nameElementRu;
    public string nameElementEn;
    public string nameElementTr;

    private void Start()
    {

        UpdateElementName();
    }


    public void UpdateElementName()
    {
        if (YandexGame.EnvironmentData.language == "ru")
        {
            textElementName.text = nameElementRu.ToString();
        }
        if (YandexGame.EnvironmentData.language == "en")
        {
            textElementName.text = nameElementEn.ToString();
        }
        if (YandexGame.EnvironmentData.language == "tr")
        {
            textElementName.text = nameElementTr.ToString();
        }
        
    }
}
