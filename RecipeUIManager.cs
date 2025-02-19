using System.Collections.Generic;
using TMPro;
using UnityEngine;
using YG;


public class RecipeUIManager : MonoBehaviour
{
    public Transform recipeListParent;
    public GameObject recipeUIPrefab;
    public TextMeshProUGUI openElementText;
    public VpSellAnimation vpSellAnimation;
    public int openElementCount;
    public static event System.Action<int> OnOpenElement;
    public HashSet<RecipeSO> discoveredRecipes = new HashSet<RecipeSO>();

    private void Start()
    {
        OpenElementDisplay();
    }

    public void AddRecipe(RecipeSO newRecipe)
    {
        if (discoveredRecipes.Contains(newRecipe)) return;
        AudioManager.Instance.PlaySFX("new resipe");
        discoveredRecipes.Add(newRecipe);
        openElementCount++;
        OpenElementDisplay();
        OnOpenElement?.Invoke(newRecipe.vpOpenCount);
        vpSellAnimation.OpennewRecipe();
        GameObject recipeUIObject = Instantiate(recipeUIPrefab, recipeListParent);
        RecipeUIElement recipeUIElement = recipeUIObject.GetComponent<RecipeUIElement>();
        recipeUIElement.Setup(newRecipe);
        
    }

    public void OpenElementDisplay()
    {
        openElementText.text = openElementCount + "/26".ToString();
    }
}
