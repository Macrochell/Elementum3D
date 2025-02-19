using UnityEngine;
using YG;

public class SaveLoad : MonoBehaviour
{
    
    public ManagerResourseData managerResourseData;
    public ManagerElementData managerElementData;
    public RecipesManager recipesManager;
    public ManagerPlanetData managerPlanetData;



    private void OnEnable() => YandexGame.GetDataEvent += LoadDataFromCloud;
    private void OnDisable() => YandexGame.GetDataEvent -= LoadDataFromCloud;

   

    public void SaveDataToCloud()
    {

     
        managerResourseData.SaveGameData();
        managerElementData.SaveElementToCloud();
        recipesManager.SaveRecipesToCloud();
        managerPlanetData.SavePlanetState();



    }


    public void LoadDataFromCloud()
    {


    
        managerResourseData.LoadGameData();
        managerElementData.LoadElementFromCloud();
        recipesManager.LoadRecipesFromCloud();
        managerPlanetData.LoadPlanetState();    
    }
}