using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereController : MonoBehaviour
{
    [SerializeField] private float shakeAmount;
    private float shakeTime;

    private Vector3 originPos;

    void Start()
    {
        originPos = transform.position;
    }
    void Update()
    {
        if (shakeTime > 0)
        {
            transform.position = Random.insideUnitSphere * shakeAmount + originPos;
            shakeTime -= Time.deltaTime;
        }
        else
        {
            shakeTime = 0f;
            transform.position = originPos;
        }
    }
    public void SetShakeTime(float _time)
    {
        shakeTime = _time;
    }
}
