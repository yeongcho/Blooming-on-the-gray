using UnityEngine;

public class MoveButton : MonoBehaviour
{
    public bool isUp; // true면 위로 이동 버튼, false면 아래로 이동 버튼
    private GameManager gm; // GameManager에 접근하기 위한 참조

    void Start()
    {
        // 최신 Unity 권장 방식으로 GameManager 오브젝트 탐색
        gm = Object.FindFirstObjectByType<GameManager>();
    }

    // 마우스 클릭 시 호출됨
    private void OnMouseDown()
    {
        if (gm == null) return; // GameManager 참조가 없으면 아무것도 하지 않음

        if (isUp)
            gm.MoveCatUp();    // 위로 이동
        else
            gm.MoveCatDown();  // 아래로 이동
    }
}
