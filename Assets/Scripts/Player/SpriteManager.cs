using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteManager : MonoBehaviour
{
    [SerializeField] private Transform playerSprite;
    [SerializeField] Vector2 originalScale;

    public float xFactor = 1f, yFactor = 1f;
    [SerializeField] float velocityFactor = .035f;
    private void Update()
    {
        ScaleReference();
        ReturnToOriginalScale();
    }
    public void Squash(float x, float y)
    {
        xFactor = x;
        yFactor = y;
    }
    void ScaleReference()
    {
        playerSprite.localScale = new Vector2(xFactor, yFactor);
    }
    void ReturnToOriginalScale()
    {
        if (xFactor <= 1f)
        {
            xFactor += velocityFactor;
        }

        if (xFactor >= 1f)
        {
            xFactor -= velocityFactor;
        }

        if (yFactor <= .99f)
        {
            yFactor += velocityFactor;
        }
        if (yFactor >= .99f)
        {
            yFactor -= velocityFactor;
        }

    }
}
