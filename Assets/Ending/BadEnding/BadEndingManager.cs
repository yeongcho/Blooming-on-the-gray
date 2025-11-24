using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class BadEndingManager : MonoBehaviour
{
    public TextMeshProUGUI dialogText;
    private int dialogueIndex = 0;

    private string[] dialogues = new string[]
    {
        "오늘은, 이상하리만치 조용했다.",
        "이름을 불러도, 인기척이 없다.",
        "작은 쿠션 위에 남은 건...",
        "따뜻했던 온기조차 사라진 발자국뿐.",
        "...",
        "나는 아무 말도 할 수 없었다.",
        "「Bad Ending : 텅 빈 방」\n\t- 떠난 건 아이였을까, 나였을까."
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