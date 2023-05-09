using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class LeadBoardObject
{
    public int score;
    public string name;
}
public class LeadBoard
{
    public List<LeadBoardObject> leadBoardEntryList;
}

public class LeadBoardController : MonoBehaviour
{
    [SerializeField] public Transform entryContainer;
    [SerializeField] public Transform entryTemplate;
    private List<LeadBoardObject> leadBoardEntryList;
    private List<Transform> leadBoardTransformList;


    private void Awake()
    {
        gameObject.SetActive(false);

        leadBoardTransformList = new List<Transform>();

        leadBoardEntryList = new List<LeadBoardObject>()
        {
            new LeadBoardObject{ score = 2, name = "user3"},
            new LeadBoardObject{ score = 4, name = "barak"},
            new LeadBoardObject{ score = 6, name = "dor"},
            new LeadBoardObject{ score = 8, name = "nadav"},
            new LeadBoardObject{ score = 10, name = "ofek"},
            new LeadBoardObject{ score = 12, name = "ella"},
            new LeadBoardObject{ score = 14, name = "omri"},
            new LeadBoardObject{ score = 10, name = "aliya"},
            new LeadBoardObject{ score = 8, name = "user1"},
            new LeadBoardObject{ score = 12, name = "user2"},
        };

        LeadBoard leadBoard = new LeadBoard { leadBoardEntryList = leadBoardEntryList };
        saveLeadBoard(leadBoard);

        LeadBoard leadboard1 = loadLeadBoard();



        //string jsonString = PlayerPrefs.GetString("LeadBoardTable");
        //LeadBoard leadBoard = JsonUtility.FromJson<LeadBoard>(jsonString);
        //if (leadBoard == null) { 
        //    leadBoard = new LeadBoard();
        //}

        ////AddScoreEntry(888, "Aviv");
        //gameObject.SetActive(false);
        //entryTemplate.gameObject.SetActive(false);



        //sort the list 
        for (int i = 0; i < leadBoard.leadBoardEntryList.Count; i++) { 
            for(int j = i+1; j < leadBoard.leadBoardEntryList.Count; j++) {
                if (leadBoard.leadBoardEntryList[j].score > leadBoard.leadBoardEntryList[i].score) {
                    //swap entries
                    LeadBoardObject tmp = leadBoard.leadBoardEntryList[i];
                    leadBoard.leadBoardEntryList[i] = leadBoard.leadBoardEntryList[j];
                    leadBoard.leadBoardEntryList[j] = tmp;
                }
            }        
        }

        foreach (LeadBoardObject leadBoardEntry in leadBoardEntryList) {
            createLeadBoardEntryTransform(leadBoardEntry, entryContainer, leadBoardTransformList);
        }

        //LeadBoard leadBoard = new LeadBoard() { leadBoardEntryList = leadBoardEntryList };  
        //string json = JsonUtility.ToJson(leadBoard);
        //PlayerPrefs.SetString("LeadBoardTable", json);
        //PlayerPrefs.Save();
        
    }

    private void createLeadBoardEntryTransform(LeadBoardObject leadBoardEntry, Transform container, List<Transform> transformList) {
        float templateHeight = 20f;
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count + 1;
        string rankString;
        switch (rank)
        {
            default: rankString = rank + "TH"; break;
            case 1: rankString = "1ST"; break;
            case 2: rankString = "2ND"; break;
            case 3: rankString = "3ND"; break;
        }

        entryTransform.Find("PosText").GetComponent<TextMeshProUGUI>().text = rankString;
        entryTransform.Find("ScoreText").GetComponent<TextMeshProUGUI>().text = leadBoardEntry.score.ToString();
        entryTransform.Find("NameText").GetComponent<TextMeshProUGUI>().text = leadBoardEntry.name;

        transformList.Add(entryTransform);
    }

    private void AddScoreEntry(int score, string name)
    {
        //create entry
        LeadBoardObject leadBoardEntry = new LeadBoardObject { score = score, name = name };

        //load saved leadBoard scores
        LeadBoard leadBoard = loadLeadBoard();

        //add new entry
        leadBoard.leadBoardEntryList.Add(leadBoardEntry);

        //save updated leadBoard
        saveLeadBoard(leadBoard);
    }

    public LeadBoard loadLeadBoard() {
        string jsonString = PlayerPrefs.GetString("LeadBoardTable");
        LeadBoard leadBoard = JsonUtility.FromJson<LeadBoard>(jsonString);
        return leadBoard;
    }
    public void saveLeadBoard(LeadBoard leadBoard)
    {
        string json = JsonUtility.ToJson(leadBoard);
        PlayerPrefs.SetString("LeadBoardTable", json);
        PlayerPrefs.Save();
    }
    public void show()
    {
        gameObject.SetActive(true);
    }

    public void hide() { 
        gameObject.SetActive(false) ;
    }
}
