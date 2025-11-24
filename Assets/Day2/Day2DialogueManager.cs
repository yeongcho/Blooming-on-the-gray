using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Day2DialogueManager : MonoBehaviour
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
        "고양이 화장실... 인터넷에서 봤는데, 생각보다 까다롭네.",
        "이 아이는 어디에 익숙해져야 할까..."
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

        choose1Text.text = "고양이가 자주 가는 구석에 모래 화장실을 둔다";
        choose2Text.text = "마른 수건으로 배를 문지르며 자극한다";
        choose3Text.text = "그냥 두면 알아서 하지 않을까?";
    }

    public void OnChoose1()
    {
        PlayerPrefs.SetInt("affection", PlayerPrefs.GetInt("affection", 0) + 20);
        StartResult(new string[] {
            "고양이는 조심스럽게 냄새를 맡고, 다음엔 자연스럽게 사용하기 시작한다.",
            "다행이다... 조금 안심이네.",
            "호감도 + 20"
        });
    }

    public void OnChoose2()
    {
        PlayerPrefs.SetInt("affection", Mathf.Max(0, PlayerPrefs.GetInt("affection", 0) - 20));
        StartResult(new string[] {
            "고양이는 몸을 틀며 강하게 밀쳐낸다.",
            "앗... 너무 과했나...",
            "호감도 - 20"
        });
    }

    public void OnChoose3()
    {
        StartResult(new string[] {
            "고양이는 방 한 구석에 실수를 하고 만다.",
            "...내가 너무 방심했나 봐.",
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