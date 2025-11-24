using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainSceneManager : MonoBehaviour
{
    public TextMeshProUGUI heartAmountText;
    public TextMeshProUGUI coinAmountText;
    public TextMeshProUGUI eatAmountText;
    public Image[] heartImages;
    public Image eatImage;
    public Sprite heartGray;
    public Sprite heartPink;
    public Sprite eatGray;
    public Sprite eatFull;
    public Animator catAnimator;

    private int affection = 0;
    private int coins = 0;
    private int fullness = 0;
    private int day = 1;
    private bool hasFedToday = false;
    private bool hasPlayedToday = false;

    void Start()
    {
        LoadState();

        if (PlayerPrefs.HasKey("MiniGameResult"))
        {
            int result = PlayerPrefs.GetInt("MiniGameResult");
            bool isSuccess = false;

            if (result == 1)
            {
                isSuccess = true;
            }
            else
            {
                isSuccess = false;
            }

            OnMiniGameResult(isSuccess);

            PlayerPrefs.DeleteKey("MiniGameResult");
            SaveState();
        }

        UpdateUI();
    }

    public void OnFeedButtonClicked()
    {
        int cost;

        if (day >= 3)
        {
            cost = 2;
        }
        else
        {
            cost = 1;
        }

        if (hasFedToday || coins < cost)
            return;

        coins -= cost;
        fullness = 100;
        hasFedToday = true;
        SaveState();
        UpdateUI();
    }

    public void OnPlayButtonClicked()
    {
        if (hasPlayedToday)
            return;

        hasPlayedToday = true;
        SaveState();
        SceneManager.LoadScene("MiniGameScene");
    }

    public void OnNextDayButtonClicked()
    {
        if (fullness == 0)
        {
            SceneManager.LoadScene("BadEndingScene");
            return;
        }

        day++;
        fullness = 0;
        hasFedToday = false;
        hasPlayedToday = false;

        string nextSceneName;

        if (day == 6)
        {
            if (affection >= 80)
            {
                nextSceneName = "HappyEndingScene";
            }
            else if (affection >= 50)
            {
                nextSceneName = "NormalEndingScene";
            }
            else
            {
                nextSceneName = "BadEndingScene";
            }
        }
        else
        {
            nextSceneName = $"Day{day}Scene";
        }

        SaveState();
        SceneManager.LoadScene(nextSceneName);
    }

    public void OnMiniGameResult(bool success)
    {
        if (success)
        {
            coins += 2;
            affection += 10;
        }
        else
        {
            affection = Mathf.Max(0, affection - 10);
        }

        UpdateUI();
    }

    void UpdateUI()
    {
        coinAmountText.text = $"{coins} ÄÚÀÎ";
        eatAmountText.text = $"{fullness} / 100";

        if (fullness > 0)
        {
            eatImage.sprite = eatFull;
        }
        else
        {
            eatImage.sprite = eatGray;
        }

        for (int i = 0; i < heartImages.Length; i++)
        {
            int threshold = (i + 1) * 20;

            if (affection >= threshold)
            {
                heartImages[i].sprite = heartPink;
            }
            else
            {
                heartImages[i].sprite = heartGray;
            }
        }

        heartAmountText.text = $"{affection} / 100";

        if (catAnimator != null)
        {
            catAnimator.ResetTrigger("ToGray");
            catAnimator.ResetTrigger("ToSemiColor");
            catAnimator.ResetTrigger("ToColor");

            if (affection < 40)
            {
                catAnimator.SetTrigger("ToGray");
            }
            else if (affection < 80)
            {
                catAnimator.SetTrigger("ToSemiColor");
            }
            else
            {
                catAnimator.SetTrigger("ToColor");
            }
        }
    }

    void SaveState()
    {
        PlayerPrefs.SetInt("affection", affection);
        PlayerPrefs.SetInt("coins", coins);
        PlayerPrefs.SetInt("fullness", fullness);
        PlayerPrefs.SetInt("day", day);

        int fedValue;
        if (hasFedToday)
        {
            fedValue = 1;
        }
        else
        {
            fedValue = 0;
        }
        PlayerPrefs.SetInt("fed", fedValue);

        int playedValue;
        if (hasPlayedToday)
        {
            playedValue = 1;
        }
        else
        {
            playedValue = 0;
        }
        PlayerPrefs.SetInt("played", playedValue);

        PlayerPrefs.Save();
    }

    void LoadState()
    {
        affection = PlayerPrefs.GetInt("affection", 0);
        coins = PlayerPrefs.GetInt("coins", 0);
        fullness = PlayerPrefs.GetInt("fullness", 0);
        day = PlayerPrefs.GetInt("day", 1);
        hasFedToday = PlayerPrefs.GetInt("fed", 0) == 1;
        hasPlayedToday = PlayerPrefs.GetInt("played", 0) == 1;
    }
}