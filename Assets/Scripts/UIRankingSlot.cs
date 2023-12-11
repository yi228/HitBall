using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIRankingSlot : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI rankText;
    [SerializeField] private TextMeshProUGUI countText;

    public void SetText(string _name, int _rank, long _count)
    {
        nameText.text = _name;
        rankText.text = _rank.ToString();
        countText.text = _count.ToString();
    }
}
