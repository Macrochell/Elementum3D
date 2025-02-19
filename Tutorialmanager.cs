using DG.Tweening;
using TMPro;
using UnityEngine;
using YG;
using UnityEngine.UI;

public class Tutorialmanager : MonoBehaviour
{
    [SerializeField] private float startX;
    [SerializeField] private float endX;
    [SerializeField] private float duration;
    [SerializeField] private GameObject handTutorial;
    [SerializeField] private GameObject arrowTutorial;
    public bool doneTutorial;
    public GameObject tutorialBg;
    public Image highlight;
    public TextMeshProUGUI tutorialText;
    private int currentStep = 0;


    [System.Serializable]
    public struct HighlightData
    {
        public Vector2 position;
        public Vector2 scale;
    }


    private HighlightData[] highlightData = new HighlightData[]
    {

       
        new HighlightData { position = new Vector2(0, -388), scale = new Vector2(1f, 1f) },
        new HighlightData { position = new Vector2(455, 389), scale = new Vector2(1f, 1f) },
        new HighlightData { position = new Vector2(377, -400), scale = new Vector2(1f, 1f) },
        new HighlightData { position = new Vector2(-874, 70), scale = new Vector2(0.5f, 1f) },
     
    };

    private string[] tutorialMessagesRU = new string[]
    {



        "Энергию можно получить, нажав на колодец или подождав её накопления.",

        "Ищите элементы для развития планеты и набора очков в таблице рекордов.",

        "Зарабатывайте очки, открывая рецепты или продавая элементы в зоне продажи.",

        "Создайте свой первый элемент, Земля+Земля+Огонь+Огонь, перетащив их в центр и нажав кнопку смешивания.",
            };


    private string[] tutorialMessagesEN = new string[]
   {




"Energy can be obtained by clicking on the well or waiting for it to accumulate.",

"Search for elements to develop the planet and earn points in the leaderboard.",

"Earn points by unlocking recipes or selling elements in the sales area.",

"Create your first element, Earth+Earth+Fire+Fire, by dragging them to the center and clicking the mix button.",


   };

    private string[] tutorialMessagesTR = new string[]
  {


"Enerji, kuyuya tıklayarak veya birikmesini bekleyerek elde edilebilir.",

"Gezegeni geliştirmek ve liderlik tablosunda puan kazanmak için öğeler arayın.",

"Reçeteleri açarak veya öğeleri satış alanında satarak puan kazanın.",

"İlk öğenizi oluşturun, Toprak+Toprak+Ateş+Ateş, bunları merkeze sürükleyip karıştırma butonuna tıklayarak.",


  };


    void Start()
    {
       

        if (YandexGame.EnvironmentData.language == "ru")
        {
            YandexGame.SwitchLanguage("ru");
        }
        if (YandexGame.EnvironmentData.language == "en")
        {
            YandexGame.SwitchLanguage("en");
        }
        if (YandexGame.EnvironmentData.language == "tr")
        {
            YandexGame.SwitchLanguage("tr");
        }

        if(!doneTutorial)
        {
            handTutorial.transform.DOLocalMoveX(endX, duration).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
            tutorialBg.SetActive(true);
            currentStep = 0;
            ShowStep();
        }
        

    }


    public void ShowStep()
    {


        if (YandexGame.EnvironmentData.language == "ru")
        {
            if (currentStep >= tutorialMessagesRU.Length)
            {
                tutorialBg.SetActive(false);
                arrowTutorial.SetActive(false);
                doneTutorial = true;
                return;
            }
            highlight.rectTransform.anchoredPosition = highlightData[currentStep].position;
            highlight.rectTransform.localScale = highlightData[currentStep].scale;
            tutorialText.text = tutorialMessagesRU[currentStep];
        }


        if (YandexGame.EnvironmentData.language == "tr")
        {
            if (currentStep >= tutorialMessagesTR.Length)
            {
                tutorialBg.SetActive(false);
                arrowTutorial.SetActive(false);
                doneTutorial = true;
                return;
            }
            highlight.rectTransform.anchoredPosition = highlightData[currentStep].position;
            highlight.rectTransform.localScale = highlightData[currentStep].scale;
            tutorialText.text = tutorialMessagesTR[currentStep];
        }

        if (YandexGame.EnvironmentData.language == "en")
        {
            if (currentStep >= tutorialMessagesEN.Length)
            {
                tutorialBg.SetActive(false);
                arrowTutorial.SetActive(false);
                doneTutorial = true;
                return;
            }
            highlight.rectTransform.anchoredPosition = highlightData[currentStep].position;
            highlight.rectTransform.localScale = highlightData[currentStep].scale;
            tutorialText.text = tutorialMessagesEN[currentStep];
        }
    }

    public void NextStep()
    {
        arrowTutorial.SetActive(true);
        currentStep++;
        AudioManager.Instance.PlaySFX("click");
        ShowStep();
    }
}

