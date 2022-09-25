using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;


public class UIBarController : MonoBehaviour
{
    private Image bar;
    private Tween fillSequence;
    private void Awake()
    {
        bar = GetComponent<Image>();
    }

    public void incrementValue(float value)
    {
        DOTween.Kill(fillSequence);
        float target = Mathf.Clamp(bar.fillAmount + value, 0, 1);
        fillSequence = DOTween.To(() => bar.fillAmount, x => bar.fillAmount = x, target, 0.1f).SetSpeedBased();
    }


}
