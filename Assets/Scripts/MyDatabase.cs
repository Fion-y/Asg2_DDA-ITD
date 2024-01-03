using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;
using Firebase;
using Firebase.Database;
using Firebase.Extensions; // for ContinueWithOnMainThread
using System;
using System.Threading;

public class MyDatabase : MonoBehaviour
{
    //initialise ui, assigning ui in inspector

    [SerializeField]
    private Button btnCreate; 

    [SerializeField]
    private Button btnRead; 

    [SerializeField]
    private Button btnUpdate; 

    [SerializeField]
    private Button btnDelete; 

    [SerializeField]
    private TMP_InputField inputUpdate;
    // we put serializefield to avoid using public. public gives us a little too much exposure

    public TMP_InputField regUsernametxt;

    DatabaseReference reference;
    void Start()
    {
        //get root reference location of the database 
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        CreatePlayer("user1130", 93, "playing", 35);
    }
    public void CreatePlayer(string username, int health, string status, float progress)
    {
        string usernameInput = regUsernametxt.text;

        Player newPlayer = new Player(usernameInput, health, status, progress);
        string json = JsonUtility.ToJson(newPlayer);
        reference.Child("players").Child(usernameInput).SetRawJsonValueAsync(json);
    }

    public void ReadPlayer(string username, Action<Player> callback)
    {
        reference.Child("players").Child(username).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted || task.IsCanceled)
            {
                Debug.LogError("Failed to read player data.");
                return;
            }

            DataSnapshot snapshot = task.Result;
            if (snapshot.Exists)
            {
                Player player = JsonUtility.FromJson<Player>(snapshot.GetRawJsonValue());
                callback?.Invoke(player);
            }
            else
            {
                Debug.LogWarning("Player not found.");
                callback?.Invoke(null);
            }
        });
    }

    public void UpdatePlayer(string username, Player updatedPlayer)
    {
        string json = JsonUtility.ToJson(updatedPlayer);
        reference.Child("players").Child(username).SetRawJsonValueAsync(json);
    }

    public void DeletePlayer(string username)
    {
        reference.Child("players").Child(username).RemoveValueAsync();
    }
}

    [Serializable]
    public class Player
    {
        public string username;
        public int health;
        public string status;
        public float progress;

        public Player(string username, int health, string status, float progress)
        {
            this.username = username;
            this.health = health;
            this.status = status;
            this.progress = progress;
        }
    }