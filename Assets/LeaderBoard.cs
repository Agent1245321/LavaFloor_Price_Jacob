using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;
using UnityEngine.Rendering;

public class LeaderBoard : MonoBehaviour
{

    

    
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
}
