using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;

public class LootLockerManager : MonoBehaviour
{
    public LeaderBoard leaderboard;
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
     "Level8Time"
    };
    IEnumerator SetupRoutine() 
    {
        yield return LoginRoutine();

        Debug.Log("Beggining Fetch");
        for (int i = 0; i < 8; i++) 
        {
            Debug.Log($"Fetching {i}");
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
                Debug.Log("Player was logged in");
                PlayerPrefs.SetString("PlayerID", response.player_id.ToString());
                done = true;
            }
            else
            {
                Debug.Log("Could not start session");
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
