using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIAnimations : MonoBehaviour
{
    [Header("Animation Toggles")]
    public bool animateScale = true;
    public bool animatePosition = true;
    public bool animateAlpha = true;

    [Header("Animated Components")]
    public List<RectTransform> scaleTargets = new List<RectTransform>();
    public List<RectTransform> positionTargets = new List<RectTransform>();
    public List<CanvasGroup> alphaTargets = new List<CanvasGroup>();

    [Header("Animation Settings")]
    public float animationDuration = 0.5f;
    public Vector3 showScale = Vector3.one;
    public Vector3 hideScale = Vector3.zero;
    public Vector3 showPosition = Vector3.zero;
    public Vector3 hidePosition = new Vector3(0, -200, 0);
    public float showAlpha = 1f;
    public float hideAlpha = 0f;

    private float animationTime = 0f;
    private bool isShowing = false;
    private bool isAnimating = false;
    private bool isHiding = false;

    void Start()
    {
        
    }

    void Update()
    {
        if (isAnimating)
        {
            animationTime += (isShowing ? 1 : -1) * Time.deltaTime;
            float t = Mathf.Clamp01(animationTime / animationDuration);

            if (animateScale)
            {
                foreach (var target in scaleTargets)
                {
                    target.localScale = Vector3.Lerp(hideScale, showScale, t);
                }
            }

            if (animatePosition)
            {
                foreach (var target in positionTargets)
                {
                    target.anchoredPosition = Vector3.Lerp(hidePosition, showPosition, t);
                }
            }

            if (animateAlpha)
            {
                foreach (var target in alphaTargets)
                {
                    target.alpha = Mathf.Lerp(hideAlpha, showAlpha, t);
                }
            }

            if ((isShowing && t >= 1f) || (!isShowing && t <= 0f))
            {
                isAnimating = false;
            }
        }
    }

    public void ShowUI()
    {
        isShowing = true;
        isAnimating = true;
        isHiding = false;
        if (animationTime < 0f) animationTime = 0f;
    }

    public void HideUI()
    {
        isShowing = false;
        isAnimating = true;
        isHiding = true;
        if (animationTime > animationDuration) animationTime = animationDuration;
    }
}
