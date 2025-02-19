using UnityEngine;
using YG;

public class ManagerPlanetData : MonoBehaviour
{


    public PlanetStateData planetStateData;
    public Match match;

   
    public void SavePlanetState()
    {
        planetStateData.isMaterialAppliedStone = match.isMaterialAppliedStone;
        planetStateData.isMaterialAppliedLava = match.isMaterialAppliedLava;
        planetStateData.isMaterialAppliedGrass = match.isMaterialAppliedGrass;

       
        planetStateData.isTreeActive = match.treeObject.activeSelf;
        planetStateData.isPlantActive = match.plantObject.activeSelf;
        planetStateData.isGolemActive = match.golemObject.activeSelf;
        planetStateData.isMountainActive = match.mountainsObject.activeSelf;

      
        string json = JsonUtility.ToJson(planetStateData);
        YandexGame.savesData.planetStateJson = json;
        YandexGame.SaveProgress();
    }

   
    public void LoadPlanetState()
    {
        if (!string.IsNullOrEmpty(YandexGame.savesData.planetStateJson))
        {
            PlanetStateData loadedData = JsonUtility.FromJson<PlanetStateData>(YandexGame.savesData.planetStateJson);

            
            match.isMaterialAppliedStone = loadedData.isMaterialAppliedStone;
            match.isMaterialAppliedLava = loadedData.isMaterialAppliedLava;
            match.isMaterialAppliedGrass = loadedData.isMaterialAppliedGrass;

            match.mountainsObject.SetActive(loadedData.isMountainActive);
            match.treeObject.SetActive(loadedData.isTreeActive);
            match.plantObject.SetActive(loadedData.isPlantActive);
            match.golemObject.SetActive(loadedData.isGolemActive);
            

           
            if (match.isMaterialAppliedStone)
            {
                match.planet.GetComponent<Renderer>().material = match.MaterialStone;
            }

            if (match.isMaterialAppliedLava)
            {
                match.planet.GetComponent<Renderer>().material = match.MaterialLava;
            }

            if (match.isMaterialAppliedGrass)
            {
                match.planet.GetComponent<Renderer>().material = match.MaterialGrass;
            }
        }
    }
}
