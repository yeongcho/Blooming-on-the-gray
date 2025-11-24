using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Day4DialogueManager : MonoBehaviour
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
        "같이 놀고 싶은데, 계속 등을 돌리네.",
        "내가 뭘 잘못한 걸까?"
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

        choose1Text.text = "고양이가 좋아하는 장난감을 찾으려고 종류를 바꿔본다";
        choose2Text.text = "한 번 놀자고 던졌는데 안 와서 포기한다";
        choose3Text.text = "고양이는 혼자 노는 거 좋아하지 않나?";
    }

    public void OnChoose1()
    {
        PlayerPrefs.SetInt("affection", PlayerPrefs.GetInt("affection", 0) + 20);
        StartResult(new string[] {
            "고양이의 눈이 반짝인다.",
            "꼬리를 살랑이며 장난감을 향해 달린다!",
            "역시 맞는 게 있었구나!",
            "호감도 + 20"
        });
    }

    public void OnChoose2()
    {
        PlayerPrefs.SetInt("affection", Mathf.Max(0, PlayerPrefs.GetInt("affection", 0) - 20));
        StartResult(new string[] {
            "고양이는 조용히 창밖을 바라본다.",
            "그냥... 오늘은 관심이 없는 걸까...",
            "호감도 - 20"
        });
    }

    public void OnChoose3()
    {
        StartResult(new string[] {
            "고양이는 가끔 장난감을 툭툭 건드릴 뿐이다.",
            "...그래도 나랑 노는 건 싫은 건 아닐까?",
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