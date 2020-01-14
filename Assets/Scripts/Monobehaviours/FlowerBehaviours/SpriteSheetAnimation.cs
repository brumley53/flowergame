using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteSheetAnimation : MonoBehaviour
{
    [HideInInspector]
    public SpriteRenderer spriteRenderer;
    private Coroutine animationRoutine;
    private int currentFrame = 0;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void setSpriteSheet(SpriteSheet spriteSheet)
    {
        if (animationRoutine != null)
        {
            StopCoroutine(animationRoutine);
        }
        currentFrame = 0;
        animationRoutine = StartCoroutine(AnimationRoutine(spriteSheet));
    }

    IEnumerator AnimationRoutine(SpriteSheet spriteSheet)
    {
        while (true)
        {
            spriteRenderer.sprite = spriteSheet.frames[currentFrame];
            yield return new WaitForSeconds(1 / spriteSheet.framesPerSecond);
            currentFrame = (currentFrame + 1) % spriteSheet.frames.Length;
        }
    }
}
