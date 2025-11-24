using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Day1DialogueManager : MonoBehaviour
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
        "이 아이... 아직도 날 경계하는 걸까?",
        "어떻게 다가가야... 부담스럽지 않을까?"
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

        choose1Text.text = "손등을 내밀고 냄새를 맡게 해본다";
        choose2Text.text = "일단 안아 들고 털을 만져본다";
        choose3Text.text = "그냥 혼자 있게 둔다";
    }

    public void OnChoose1()
    {
        PlayerPrefs.SetInt("affection", PlayerPrefs.GetInt("affection", 0) + 20);
        StartResult(new string[] {
            "고양이는 조심스럽게 냄새를 맡는다. 그리고 눈을 깜빡인다.",
            "...조금 마음의 문을 열어준 건가?",
            "호감도 + 20"
        });
    }

    public void OnChoose2()
    {
        PlayerPrefs.SetInt("affection", Mathf.Max(0, PlayerPrefs.GetInt("affection", 0) - 20));
        StartResult(new string[] {
            "고양이는 몸을 잔뜩 웅크리며 도망간다.",
            "앗... 너무 성급했나 봐.",
            "호감도 - 20"
        });
    }

    public void OnChoose3()
    {
        StartResult(new string[] {
            "고양이는 가만히 엎드린 채, 눈만 깜빡인다.",
            "...아직은 준비가 안 됐을지도 몰라.",
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