using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorSprite : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite sprite;
    public float offsetX;
    public float offsetY;

    void Update()
    {
        // Set the sprite's position to the mouse position
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.x += offsetX;
        mousePos.y += offsetY;
        mousePos.z = 0f;
        transform.position = mousePos;

        // Show the sprite
        spriteRenderer.enabled = true;
        spriteRenderer.sprite = sprite;
    }

    void OnDestroy()
    {
        // Hide the sprite when the script is destroyed
        spriteRenderer.enabled = false;
    }
}
