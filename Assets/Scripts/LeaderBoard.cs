using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;
using UnityEngine.Rendering;
using System;

public class LeaderBoard : MonoBehaviour
{

    public string[] leaderboardsNames;
    public string[] leaderboardsScores;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public IEnumerator SubmitScoreRoutine(int scoreToUpload, string Key)
    {
        bool done = false;
        string playerID = PlayerPrefs.GetString("PlayerID");
        LootLockerSDKManager.SubmitScore(playerID, scoreToUpload, Key, (response) =>
        {
            if(response.success) 
            {
                Debug.Log("Successfully uploaded score to:" + Key);
                done = true;
            }
            else
            {
                Debug.Log("Failed" + response.errorData);
                done = true;
            }
        });

        yield return new WaitWhile(() => done == false);
    }

    public IEnumerator FetchTopHighscoresRoutine(string key, int item)
    {
        Debug.Log("Fetching High Scores");
        bool done = false;
        Debug.Log($"Key {key}");
        LootLockerSDKManager.GetScoreList(key, 5, (response) =>
        {
            if(response.success)
            {
                Debug.Log("Succeeded to get scorelist");
                string tempPlayerNames = "Names\n";
                string tempPlayerScores = "Scores\n";

                LootLockerLeaderboardMember[] members = response.items;

                for(int i = 0; i < members.Length; i++)
                {
                    tempPlayerNames += members[i].rank + ". ";
                    if (members[i].player.name != "")
                    {
                        tempPlayerNames += members[i].player.name;
                    }
                    else
                    {
                        tempPlayerNames += members[i].player.id;
                    }

                    tempPlayerScores += Convert.ToDouble(members[i].score)/1000 + "\n";
                    tempPlayerNames += "\n";
                }
                done = true;

                Debug.Log(item);
                leaderboardsNames[item] = tempPlayerNames;
                leaderboardsScores[item] = tempPlayerScores;

                Debug.Log(leaderboardsNames[item]);
                Debug.Log(leaderboardsScores[item]);
            }
            else
            {
                Debug.Log("failed" + response.errorData);
            }
        });

        yield return new WaitWhile(() => done == false) ;
    }
}
