using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Rendering.Universal;


public class LightControll : MonoBehaviour
{
    [SerializeField] Light2D mainLight;
    [SerializeField] private float rangeIncrease = 0.05f;
    [SerializeField] private float minRange = 0f;
    [SerializeField] private float maxRangeOuter = 40;
    [SerializeField] private float intensityMax = 53;
    //[SerializeField] private float maxRangeInner = 24.5f;
    private float startTime;
    void Start()
    {
        mainLight = GetComponent<Light2D>();
    }

    void Update()
    {
        if (Input.GetKey("e"))
        {
            if(mainLight != null)
            {
                //mainLight.pointLightInnerRadius = Mathf.Clamp(mainLight.pointLightInnerRadius + rangeIncrease, minRange, maxRangeInner);
                mainLight.pointLightOuterRadius = Mathf.Clamp(mainLight.pointLightOuterRadius + rangeIncrease, minRange, maxRangeOuter);
            }
        }
        if (Input.GetKey("q"))
        {
            if (mainLight != null)
            {
                //mainLight.pointLightInnerRadius = Mathf.Clamp(mainLight.pointLightInnerRadius - rangeIncrease, minRange, maxRangeInner);
                mainLight.pointLightOuterRadius = Mathf.Clamp(mainLight.pointLightOuterRadius - rangeIncrease, minRange, maxRangeOuter);
            }
        }
    }
    
    public void RestartLight ()
    {
        mainLight.intensity = 1.5f;
    }

    public void ReduceLight()
    {
        //mainLight.pointLightOuterRadius -= 1; 
        //float time = Mathf.PingPong(Time.time * 0.3f, 1);
        //mainLight.intensity = Mathf.Lerp(mainLight.intensity,0,time);
        StartCoroutine(LoopDelay());
    }

    IEnumerator LoopDelay()
    {
        yield return new WaitForSeconds(0.6f);
        mainLight.intensity -= 0.4f;
        yield return new WaitForSeconds(0.6f);
        mainLight.intensity -= 0.4f;
        yield return new WaitForSeconds(0.6f);
        mainLight.intensity -= 0.4f;
        yield return new WaitForSeconds(0.6f);
        mainLight.intensity -= 0.4f;
        yield return new WaitForSeconds(0.6f);
        mainLight.intensity -= 0.4f;
    }
    public void Gameclear()
    {
        StartCoroutine(Intensity());
        StartCoroutine(OuterRadius());
    }

    public IEnumerator Intensity()
    {
        for (float i = 0.05f; i < intensityMax + 1; i++)
        {
            mainLight.intensity += i;
            yield return new WaitForSeconds(0.30f);
        }
    }

    private IEnumerator OuterRadius()
    {
        for (float i = 0.05f; i < maxRangeOuter + 1; i++)
        {
            mainLight.pointLightOuterRadius += i;
            yield return new WaitForSeconds(0.18f);
        }
    }

}
