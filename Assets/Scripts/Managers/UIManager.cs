using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public struct playerData
{
    public string name;
    public long count;
    public int rank;
}

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public bool rankingOn = false;

    [SerializeField] private TextMeshProUGUI rankingText;
    [SerializeField] private GameObject rankingPanel;
    [SerializeField] private GameObject rankingSlotPrefab;
    [SerializeField] private GameObject rankingScrollContent;
    [SerializeField] private GameObject rankLoadingText;

    public List<playerData> playerList;

    void Start()
    {
        instance = this;
        rankingPanel.SetActive(false);
        rankLoadingText.SetActive(false);
        playerList = new List<playerData>();
    }
    void Update()
    {
        UpdateRankingText();
    }
    private void UpdateRankingText()
    {
        StringBuilder _tempText = new StringBuilder();
        _tempText.Append("<color=#179AFF>");
        _tempText.Append(GameManager.instance.curRank.ToString());
        _tempText.Append("</color> / ");
        _tempText.Append(DBManager.instance.userCount.ToString());
        rankingText.text = _tempText.ToString();
    }
    public void OnClickRank()
    {
        rankingPanel.SetActive(true);
        rankingOn = true;
        ClearSlots();
        DBManager.instance.LoadRankingData();;
        StartCoroutine(CoLoadAndCreateSlots());
    }
    public void OnClickCloseRank()
    {
        rankingPanel.SetActive(false);
        rankingOn = false;
    }
    private void ClearSlots()
    {
        foreach (Transform child in rankingScrollContent.transform)
            Destroy(child.gameObject);
        playerList.Clear();
    }
    private void CreateSlot()
    {
        for(int i = 0; i<playerList.Count; i++) {
            GameObject go = Instantiate(rankingSlotPrefab, rankingScrollContent.transform);
            go.GetComponent<UIRankingSlot>().SetText(playerList[i].name, playerList[i].rank, playerList[i].count);
        }
    }
    private IEnumerator CoLoadAndCreateSlots()
    {
        rankLoadingText.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        rankLoadingText.SetActive(false);
        CreateSlot();
    }
}
