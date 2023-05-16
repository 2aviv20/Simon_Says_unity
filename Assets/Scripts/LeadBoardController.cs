using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;
using Unity.VisualScripting;

[Serializable]
public class LeadBoardObject
{
    public int score;
    public string name;
}

[Serializable]
public class LeadBoard
{
    public List<LeadBoardObject> leadBoardEntryList;
}

public class LeadBoardController : MonoBehaviour
{
    [SerializeField] public Transform gameContoller;
    [SerializeField] public Transform entryContainer;
    [SerializeField] public Transform entryTemplate;
    private List<Transform> leadBoardTransformList;
    public LeadBoard leadBoard;

    private void Awake()
    {
        gameObject.SetActive(false);
        entryTemplate.gameObject.SetActive(false);
        leadBoardTransformList = new List<Transform>();
        drawLeadBoard();
    }

    public void drawLeadBoard() {
        loadLeadBoard();
        deleteListFromSreen();
        //sort the list 
        sortList();
        //remove items from list , max 10 items
        for (int i = this.leadBoard.leadBoardEntryList.Count - 1; i >= 10; i--)
        {
            this.leadBoard.leadBoardEntryList.RemoveAt(i);
        }

        foreach (LeadBoardObject leadBoardEntry in this.leadBoard.leadBoardEntryList)
        {
            createLeadBoardEntryTransform(leadBoardEntry, entryContainer, leadBoardTransformList);
        }

    }

    void deleteListFromSreen() {
        for (int i = leadBoardTransformList.Count - 1; i >= 0; i--)
        {
            Destroy(leadBoardTransformList[i].gameObject);
            leadBoardTransformList.RemoveAt(i);
        }
    }

    public void sortList() {
        for (int i = 0; i < this.leadBoard.leadBoardEntryList.Count; i++)
        {
            for (int j = i + 1; j < this.leadBoard.leadBoardEntryList.Count; j++)
            {
                if (this.leadBoard.leadBoardEntryList[j].score > leadBoard.leadBoardEntryList[i].score)
                {
                    //swap entries
                    LeadBoardObject tmp = this.leadBoard.leadBoardEntryList[i];
                    this.leadBoard.leadBoardEntryList[i] = this.leadBoard.leadBoardEntryList[j];
                    this.leadBoard.leadBoardEntryList[j] = tmp;
                }
            }
        }
    }

    private void createLeadBoardEntryTransform(LeadBoardObject leadBoardEntry, Transform container, List<Transform> transformList) {
        float templateHeight = 40f;
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

        if (leadBoardEntry.name == gameContoller.GetComponent<GameController>().playerName)
        {
            entryTransform.Find("PosText").GetComponent<TextMeshProUGUI>().color = Color.red;
            entryTransform.Find("ScoreText").GetComponent<TextMeshProUGUI>().color = Color.red;
            entryTransform.Find("NameText").GetComponent<TextMeshProUGUI>().color = Color.red;
        }
        transformList.Add(entryTransform);
    }

    public void AddScoreEntry(int score, string name)
    {
        //create entry
        LeadBoardObject leadBoardEntry = new LeadBoardObject { score = score, name = name };

        //load saved leadBoard scores
        loadLeadBoard();

        bool duplicatedNameFound = false;
        //add new entry

        //perevent duplicated names in leadBoard
        foreach (LeadBoardObject item in this.leadBoard.leadBoardEntryList) {
            if (item.name == name) { 
                item.score = score;
                duplicatedNameFound = true;
            }
        }
        if (!duplicatedNameFound) {
            this.leadBoard.leadBoardEntryList.Add(leadBoardEntry);
        }

        //save updated leadBoard
        saveLeadBoard();
    }

    public void loadLeadBoard() {
        string jsonString = PlayerPrefs.GetString("LeadBoardTable");
        
        LeadBoard loadedData = JsonUtility.FromJson<LeadBoard>(jsonString);
        if (this.leadBoard.leadBoardEntryList == null) {
            this.leadBoard.leadBoardEntryList= new List<LeadBoardObject>();
        }
        if (loadedData != null) {
            this.leadBoard = loadedData;
        }
    }
    public void saveLeadBoard()
    {
        string json = JsonUtility.ToJson(this.leadBoard);
        PlayerPrefs.SetString("LeadBoardTable", json);
        PlayerPrefs.Save();
    }

    public void resetLeadBoard()
    {
        PlayerPrefs.SetString("LeadBoardTable", "{}");
        PlayerPrefs.Save();
    }

    public void show()
    {
        drawLeadBoard();
        gameObject.SetActive(true);
    }

    public void hide() { 
        gameObject.SetActive(false) ;
    }
}
