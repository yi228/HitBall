using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DBManager : MonoBehaviour
{
    public static DBManager instance;
    public string clientUserName;
    public int userCount = 0;

    DatabaseReference reference;

    public class User
    {
        public string username;
        public long touchcount;
        public User(string username, long touchcount)
        {
            this.username = username;
            this.touchcount = touchcount;
        }
    }

    void Start()
    {
        Init();
    }
    private void Init()
    {
        instance = this;
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        //clientUserName = LogInScene.instance.userName;
        clientUserName = "Guest";
        LoadRankingData(true);
    }
    public void SaveUserInfo()
    {
        WriteNewUser("RankingInfo", clientUserName, GameManager.instance.touchCount);
        WriteNewUser("RankingInfo", "Tester 1", 234);
        WriteNewUser("RankingInfo", "Tester 2", 2135);
        WriteNewUser("RankingInfo", "Tester 3", 875);
        WriteNewUser("RankingInfo", "Tester 4", 6554);
    }
    private void WriteNewUser(string type, string name, long touchcount)
    {
        User user = new User(name, touchcount);
        string json = JsonUtility.ToJson(user);
        reference.Child(type).Child(name).SetRawJsonValueAsync(json);
    }
    public void LoadRankingData(bool _forInit = false)
    {
        reference.Child("RankingInfo").OrderByChild("touchcount").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
                Debug.Log("DB data load error");
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                userCount = (int)snapshot.ChildrenCount;
                int _rank = 0;
                foreach (DataSnapshot data in snapshot.Children.Reverse<DataSnapshot>())
                {
                    IDictionary personInfo = (IDictionary)data.Value;
                    
                    string _name = personInfo["username"].ToString();
                    long _count = long.Parse(personInfo["touchcount"].ToString());
                    _rank++;

                    if (_name == clientUserName)
                    {
                        GameManager.instance.curRank = _rank;
                        if (_forInit)
                        {
                            GameManager.instance.touchCount = _count;
                            GameManager.instance.UpdateTouchCount();
                            break;
                        }
                    }
                    if (!_forInit)
                    {
                        playerData temp = new playerData();
                        temp.name = _name;
                        temp.rank = _rank;
                        temp.count = _count;
                        UIManager.instance.playerList.Add(temp);
                    }
                }
            }
        });
    }
}
