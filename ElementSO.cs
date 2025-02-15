using UnityEngine;

[CreateAssetMenu(fileName = "NewElement", menuName = "Alchemy/Element")]
public class ElementSO : ScriptableObject
{
    public string elementName;
    public Sprite icon;
    public string description;
    public Transform prefab;
    public int vpCount;
}
