using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EnergyTapAnimation : MonoBehaviour
{
    public Button energyButton;
    public GameObject energyPrefab;
    public Transform energyCounter;

    public int numberOfImage;
    public float duration;

    private void Start()
    {
        energyButton.onClick.AddListener(OnEnergyButtonClicked);
    }

    private void OnEnergyButtonClicked()
    {
        StartCoroutine(AnimateEnergy());
    }

    private IEnumerator AnimateEnergy()
    {
        Vector3 buttonPosition = energyButton.transform.position;

        for (int i = 0; i < numberOfImage; i++)
        {
            GameObject energy = Instantiate(energyPrefab, buttonPosition, Quaternion.identity, transform);

            Vector3 targetPosition = energyCounter.position;

         
            energy.transform.DOMove(targetPosition, duration)
                .SetEase(Ease.InOutQuad)
                .OnComplete(() => Destroy(energy));

            yield return new WaitForSeconds(0.07f);
        }
    }
}
