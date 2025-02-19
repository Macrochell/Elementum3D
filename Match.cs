using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using YG;


public class Match : MonoBehaviour
{
    public List<RecipeSO> recipeSOList;
    public SphereCollider placeElementsAreaSphereCollider;
    public Transform itemSpawnPoint;
    public Transform successvfxSpawnitem;

    public Transform failurevfxSpawnitem;
    public RecipeUIManager recipeUIManager;
    public WorldState worldState;
    public GameObject buttonMerge;
    public GameObject buttonTap;
    public GameObject iconTap;
    public bool isMaterialAppliedStone;
    public bool isMaterialAppliedLava;
    public bool isMaterialAppliedGrass;

    private Vector3 originalScale;
    public GameObject planet;
    public Material MaterialLava;
    public Material MaterialStone;
    public Material MaterialGrass;


    public GameObject mountainsObject;
    public GameObject treeObject;
    public GameObject plantObject;
    public GameObject golemObject;


    Dictionary<string, string> elementDescriptionsRu = new Dictionary<string, string>
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

    Dictionary<string, string> elementDescriptionsEn = new Dictionary<string, string>
{

   { "Lava", "\"From fire and earth, lava was born — the burning blood of the planet.\"" },
{ "Stone", "\"Lava cooled, air embraced it, and a strong stone was born.\"" },
{ "Bacteria", "\"Life was born in a drop of water, where tiny creatures became the first inhabitants of the world.\"" },
{ "Clay", "\"Earth and water merged, creating soft clay — the foundation for future creations.\"" },
{ "Coal", "\"Years under pressure turned ancient forests into black fuel, storing energy within.\"" },
{ "Diamond", "\"Immense pressure and time transformed carbon into a shining stone of eternity.\"" },
{ "Earthquake", "\"The earth trembled, and rocks parted, revealing the planet's depths.\"" },
{ "Energy", "\"The fusion of elements unleashed pure power, capable of giving life or destruction.\"" },
{ "Glass", "\"Fire embraced sand, turning it into a transparent stone that holds light.\"" },
{ "Golem", "\"From clay and ancient magic, a giant was created, brought to life by the power of the earth.\"" },
{ "Grass", "\"The first greenery broke through the soil, reaching for the sun.\"" },
{ "Heat", "\"The power of fire heated the air, filling the world with unbearable heat.\"" },
{ "Life", "\"From the chaos of elements, something new was born — the breath of life, ready to explore the world.\"" },
{ "Mountain", "\"Giant masses rose from the depths, forming mighty peaks.\"" },
{ "Mud", "\"Earth mixed with water, creating sticky mud — the foundation of new forms.\"" },
{ "Ocean", "\"Water gathered into endless expanses, birthing depths full of mysteries.\"" },
{ "Plant", "\"From a tiny seed, green life grew, nourished by light and water.\"" },
{ "Pressure", "\"The world compressed, and its force solidified matter, creating new substances.\"" },
{ "Rain", "\"The sky wept, nourishing the land and giving life to all living things.\"" },
{ "Sand", "\"The wind eroded rocks, leaving behind endless dunes of sand.\"" },
{ "Sea", "\"Drops of water merged together, creating the power of tides and waves.\"" },
{ "Steam", "\"Heat touched water, turning it into a light mist, drifting into the sky.\"" },
{ "Swamp", "\"Water and earth merged, creating marshlands where life thrives in the shadows.\"" },
{ "Tree", "\"From a tiny sprout grew a mighty giant, stretching its branches to the sky.\"" },
{ "Volcano", "\"The earth split open, and fire burst forth, shaping new lands.\"" },
{ "Wind", "\"Air moved, bringing change and filling the world with the breath of freedom.\"" }

};

