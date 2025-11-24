using UnityEngine;

public class ExitButton : MonoBehaviour
{
    public Sprite defaultSprite; // 기본 이미지 (Exit_button_white.png)
    public Sprite hoverSprite;   // 마우스를 올렸을 때 이미지 (Exit_button_red.png)

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        // SpriteRenderer 컴포넌트를 가져와 기본 이미지로 설정
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = defaultSprite;
    }

    void OnMouseEnter()
    {
        // 마우스가 버튼 위에 올라가면 hover 이미지로 변경
        spriteRenderer.sprite = hoverSprite;
    }

    void OnMouseExit()
    {
        // 마우스가 버튼에서 벗어나면 기본 이미지로 복원
        spriteRenderer.sprite = defaultSprite;
    }

    void OnMouseDown()
    {
        // 버튼 클릭 시 게임 종료
        Application.Quit();

        // 유니티 에디터에서 종료 테스트용
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
