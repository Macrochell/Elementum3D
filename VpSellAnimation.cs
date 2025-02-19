using System.Collections;
using UnityEngine;
using DG.Tweening;

public class VpSellAnimation : MonoBehaviour
{
   
    public GameObject vpPrefab;
    public Transform vpCounter;
    public Transform sellZone;

    public int numberOfImage;
    public float duration;

  

    public void OnEnergyButtonClicked()
    {
        StartCoroutine(AnimateEnergy());
    }

    public void OpennewRecipe()
    {
        StartCoroutine(AnimateEnergy());
    }

    private IEnumerator AnimateEnergy()
    {
        Vector3 buttonPosition = sellZone.transform.position;

        for (int i = 0; i < numberOfImage; i++)
        {
            GameObject energy = Instantiate(vpPrefab, buttonPosition, Quaternion.identity, transform);

            Vector3 targetPosition = vpCounter.position;

           
            energy.transform.DOMove(targetPosition, duration)
                .SetEase(Ease.InOutQuad)
                .OnComplete(() => Destroy(energy));

            yield return new WaitForSeconds(0.07f);
        }
    }
}
