using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSphere : MonoBehaviour
{
    private List<GameObject> spheres;
    [SerializeField] private float updateTime;
    private float curUpdateTime;

    void Start()
    {
        spheres = new List<GameObject>();
        foreach(Transform child in transform)
        {
            spheres.Add(child.gameObject);
            child.gameObject.SetActive(false);
        }
        curUpdateTime = updateTime;
    }
    void Update()
    {
        if(curUpdateTime < updateTime)
            curUpdateTime += Time.deltaTime;
        else
        {
            curUpdateTime = 0f;
            SetRandomSphere();
        }
    }
    private void SetRandomSphere()
    {
        int _randomNum = Random.Range(0, 9);
        ActivateOneSphere( _randomNum );
    }
    private void ActivateOneSphere(int _index)
    {
        for(int i=0; i<spheres.Count; i++)
        {
            if (i == _index)
                spheres[i].SetActive(true);
            else
                spheres[i].SetActive(false);
        }
    }
}
