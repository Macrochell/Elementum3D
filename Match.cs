using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Match : MonoBehaviour
{
    [SerializeField] private List<RecipeSO> recipeSOList;
    [SerializeField] private SphereCollider placeElementsAreaSphereCollider;
    [SerializeField] private Transform itemSpawnPoint;
    [SerializeField] private Transform successvfxSpawnitem;

    [SerializeField] private Transform failurevfxSpawnitem;
    [SerializeField] private RecipeUIManager recipeUIManager;
    [SerializeField] private WorldState worldState;
    [SerializeField] private GameObject buttonMerge;
    [SerializeField] private GameObject buttonTap;
    [SerializeField] private GameObject iconTap;
    private bool isMaterialAppliedStone;
    private bool isMaterialAppliedLava;
    private bool isMaterialAppliedGrass;

    private Vector3 originalScale;
    [SerializeField] private GameObject planet;

    [SerializeField] private Material MaterialLava;
    [SerializeField] private Material MaterialStone;
    [SerializeField] private Material MaterialGrass;


    [SerializeField] private GameObject mountainsObject;
    [SerializeField] private GameObject treeObject;
    [SerializeField] private GameObject plantObject;
    [SerializeField] private GameObject GolemObject;

    Dictionary<string, string> elementDescriptions = new Dictionary<string, string>
{
    { "Lava", "\"Из огня и земли родилась лава — пылающая кровь планеты.\"" },
    { "Stone", "\"Лава остыла, воздух обнял её, и родился крепкий камень.\"" },
    { "Bacteria", "\"Жизнь родилась в капле воды, где крошечные создания стали первыми обитателями мира.\"" },
    { "Clay", "\"Земля и вода слились воедино, породив мягкую глину — основу для будущих творений.\"" },
    { "Coal", "\"Годы под давлением превратили древние леса в чёрное топливо, хранящее в себе энергию.\"" },
    { "Diamond", "\"Чудовищное давление и время превратили углерод в сияющий камень вечности.\"" },
    { "Earthquake", "\"Земля вздрогнула, и скалы разошлись, открывая глубины недр планеты.\"" },
    { "Energy", "\"Слияние стихий высвободило чистую силу, способную дарить жизнь или разрушение.\"" },
    { "Glass", "\"Огонь обнял песок, превратив его в прозрачный камень, что хранит свет.\"" },
    { "Golem", "\"Из глины и древних чар был создан великан, оживший благодаря силе земли.\"" },
    { "Grass", "\"Первая зелень пробилась сквозь землю, устремившись к солнцу.\"" },
    { "Heat", "\"Сила огня раскалила воздух, наполнив мир нестерпимым жаром.\"" },
    { "Life", "\"Из хаоса стихий родилось нечто новое — дыхание жизни, готовое познавать мир.\"" },
    { "Mountain", "\"Гигантские глыбы поднялись из недр, создавая могучие вершины.\"" },
    { "Mud", "\"Земля смешалась с водой, создавая вязкую грязь — основу новых форм.\"" },
    { "Ocean", "\"Вода собралась в бескрайние просторы, рождая глубины, полные тайн.\"" },
    { "Plant", "\"Из маленького семени выросла зелёная жизнь, питаемая светом и водой.\"" },
    { "Pressure", "\"Мир сжался, и его сила уплотнила вещество, создавая новые материи.\"" },
    { "Rain", "\"Небо заплакало, питая землю и даруя жизнь всему живому.\"" },
    { "Sand", "\"Ветер разрушил камни, оставив после себя бескрайние песчаные дюны.\"" },
    { "Sea", "\"Капли воды слились воедино, рождая мощь приливов и волн.\"" },
    { "Steam", "\"Жар коснулся воды, и она превратилась в лёгкую дымку, уносящуюся в небо.\"" },
    { "Swamp", "\"Вода и земля слились, создавая топкие земли, где жизнь процветает в тени.\"" },
    { "Tree", "\"Из крошечного ростка вырос могучий великан, раскинувший ветви к небесам.\"" },
    { "Volcano", "\"Земля разверзлась, и огонь вырвался наружу, творя новые земли.\"" },
    { "Wind", "\"Воздух пришёл в движение, неся перемены и наполняя мир дыханием свободы.\"" }
};


    void Start()
    {
        originalScale = transform.localScale;
    }

    private void Update()
    {
        
        Collider[] colliderArray = Physics.OverlapSphere(
            transform.position + placeElementsAreaSphereCollider.center,
            placeElementsAreaSphereCollider.radius);

        int elementCount = 0;

       
        foreach (Collider collider in colliderArray)
        {
            if (collider.TryGetComponent(out ElementSO_Holder elementSO_Holder))
            {
                elementCount++;
            }
        }

        
        if (elementCount == 4)
        {
            buttonMerge.SetActive(true);
            buttonTap.SetActive(false);
            iconTap.SetActive(false);
        }
        else
        {
            buttonMerge.SetActive(false);
            buttonTap.SetActive(true);
            iconTap.SetActive(true);
        }
    }


    public void Craft()
    {
        Collider[] colliderArray = Physics.OverlapSphere(
    transform.position + placeElementsAreaSphereCollider.center,
    placeElementsAreaSphereCollider.radius);

        List<ElementSO> presentElements = new List<ElementSO>();
        Dictionary<ElementSO, List<GameObject>> elementToGameObjects = new Dictionary<ElementSO, List<GameObject>>();

        foreach (Collider collider in colliderArray)
        {
            if (collider.TryGetComponent(out ElementSO_Holder elementSO_Holder))
            {
                presentElements.Add(elementSO_Holder.elementSO);

                if (!elementToGameObjects.ContainsKey(elementSO_Holder.elementSO))
                {
                    elementToGameObjects[elementSO_Holder.elementSO] = new List<GameObject>();
                }
                elementToGameObjects[elementSO_Holder.elementSO].Add(collider.gameObject);
            }
        }

        bool foundExactMatch = false;
        List<GameObject> matchedObjects = new List<GameObject>();

        foreach (RecipeSO recipe in recipeSOList)
        {
            if (HasExactIngredients(presentElements, recipe.ingredientsItemSOList))
            {
                foundExactMatch = true;


                List<GameObject> toDestroy = new List<GameObject>();
                foreach (ElementSO element in recipe.ingredientsItemSOList)
                {
                    toDestroy.Add(elementToGameObjects[element][0]);
                    elementToGameObjects[element].RemoveAt(0);
                }
                matchedObjects = toDestroy;


                Instantiate(recipe.outputElementSO.prefab, itemSpawnPoint.position, itemSpawnPoint.rotation);
                Instantiate(successvfxSpawnitem, itemSpawnPoint.position, successvfxSpawnitem.rotation);

                recipeUIManager.AddRecipe(recipe);

                transform.DOScale(originalScale * 1.1f, 0.2f) 
            .SetEase(Ease.OutQuad)
            .OnComplete(() =>
                transform.DOScale(originalScale, 0.2f).SetEase(Ease.InQuad));


                if (elementDescriptions.TryGetValue(recipe.outputElementSO.name, out string description))
                {
                    worldState.AddEvent(description);
                }

                if (recipe.outputElementSO.name == "Stone" && !isMaterialAppliedStone)
                {
                    planet.SetActive(true);


                    Renderer planetRenderer = planet.GetComponent<Renderer>();
                    if (planetRenderer != null)
                    {
                        planetRenderer.material = MaterialStone;
                    }

                    isMaterialAppliedStone = true;


                }

                if (recipe.outputElementSO.name == "Lava" && !isMaterialAppliedLava)
                {
                    planet.SetActive(true);


                    Renderer planetRenderer = planet.GetComponent<Renderer>();
                    if (planetRenderer != null)
                    {
                        planetRenderer.material = MaterialLava;
                    }

                    isMaterialAppliedLava = true;
                }

                if (recipe.outputElementSO.name == "Grass" && !isMaterialAppliedGrass)
                {
                    planet.SetActive(true);


                    Renderer planetRenderer = planet.GetComponent<Renderer>();
                    if (planetRenderer != null)
                    {
                        planetRenderer.material = MaterialGrass;
                    }

                    isMaterialAppliedGrass = true;
                }


                if (recipe.outputElementSO.name == "Tree")
                {
                    planet.SetActive(true);
                    treeObject.SetActive(true);
                }

                if (recipe.outputElementSO.name == "Plant")
                {
                    planet.SetActive(true);
                    plantObject.SetActive(true);
                }

                if (recipe.outputElementSO.name == "Golem")
                {
                    planet.SetActive(true);
                    GolemObject.SetActive(true);
                }

                if (recipe.outputElementSO.name == "Mountain")
                {
                    planet.SetActive(true);
                    mountainsObject.SetActive(true);
                }

                break;
            }
        }

        if (foundExactMatch)
        {
            foreach (GameObject obj in matchedObjects)
            {
                Destroy(obj);
            }
        }
        else
        {

            if (presentElements.Count == 4)
            {
                List<GameObject> allObjectsToDestroy = new List<GameObject>();
                foreach (var objList in elementToGameObjects.Values)
                {
                    allObjectsToDestroy.AddRange(objList);
                }

                foreach (GameObject obj in allObjectsToDestroy)
                {
                    Destroy(obj);
                }
                Instantiate(failurevfxSpawnitem, itemSpawnPoint.position, failurevfxSpawnitem.rotation);
                worldState.AddEvent("\"Что-то не так, но ты на правильном пути. Попробуй снова, и ответы откроются!\"");
                transform.DOScale(originalScale * 0.85f, 0.15f) 
            .SetEase(Ease.OutQuad)
            .OnComplete(() =>
                transform.DOScale(originalScale, 0.15f).SetEase(Ease.InQuad));
            }
        }
    }

    private bool HasExactIngredients(List<ElementSO> presentElements, List<ElementSO> recipeIngredients)
    {
        if (presentElements.Count != recipeIngredients.Count)
            return false;

        List<ElementSO> tempElements = new List<ElementSO>(presentElements);

        foreach (ElementSO ingredient in recipeIngredients)
        {
            if (!tempElements.Remove(ingredient))
                return false;
        }

        return tempElements.Count == 0;
    }
}