    Dictionary<string, string> elementDescriptionsTr = new Dictionary<string, string>
{

    { "Lava", "\"Ateş ve topraktan doğan lav, gezegenin yanan kanıdır.\"" },
{ "Stone", "\"Lav soğudu, hava onu sardı ve sağlam bir taş doğdu.\"" },
{ "Bacteria", "\"Hayat, bir su damlasında doğdu ve minik yaratıklar dünyanın ilk sakinleri oldu.\"" },
{ "Clay", "\"Toprak ve su birleşerek yumuşak kil oluşturdu — gelecekteki yaratımların temeli.\"" },
{ "Coal", "\"Yıllarca süren basınç, eski ormanları içinde enerji barındıran siyah yakıta dönüştürdü.\"" },
{ "Diamond", "\"Muazzam basınç ve zaman, karbonu sonsuzluğun parlayan taşına çevirdi.\"" },
{ "Earthquake", "\"Yer sallandı, kayalar ayrıldı ve gezegenin derinlikleri açığa çıktı.\"" },
{ "Energy", "\"Elementlerin birleşimi, hayat verebilen ya da yok edebilen saf gücü açığa çıkardı.\"" },
{ "Glass", "\"Ateş, kumu sararak onu ışığı saklayan şeffaf bir taşa dönüştürdü.\"" },
{ "Golem", "\"Kil ve kadim büyülerden, toprak gücüyle canlanan bir dev yaratıldı.\"" },
{ "Grass", "\"İlk yeşillik toprağı delerek güneşe doğru uzandı.\"" },
{ "Heat", "\"Ateşin gücü havayı ısıttı ve dünyayı dayanılmaz bir sıcaklıkla doldurdu.\"" },
{ "Life", "\"Elementlerin kaosundan yeni bir şey doğdu — dünyayı keşfetmeye hazır bir yaşam nefesi.\"" },
{ "Mountain", "\"Devasa kayalar yerin derinliklerinden yükselerek güçlü zirveler oluşturdu.\"" },
{ "Mud", "\"Toprak ve su karışarak yapışkan çamur oluşturdu — yeni formların temeli.\"" },
{ "Ocean", "\"Su sonsuz genişliklerde toplandı ve içinde gizemler barındıran derinlikleri doğurdu.\"" },
{ "Plant", "\"Küçük bir tohumdan, ışık ve suyla beslenen yeşil bir hayat büyüdü.\"" },
{ "Pressure", "\"Dünya sıkıştı ve gücü maddeyi sertleştirerek yeni yapılar oluşturdu.\"" },
{ "Rain", "\"Gökyüzü ağladı, toprağı besledi ve tüm canlılara hayat verdi.\"" },
{ "Sand", "\"Rüzgar kayaları aşındırarak ardında sonsuz kum tepeleri bıraktı.\"" },
{ "Sea", "\"Su damlaları birleşerek gelgitlerin ve dalgaların gücünü yarattı.\"" },
{ "Steam", "\"Isı suya dokundu ve onu gökyüzüne yükselen hafif bir sise dönüştürdü.\"" },
{ "Swamp", "\"Su ve toprak birleşerek gölgelerde hayatın geliştiği bataklıkları oluşturdu.\"" },
{ "Tree", "\"Küçük bir filizden, gökyüzüne dallarını uzatan güçlü bir dev büyüdü.\"" },
{ "Volcano", "\"Yeryüzü yarıldı ve ateş dışarı fırlayarak yeni topraklar oluşturdu.\"" },
{ "Wind", "\"Hava hareket etti, değişimi getirdi ve dünyayı özgürlüğün nefesiyle doldurdu.\"" }

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
                AudioManager.Instance.PlaySFX("merge");

                recipeUIManager.AddRecipe(recipe);

                transform.DOScale(originalScale * 1.1f, 0.2f)
            .SetEase(Ease.OutQuad)
            .OnComplete(() =>
                transform.DOScale(originalScale, 0.2f).SetEase(Ease.InQuad));

                if (YandexGame.EnvironmentData.language == "ru")
                {
                    if (elementDescriptionsRu.TryGetValue(recipe.outputElementSO.name, out string description))
                    {
                        worldState.AddEvent(description);
                    }
                }

                if (YandexGame.EnvironmentData.language == "en")
                {
                    if (elementDescriptionsEn.TryGetValue(recipe.outputElementSO.name, out string description))
                    {
                        worldState.AddEvent(description);
                    }
                }

                if (YandexGame.EnvironmentData.language == "tr")
                {
                    if (elementDescriptionsTr.TryGetValue(recipe.outputElementSO.name, out string description))
                    {
                        worldState.AddEvent(description);
                    }
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
                    golemObject.SetActive(true);
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
                AudioManager.Instance.PlaySFX("badMerge");
                if (YandexGame.EnvironmentData.language == "ru")
                {
                    worldState.AddEvent("\"Что-то не так, но ты на правильном пути. Попробуй снова, и ответы откроются!\"");
                }
                if (YandexGame.EnvironmentData.language == "en")
                {
                    worldState.AddEvent("\"Something's wrong, but you're on the right track. Try again, and the answers will be revealed!\"");
                }
                if (YandexGame.EnvironmentData.language == "tr")
                {
                    worldState.AddEvent("\"Bir şeyler yanlış, ama doğru yoldasın.Yeniden dene ve cevaplar açılacak!\"");
                }
               
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
