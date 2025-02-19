using UnityEngine;
using System.IO;
using YG;

public class ManagerResourseData : MonoBehaviour
{
    public VPManager vpManager;
    public EthereumManager ethereumManager;
    public RecipeUIManager recipeUIManager;
    public Tutorialmanager tutorialmanager;


    private string saveFilePath;

    private void Start()
    {
        saveFilePath = Application.persistentDataPath + "/gameData.json";
    }

    public void SaveGameData()
    {
        ResourseData gameData = new ResourseData
        {
            bestScoreVP = vpManager.vpCount,
            energyCount = ethereumManager.ethereumCount,
            openElementCount = recipeUIManager.openElementCount,
            doneTutorial = tutorialmanager.doneTutorial,

        };

        YandexGame.NewLeaderboardScores("Bestcreator", vpManager.vpCount);

        string json = JsonUtility.ToJson(gameData, true);
        File.WriteAllText(saveFilePath, json);

        SaveGameDataToCloud(gameData);
    }

    public void LoadGameData()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            ResourseData gameData = JsonUtility.FromJson<ResourseData>(json);

            vpManager.vpCount = gameData.bestScoreVP;
            ethereumManager.ethereumCount = gameData.energyCount;
            recipeUIManager.openElementCount = gameData.openElementCount;
            tutorialmanager.doneTutorial = gameData.doneTutorial;

            vpManager.VPDisplay();
            ethereumManager.EthereumDisplay();
            recipeUIManager.OpenElementDisplay();
        }


        LoadGameDataFromCloud();
    }


    private void SaveGameDataToCloud(ResourseData gameData)
    {
        string json = JsonUtility.ToJson(gameData, true);


        YandexGame.savesData.gameDataJson = json;
        YandexGame.SaveProgress();


    }


    private void LoadGameDataFromCloud()
    {

        if (!string.IsNullOrEmpty(YandexGame.savesData.gameDataJson))
        {

            string json = YandexGame.savesData.gameDataJson;
            ResourseData gameData = JsonUtility.FromJson<ResourseData>(json);

            vpManager.vpCount = gameData.bestScoreVP;
            ethereumManager.ethereumCount = gameData.energyCount;
            recipeUIManager.openElementCount = gameData.openElementCount;
            tutorialmanager.doneTutorial = gameData.doneTutorial;

            vpManager.VPDisplay();
            ethereumManager.EthereumDisplay();
            recipeUIManager.OpenElementDisplay();



        }
    }
}
