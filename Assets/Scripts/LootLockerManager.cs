using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;
using TMPro;

public class LootLockerManager : MonoBehaviour
{
    public LeaderBoard leaderboard;
    public TMP_InputField inputField;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SetupRoutine());
    }

    string[] keys = {
     "Level1Time",
     "Level2Time",
     "Level3Time",
     "Level4Time",
     "Level5Time",
     "Level6Time",
     "Level7Time",
     "Level8Time",
     "Level9Time",
     "Level10Time",
     "Level11Time",
     "Level12Time"
    };

    public void SetPlayerName()
    {
        LootLockerSDKManager.SetPlayerName(inputField.text, (response) =>
        {
            if (response.success) {
                //Debug.Log("Succesfully Set Player Name");
                }
            else
            {
              //  Debug.Log("Failed to change player name" + response.errorData);
            }
        });
    }
    IEnumerator SetupRoutine() 
    {
        yield return LoginRoutine();

       // Debug.Log("Beggining Fetch");
        for (int i = 0; i < 12; i++) 
        {
           // Debug.Log($"Fetching {i}");
            yield return leaderboard.FetchTopHighscoresRoutine(keys[i], i); 
        }
        
    }

    IEnumerator LoginRoutine()
    {
        bool done = false;
        LootLockerSDKManager.StartGuestSession((response) =>
        {
            if(response.success) 
            {
              //  Debug.Log("Player was logged in");
                PlayerPrefs.SetString("PlayerID", response.player_id.ToString());
                done = true;
            }
            else
            {
              //  Debug.Log("Could not start session");
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
