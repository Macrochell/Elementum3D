using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewRecipe", menuName = "Alchemy/Recipe")]
public class RecipeSO : ScriptableObject
{
    public Sprite recipeSprite;
    public string recipeName;
    public List<ElementSO> ingredientsItemSOList;
    public ElementSO outputElementSO;
    


}
