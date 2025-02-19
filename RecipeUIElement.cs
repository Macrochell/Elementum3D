using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecipeUIElement : MonoBehaviour
{
    [SerializeField] private Image recipeImage;
    //[SerializeField] private TextMeshProUGUI recipeName;

    public void Setup(RecipeSO recipe)
    {
        recipeImage.sprite = recipe.recipeSprite; 
       // recipeName.text = recipe.recipeName; 
    }
}
