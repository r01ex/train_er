using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class CircleQTE : MonoBehaviour
{
    public static CircleQTE Instance;
    [SerializeField] Slider sliderSpinner;
    [SerializeField] Slider sliderSuccess1;
    [SerializeField] Slider sliderSuccess2;
    [SerializeField] Slider sliderSuccess3;
    [SerializeField] GameObject sliderSuccessParent1;
    [SerializeField] GameObject sliderSuccessParent2;
    [SerializeField] GameObject sliderSuccessParent3;
    [SerializeField] GameObject QTEcanvas;
    [SerializeField] RawImage itemimgPlaceholder;
    Coroutine spin;
    float successRot;
    public UnityEvent onQTESuccess;
    public UnityEvent onQTEFail;
    public UnityEvent onQTEend;
    [SerializeField] int QTEstopCnt = 0;
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
        QTEstopCnt = 0;
        onQTEFail.RemoveAllListeners();
        onQTESuccess.RemoveAllListeners();
        onQTEend.RemoveAllListeners();
        QTEcanvas.SetActive(true);
        selectSuccessArea(areaAngle);
        startSpin(spintime);
    }

    void selectSuccessArea(float areaAngle)
    {
        Debug.Log("in select success area");
        Time.timeScale = 0;
        successRot = Random.Range(0, 60);
        sliderSuccess1.value = areaAngle / 360;
        sliderSuccess2.value = areaAngle / 360;
        sliderSuccess3.value = areaAngle / 360;
        sliderSuccessParent1.transform.Rotate(new Vector3(0, 0, successRot));
        successRot = Random.Range(0, 30);
        sliderSuccessParent2.transform.Rotate(new Vector3(0, 0, successRot));
        successRot = Random.Range(0, 60);
        sliderSuccessParent3.transform.Rotate(new Vector3(0, 0, successRot));
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
        //onQTEFail.Invoke();
        onQTEend.Invoke();
        Time.timeScale = 1;
        QTEcanvas.SetActive(false);
        PlayerControl.Instance.isQTE = false;
    }
    public void stop()
    {
        Debug.Log("stop spin called");
        if (QTEstopCnt < 3)
        {
            if (sliderSuccessParent1.transform.rotation.z > sliderSpinner.transform.rotation.z && sliderSuccessParent1.transform.rotation.z - sliderSuccess1.value * 360 < sliderSpinner.transform.rotation.z - sliderSpinner.value * 360)
            {
                onQTESuccess.Invoke();
                Debug.Log("slider1!");
                //success
            }
            if (sliderSuccessParent2.transform.rotation.z > sliderSpinner.transform.rotation.z && sliderSuccessParent2.transform.rotation.z - sliderSuccess2.value * 360 < sliderSpinner.transform.rotation.z - sliderSpinner.value * 360)
            {
                onQTESuccess.Invoke();
                Debug.Log("slider2!");
                //success
            }
            if (sliderSuccessParent3.transform.rotation.z > sliderSpinner.transform.rotation.z && sliderSuccessParent3.transform.rotation.z - sliderSuccess3.value * 360 < sliderSpinner.transform.rotation.z - sliderSpinner.value * 360)
            {
                onQTESuccess.Invoke();
                Debug.Log("slider3!");
                //success
            }
            else
            {
                onQTEFail.Invoke();
                Debug.Log("fail!");
                //fail
            }
        }
        QTEstopCnt++;
    }
    public void setCenterSprite(Sprite sprite)
    {
        itemimgPlaceholder.texture = sprite.texture;
    }
}
