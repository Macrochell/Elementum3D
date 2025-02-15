using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class RecipeUIManager : MonoBehaviour
{
    [SerializeField] private Transform recipeListParent; 
    [SerializeField] private GameObject recipeUIPrefab;
    [SerializeField] private TextMeshProUGUI openElementText;
    public int openElementCount;

    private HashSet<RecipeSO> discoveredRecipes = new HashSet<RecipeSO>();

    public void AddRecipe(RecipeSO newRecipe)
    {
        if (discoveredRecipes.Contains(newRecipe)) return; 

        discoveredRecipes.Add(newRecipe);
        openElementCount++;
        openElementText.text = openElementCount + "/26".ToString();   
        GameObject recipeUIObject = Instantiate(recipeUIPrefab, recipeListParent);
        RecipeUIElement recipeUIElement = recipeUIObject.GetComponent<RecipeUIElement>();
        recipeUIElement.Setup(newRecipe);
    }
}
