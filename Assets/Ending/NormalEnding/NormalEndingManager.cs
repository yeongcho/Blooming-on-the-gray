using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class NormalEndingManager : MonoBehaviour
{
    public TextMeshProUGUI dialogText;
    private int dialogueIndex = 0;

    private string[] dialogues = new string[]
    {
        "오늘도 이 아이는 같은 자리에 앉아 있었다.",
        "아직은 조금 멀게 느껴진다.",
        "하지만, 예전처럼 숨거나 등을 돌리진 않는다.",
        "같이 있는 시간이 길어졌지만,",
        "완전히 마음을 연 건 아니야.",
        "그래도... 떠나지 않았다는 것만으로도",
        "왠지 안도감이 든다.",
        "나를 보는 건 아닐지도 모르지만,",
        "그 조용한 반응 하나에",
        "‘아직 기회는 있구나’ 싶었다.",
        "「Normal Ending : 창밖을 보는 아이」\n\t- 아직은, 멀지만 닿을 수 있을지도 몰라."
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