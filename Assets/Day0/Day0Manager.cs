using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Day0Manager : MonoBehaviour
{
    public TextMeshProUGUI dialogText;
    public SpriteRenderer backgroundRenderer;
    public Sprite catRainBackgroundSprite;

    private int dialogueIndex = 0;

    private string[] dialogues = new string[]
    {
        "비 냄새가 진하게 밴 골목에서 \n낡고 젖은 상자가 눈에 들어왔다.",
        "조심스레 상자를 열자,",
        "안에는 작은 고양이 한 마리가 웅크리고 있었다.",
        "말라붙은 눈동자. \n움직이지 않는 몸.",
        "아무 말도, \n아무 소리도 없다.",
        "이상하게도... 나는 망설이지 않았다.",
        "그저 조용히, 이 아이를 품에 안았다.",
        "그렇게, \n나와 이 아이의 시간이 시작되었다."
    };

    void Start()
    {
        dialogText.text = "";
        ShowNextLine();
    }

    public void OnDialogClicked()
    {
        ShowNextLine();
    }

    void ShowNextLine()
    {
        if (dialogueIndex < dialogues.Length)
        {
            dialogText.text = dialogues[dialogueIndex];

            if (dialogues[dialogueIndex] == "안에는 작은 고양이 한 마리가 웅크리고 있었다.")
            {
                backgroundRenderer.sprite = catRainBackgroundSprite;
            }

            dialogueIndex++;
        }
        else
        {
            StartCoroutine(GoToTitleScene());
        }
    }

    IEnumerator GoToTitleScene()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("Day1Scene");
    }
}