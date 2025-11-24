using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Day5DialogueManager : MonoBehaviour
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
        "오늘은 조금 더 가까워지고 싶어.",
        "뭔가 더... 마음이 닿는 방법 없을까?"
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

        choose1Text.text = "그냥 곁에 앉아서 기다린다";
        choose2Text.text = "막 쓰다듬고 장난친다";
        choose3Text.text = "장난감 던져준다";
    }

    public void OnChoose1()
    {
        PlayerPrefs.SetInt("affection", PlayerPrefs.GetInt("affection", 0) + 20);
        StartResult(new string[] {
            "고양이가 천천히 다가와 무릎에 살짝 기대 앉는다.",
            "...이제 조금, 내 마음이 닿은 걸까?",
            "호감도 + 20"
        });
    }

    public void OnChoose2()
    {
        PlayerPrefs.SetInt("affection", Mathf.Max(0, PlayerPrefs.GetInt("affection", 0) - 20));
        StartResult(new string[] {
            "고양이가 짧게 야옹! 하고 도망간다.",
            "아... 또 너무 앞서갔나 봐.",
            "호감도 - 20"
        });
    }

    public void OnChoose3()
    {
        StartResult(new string[] {
            "고양이는 장난감을 향해 시선을 주다가 돌아눕는다.",
            "조금만 더 노력해볼까...",
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