using UnityEngine;

public class Scroller : MonoBehaviour
{
    public float scrollSpeed = 0.5f; // ��ũ�� �ӵ�
    public Renderer backgroundRenderer; // ���
    public Renderer groundRenderer; // ����
    public Renderer decorateRenderer; // ���

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
        // ���
        backgroundOffset = backgroundRenderer.material.mainTextureOffset;
        backgroundOffset.x += Time.deltaTime * scrollSpeed;
        backgroundRenderer.material.mainTextureOffset = backgroundOffset;

        // ����
        groundOffset = groundRenderer.material.mainTextureOffset;
        groundOffset.x += Time.deltaTime * scrollSpeed;
        groundRenderer.material.mainTextureOffset = groundOffset;

        // ���
        decorateOffset = decorateRenderer.material.mainTextureOffset;
        decorateOffset.x += Time.deltaTime * scrollSpeed;
        decorateRenderer.material.mainTextureOffset = decorateOffset;
    }
}
