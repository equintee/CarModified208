using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;
using System.Threading.Tasks;

public class UIBarController : MonoBehaviour
{
    private Image bar;
    private Tweener fillSequence;
    private float increaseSpeed;
    private float decreaseSpeed;
    public bool isFuelBar;

    private void Awake()
    {
        bar = GetComponent<Image>();
        decreaseSpeed = FindObjectOfType<LevelController>().gasDecreaseSpeed;
        increaseSpeed = FindObjectOfType<LevelController>().barIncreaseSpeed;
    }

    public async Task IncrementValue(float value, float speed)
    {
        if(isFuelBar)
            DOTween.Kill(2);
        float target = Mathf.Clamp(bar.fillAmount + value, 0, 1);
        fillSequence = DOTween.To(() => bar.fillAmount, x => bar.fillAmount = x, target, increaseSpeed).SetSpeedBased();
        await fillSequence.AsyncWaitForCompletion();
    }

    public void DecrementFuelBar()
    {
        fillSequence = DOTween.To(() => bar.fillAmount, x => bar.fillAmount = x, 0.3f, decreaseSpeed).SetSpeedBased().OnComplete(() => FindObjectOfType<LevelController>().endGame(false)).SetId(2);
    }


}
