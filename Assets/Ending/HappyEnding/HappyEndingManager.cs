using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class HappyEndingManager : MonoBehaviour
{
    public TextMeshProUGUI dialogText;
    public SpriteRenderer backgroundRenderer;
    public Sprite initialBackground;
    public Sprite happyBackground;
    public Sprite trustBackground;

    private int dialogueIndex = 0;

    private string[] dialogues = new string[]
    {
        "처음엔... 모든 게 흐렸어.",
        "사람도, 집도, 나 자신도.",
        "무서워서, 숨는 것밖에 못 했어.",
        "그런데 너는 기다려줬어.",
        "다가오지 않고, 도망치지도 않고.",
        "어느 순간, 세상이 조금씩... 색을 갖기 시작했어.",
        "너의 손길, 너의 목소리, 너의 존재로...",
        "그리고 알았어.",
        "널 믿어도 된다는 걸. \n나도... 사랑받아도 되는 존재라는 걸.",
        "「Happy Ending : 텅 빈 방」\n\t- 이제, 나의 세상은 당신으로 물들어요."
    };

    void Start()
    {
        dialogText.text = "";
        backgroundRenderer.sprite = initialBackground;
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

            if (dialogueIndex == 4)
            {
                backgroundRenderer.sprite = happyBackground;
            }
            else if (dialogueIndex == 6)
            {
                backgroundRenderer.sprite = trustBackground;
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
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("TitleScene");
    }
}