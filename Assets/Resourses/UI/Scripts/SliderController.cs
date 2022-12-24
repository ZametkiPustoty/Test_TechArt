using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    private Slider _slider;

    public float Speed;
    public TextMeshProUGUI text;
    public TextMeshProUGUI FeelText;
    public float DurationInSeconds;

    public float TimerValue;

    private float gameTime;
    // Start is called before the first frame update
    private float progress;
    public bool ProgressiveFeel;
    public Coroutine TimerRoutine;
    void Start()
    {
        _slider = GetComponent<Slider>();
        TimerValue = DurationInSeconds;
        // progress = 100;
        TimerRoutine = StartCoroutine(Timer());
        
    }
    
    public IEnumerator Timer()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            if (TimerValue > 0)
            {
                TimerValue -= 1;
                text.text = GetTime();
            }
            else
            {
                TimerValue = 0;
            }
            FeelSlider( 100-(progress * 100),1);
            
        }
    }

    private void FeelSlider(float val, float duration)
    {
        _slider.DOValue(val, duration).OnComplete(() => { EndFeelSlider(); }).SetEase(Ease.Linear);
    }

    public string GetTime()
    {
        string str = "";
        float h = 0;
        float m = 0;
        float s = 0;

        s = (int)TimerValue%60;
        m = (int)(TimerValue / 60)%60;
        h = (int)(TimerValue / 3600);
        str = h + ":" + m + ":" + s;
        return str;
    }
    
    public void FeelSlider(float val)
    {
        ProgressiveFeel = true;
        float duration = Mathf.Abs(_slider.value - val) / Speed;
        DOTween.KillAll();
        StopCoroutine(TimerRoutine);
        _slider.DOValue(val, duration).OnComplete(() => { EndFeelSlider();}).SetEase(Ease.Linear);
        
    }

    public void EndFeelSlider()
    {
        
    }

    public void EndFlushSlider()
    {
        ProgressiveFeel = false;
        TimerValue = DurationInSeconds;
        TimerRoutine = StartCoroutine(Timer());
    }
    
    public void FlushSlider(float val)
    {
        float duration = Mathf.Abs(_slider.value - val) / Speed;
        DOTween.KillAll();
        StopCoroutine(TimerRoutine);
        _slider.DOValue(val, duration).OnComplete(() => { EndFlushSlider(); }).SetEase(Ease.Linear);
    }
    
    void Update()
    {
        if (ProgressiveFeel)
        {
            progress = _slider.value / 100f;
            TimerValue = DurationInSeconds * (1 -progress );
            text.text = GetTime();
        }
        else
        {
            progress = (TimerValue / DurationInSeconds);
        }
        FeelText.text = (int)_slider.value + "/100";
    }
}
