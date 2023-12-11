using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private TextMeshProUGUI touchCountText;
    [SerializeField] private GameObject touchEffect;

    public int curRank = 0;
    public long touchCount;

    void Start()
    {
        Init();
    }
    void Update()
    {
        DetectTouch();
        DetectEscape();
    }
    private void Init()
    {
        instance = this;
    }
    private void DetectTouch()
    {
        if (!UIManager.instance.rankingOn && Input.GetMouseButtonDown(0))
        {
            RaycastHit _touch;
            Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(_ray, out _touch);

            if (_touch.collider != null)
            {
                touchCount++;
                DBManager.instance.SaveUserInfo();
                UpdateTouchCount();
                GameObject _effect = Instantiate(touchEffect);
                _effect.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 7);
                AudioManager.instance.PlayTouchSound();
                _touch.transform.GetComponent<SphereController>().SetShakeTime(0.3f);
            }
        }
    }
    private void DetectEscape()
    {
        if (Application.platform == RuntimePlatform.Android && Input.GetKey(KeyCode.Escape))
            Application.Quit();
    }
    public void UpdateTouchCount()
    {
        StringBuilder _tempText = new StringBuilder();
        _tempText.Append("ÅÍÄ¡ È½¼ö: <color=#FF4F4F>");
        _tempText.Append(touchCount.ToString());
        _tempText.Append("</color>");
        touchCountText.text = _tempText.ToString();
    }
}