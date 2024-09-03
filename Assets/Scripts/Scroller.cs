using UnityEngine;

public class Scroller : MonoBehaviour
{
    public float scrollSpeed = 0.5f; // 스크롤 속도
    public Renderer backgroundRenderer; // 배경
    public Renderer groundRenderer; // 지면
    public Renderer decorateRenderer; // 장식

    private Vector2 backgroundOffset;
    private Vector2 groundOffset;
    private Vector2 decorateOffset;

    void Start()
    {
        if (backgroundRenderer == null || groundRenderer == null || decorateRenderer == null)
        {
            Debug.Log("null");
            this.enabled = false;
        }
    }

    void Update()
    {
        // 배경
        backgroundOffset = backgroundRenderer.material.mainTextureOffset;
        backgroundOffset.x += Time.deltaTime * scrollSpeed;
        backgroundRenderer.material.mainTextureOffset = backgroundOffset;

        // 지면
        groundOffset = groundRenderer.material.mainTextureOffset;
        groundOffset.x += Time.deltaTime * scrollSpeed;
        groundRenderer.material.mainTextureOffset = groundOffset;

        // 장식
        decorateOffset = decorateRenderer.material.mainTextureOffset;
        decorateOffset.x += Time.deltaTime * scrollSpeed;
        decorateRenderer.material.mainTextureOffset = decorateOffset;
    }
}
