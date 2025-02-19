using UnityEngine;
using UnityEngine.UI;
using YG;

public class RewardButton : MonoBehaviour
{
    public int energy;
    public Image spawnRewardBG;
    public float rewardTime;
    private float rewardTimer = 0f;
    public bool rewarding;
    public bool readyToReward;
    public YandexGame sdk;

    public static event System.Action<float> OnRewardEnergy;

    private void Start()
    {
        readyToReward = true;
    }

    private void Update()
    {
        TimerReward();
    }

    public void RewardEnergy()
    {


        OnRewardEnergy?.Invoke(energy);
        AudioManager.Instance.PlaySFX("badMerge");
        readyToReward = false;
        rewarding = true;

    }


    void TimerReward()
    {
        if (rewarding && !readyToReward)
        {
            rewardTimer += Time.deltaTime;
            spawnRewardBG.fillAmount = rewardTimer / rewardTime;

            if (rewardTimer >= rewardTime)
            {
                rewarding = false;
                readyToReward = true;
                rewardTimer = 0f;
                spawnRewardBG.fillAmount = rewardTime;
            }
        }
    }


    public void OnEnable()
    {
        YandexGame.RewardVideoEvent += Rewarded;
    }

    public void OnDisable()
    {
        YandexGame.RewardVideoEvent -= Rewarded;
    }


    public void Rewarded(int id)
    {
        if (id == 1)
            RewardEnergy();
    }


    public void ExampleOpenRewardAd(int id)
    {

        if (readyToReward)
        {
            YandexGame.RewVideoShow(id);
        }

    }

}

