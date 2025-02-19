using UnityEngine;

public class PauseControl : MonoBehaviour
{
    float previoustime = 1;
    public GameObject settingMenu;
    public static bool isPause;



    public void Pause()
    {
        if (Time.timeScale > 0)
        {
            previoustime = Time.timeScale;
            Time.timeScale = 0;
            AudioManager.Instance.PlaySFX("click");
            isPause = true;
            settingMenu.SetActive(true);
        }
        else if (Time.timeScale == 0)
        {
            Time.timeScale = previoustime;
            AudioManager.Instance.PlaySFX("click");
            isPause = false;
            settingMenu.SetActive(false);
        }
    }
}
