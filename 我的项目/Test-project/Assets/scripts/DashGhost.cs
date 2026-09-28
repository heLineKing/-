using UnityEngine;

public class DashGhost : MonoBehaviour
{
    private SpriteRenderer ghostRenderer;
    private float lifetime;
    private float age;
    private float startAlpha;
    public void Init(Sprite sprite, bool flipX, int sortingLayerID, int sortingOrder,Color color, float duration)
    {
        if (ghostRenderer == null)
        {
            ghostRenderer = GetComponent<SpriteRenderer>();
        }

        ghostRenderer.sprite = sprite;
        ghostRenderer.flipX = flipX;
        ghostRenderer.sortingLayerID = sortingLayerID;
        ghostRenderer.sortingOrder = sortingOrder;
        ghostRenderer.color = color;

        startAlpha = color.a;
        lifetime = duration;
        age = 0f;
    }
    private void Update()
    {
        age += Time.deltaTime;
        float t = lifetime > 0f ? age / lifetime : 1f;

        if (t >= 1f)
        {
            Destroy(gameObject);
            return;
        }

        Color c = ghostRenderer.color;
        c.a = Mathf.Lerp(startAlpha, 0f, t);
        ghostRenderer.color = c;
    }
}
