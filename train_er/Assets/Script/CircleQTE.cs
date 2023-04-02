using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class CircleQTE : MonoBehaviour
{
    public static CircleQTE Instance;
    [SerializeField] Slider sliderSpinner;
    [SerializeField] Slider sliderSuccess;
    [SerializeField] GameObject sliderSuccessParent;
    [SerializeField] GameObject QTEcanvas;
    Coroutine spin;
    float successRot;
    public UnityEvent onQTESuccess;
    public UnityEvent onQTEFail;
    private void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void startQTE(float areaAngle, float spintime)
    {
        sliderSpinner.transform.rotation = Quaternion.Euler(0, 0, 180);
        PlayerControl.Instance.isQTE = true;
        onQTEFail.RemoveAllListeners();
        onQTESuccess.RemoveAllListeners();
        QTEcanvas.SetActive(true);
        selectSuccessArea(areaAngle);
        startSpin(spintime);
    }

    void selectSuccessArea(float areaAngle)
    {
        Debug.Log("in select success area");
        Time.timeScale = 0;
        successRot = Random.Range(areaAngle-185, 160);
        sliderSuccess.value = areaAngle / 360;
        Debug.Log("success Rot is " + successRot);
        sliderSuccessParent.transform.rotation = Quaternion.Euler(0, 0, successRot);
    }
    void startSpin(float spintime)
    {
        spin = StartCoroutine(spinCoroutine(spintime));
    }
    IEnumerator spinCoroutine(float spintime)
    {
        yield return new WaitForSecondsRealtime(1);
        float duration=0;
        while(duration<spintime)
        {
            yield return new WaitForEndOfFrame();
            sliderSpinner.transform.Rotate(new Vector3(0, 0, -(360 / spintime) * Time.unscaledDeltaTime));
            duration += Time.unscaledDeltaTime;
        }
        onQTEFail.Invoke();
        Time.timeScale = 1;
        QTEcanvas.SetActive(false);
        PlayerControl.Instance.isQTE = false;
    }
    public void stop()
    {
        Debug.Log("stop spin called");
        StopCoroutine(spin);
        Time.timeScale = 1;
        if (sliderSuccessParent.transform.rotation.z > sliderSpinner.transform.rotation.z && sliderSuccessParent.transform.rotation.z-sliderSuccess.value*360 < sliderSpinner.transform.rotation.z - sliderSpinner.value * 360)
        {
            onQTESuccess.Invoke();
            Debug.Log("success!");
            //success
        }
        else
        {
            onQTEFail.Invoke();
            Debug.Log("fail!");
            //fail
        }
        QTEcanvas.SetActive(false);
        PlayerControl.Instance.isQTE = false;
    }
}
