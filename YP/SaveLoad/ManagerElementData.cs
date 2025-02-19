using UnityEngine;
using System.Collections.Generic;
using System;
using YG;
using TMPro;
using UnityEngine.UI;

public class ManagerElementData : MonoBehaviour
{
    public GameObject prefabElement;

    public List<ElementData> GetAllElementData()
    {
        ChangeMaterialElement[] elements = FindObjectsOfType<ChangeMaterialElement>();
        List<ElementData> allElementsData = new List<ElementData>();

        foreach (var element in elements)
        {
            ElementSO_Holder holder = element.GetComponent<ElementSO_Holder>();
            Image image = element.GetComponentInChildren<Image>(); 
            

            ElementData data = new ElementData
            {
                position = element.transform.position,
                rotation = element.transform.rotation,
                materialElement = element.materialElement,
                elementID = element.gameObject.GetInstanceID().ToString(),
                spriteName = image != null && image.sprite != null ? image.sprite.name : "", 
                nameElementRu = holder.nameElementRu,
                nameElementEn = holder.nameElementEn,
                nameElementTr = holder.nameElementTr,

                elementSO = holder != null && holder.elementSO != null ? holder.elementSO.name : "",
                
               
            };

            allElementsData.Add(data);
        }

        return allElementsData;
    }

    public void RestoreElementData(List<ElementData> savedElementData)
    {
        if (savedElementData == null || savedElementData.Count == 0)
        {
            return;
        }

        ChangeMaterialElement[] existingElements = FindObjectsOfType<ChangeMaterialElement>();

        foreach (var savedData in savedElementData)
        {
            ChangeMaterialElement element = Array.Find(existingElements, c => c.gameObject.GetInstanceID().ToString() == savedData.elementID);

            if (element == null)
            {
                GameObject newElement = Instantiate(prefabElement, savedData.position, savedData.rotation);
                element = newElement.GetComponent<ChangeMaterialElement>();
            }

            element.transform.position = savedData.position;
            element.transform.rotation = savedData.rotation;
            element.materialElement = savedData.materialElement;

            Renderer renderer = element.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material = savedData.materialElement;
            }

            ElementSO_Holder holder = element.GetComponent<ElementSO_Holder>();
            if (holder != null && !string.IsNullOrEmpty(savedData.elementSO))
            {
                
                holder.elementSO = Resources.Load<ElementSO>("ElementSO/" + savedData.elementSO);
                holder.nameElementRu = savedData.nameElementRu;
                holder.nameElementEn = savedData.nameElementEn;
                holder.nameElementTr = savedData.nameElementTr;
                holder.UpdateElementName();
            }

                
            Image image = element.GetComponentInChildren<Image>();
            if (image != null && !string.IsNullOrEmpty(savedData.spriteName))
            {
                image.sprite = Resources.Load<Sprite>("Sprites/" + savedData.spriteName);
            }

           
        }
    }

    public void SaveElementToCloud()
    {
        List<ElementData> elementData = GetAllElementData();
        YandexGame.savesData.elementDataJson = JsonUtility.ToJson(new ElementListWrapper { elements = elementData });
        YandexGame.SaveProgress();
    }

    public void LoadElementFromCloud()
    {
        if (!string.IsNullOrEmpty(YandexGame.savesData.elementDataJson))
        {
            ElementListWrapper wrapper = JsonUtility.FromJson<ElementListWrapper>(YandexGame.savesData.elementDataJson);
            RestoreElementData(wrapper.elements);
        }
    }
}
