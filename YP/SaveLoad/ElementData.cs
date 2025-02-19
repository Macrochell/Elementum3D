using UnityEngine;
using System.Collections.Generic;
using TMPro;

[System.Serializable]
public class ElementData
{
    public Vector3 position;
    public Quaternion rotation;
    public Material materialElement;
    public string elementID;
    public string spriteName; 
    public string nameElementRu;
    public string nameElementEn;
    public string nameElementTr;
    public string elementSO;
    


}

public class ElementListWrapper
{
    public List<ElementData> elements;
}
