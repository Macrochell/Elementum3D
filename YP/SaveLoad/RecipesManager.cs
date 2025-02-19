using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using YG;

public class RecipesManager : MonoBehaviour
{
   public RecipeUIManager recipeUIManager;

    [System.Serializable]
    private class RecipeListWrapper
    {
        public List<string> recipes;
    }
    public void SaveRecipesToCloud()
    {
        List<string> recipeNames = recipeUIManager.discoveredRecipes.Select(r => r.name).ToList();
        YandexGame.savesData.recipeDataJson = JsonUtility.ToJson(new RecipeListWrapper { recipes = recipeNames });
        YandexGame.SaveProgress();
    }

    public void LoadRecipesFromCloud()
    {
        if (string.IsNullOrEmpty(YandexGame.savesData.recipeDataJson)) return;

        RecipeListWrapper wrapper = JsonUtility.FromJson<RecipeListWrapper>(YandexGame.savesData.recipeDataJson);
        foreach (string recipeName in wrapper.recipes)
        {
            RecipeSO recipe = FindRecipeByName(recipeName);
            if (recipe != null && !recipeUIManager.discoveredRecipes.Contains(recipe))
            {
                recipeUIManager.discoveredRecipes.Add(recipe);
                

                GameObject recipeUIObject = Instantiate(recipeUIManager.recipeUIPrefab, recipeUIManager.recipeListParent);
                RecipeUIElement recipeUIElement = recipeUIObject.GetComponent<RecipeUIElement>();
                recipeUIElement.Setup(recipe);
            }
        }
    }

    private RecipeSO FindRecipeByName(string recipeName)
    {
        return Resources.FindObjectsOfTypeAll<RecipeSO>().FirstOrDefault(r => r.name == recipeName);
    }

  
}
