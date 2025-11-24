using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Day3DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogText;
    public GameObject choose1Button;
    public GameObject choose2Button;
    public GameObject choose3Button;
    public TextMeshProUGUI choose1Text;
    public TextMeshProUGUI choose2Text;
    public TextMeshProUGUI choose3Text;

    private int dialogueIndex = 0;

    private string[] initialDialogues = new string[] {
        "사료 가격이 이렇게나 비쌌나...",
        "물가 상승으로 인해 사료당 냥코인 2개!",
        "그래도 이 아이 건강이 제일 중요한데."
    };

    private string[] resultDialogues = null;
    private int resultIndex = 0;
    private bool awaitingChoice = false;
    private bool showingResult = false;

    void Start()
    {
        choose1Button.SetActive(false);
        choose2Button.SetActive(false);
        choose3Button.SetActive(false);
        dialogText.text = "";
        ShowNextDialogue();
    }

    public void OnDialogBarClicked()
    {
        if (awaitingChoice) return;

        if (showingResult)
        {
            ShowNextResult();
        }
        else
        {
            ShowNextDialogue();
        }
    }

    void ShowNextDialogue()
    {
        if (dialogueIndex < initialDialogues.Length)
        {
            dialogText.text = initialDialogues[dialogueIndex];
            dialogueIndex++;
        }
        else
        {
            ShowChoices();
            awaitingChoice = true;
        }
    }

    void ShowChoices()
    {
        choose1Button.SetActive(true);
        choose2Button.SetActive(true);
        choose3Button.SetActive(true);

        choose1Text.text = "저렴하더라도 성분을 꼭 확인해서 고른다";
        choose2Text.text = "싸면 좋은 거 아냐? 아무거나 산다";
        choose3Text.text = "그냥 전에 쓰던 거 계속 쓴다";
    }

    public void OnChoose1()
    {
        PlayerPrefs.SetInt("affection", PlayerPrefs.GetInt("affection", 0) + 20);
        StartResult(new string[] {
            "고양이는 사료를 몇 번 킁킁대고, 만족한 듯 먹는다.",
            "입맛엔 맞는 모양이네. 다행이야.",
            "호감도 + 20"
        });
    }

    public void OnChoose2()
    {
        PlayerPrefs.SetInt("affection", Mathf.Max(0, PlayerPrefs.GetInt("affection", 0) - 20));
        StartResult(new string[] {
            "고양이는 냄새만 맡고 뒤돌아선다.",
            "...입에도 안 대네.",
            "호감도 - 20"
        });
    }

    public void OnChoose3()
    {
        StartResult(new string[] {
            "고양이는 천천히 먹기 시작한다.",
            "뭐, 익숙한 게 나을지도.",
            "호감도 + 0"
        });
    }

    void StartResult(string[] lines)
    {
        choose1Button.SetActive(false);
        choose2Button.SetActive(false);
        choose3Button.SetActive(false);

        resultDialogues = lines;
        resultIndex = 0;
        showingResult = true;
        awaitingChoice = false;
        ShowNextResult();
    }

    void ShowNextResult()
    {
        if (resultDialogues != null && resultIndex < resultDialogues.Length)
        {
            dialogText.text = resultDialogues[resultIndex];
            resultIndex++;
        }
        else
        {
            showingResult = false;
            PlayerPrefs.Save();
            SceneManager.LoadScene("MainScene");
        }
    }
}