using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;
using Firebase;
using Firebase.Database;
using Firebase.Extensions; // for ContinueWithOnMainThread

public class MyDatabase : MonoBehaviour
{

    //initialise ui, assigning ui in inspector
    [SerializeField]
    private TMP_InputField inputData;

    [SerializeField]
    private TMP_InputField inputPlayer;

    //[SerializeField]
    //private TMP_InputField inputEmail;

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

    //declare database
    DatabaseReference mDatabaseRef;

    //button fuctions that we wil handle
    // these are called when our listeners are triggers
    //note: make own function name, no relation to "creating data"
    public void CreateData(){
        //retrieve the input text
        string newData = inputData.text; //this retrieves my inputfield text
        string newplayerData = inputPlayer.text; //this retrieves my inputfield text
        //string playerEmail = inputEmail.text; //this retrieves my inputfield text


        //lets write to the database
        // TODO: call the database write so that it shows up into firebase.. use Set

        Favourite favourite = new Favourite (newData);
        string firstfav = JsonUtility.ToJson(favourite);
        mDatabaseRef.Child("fav1").SetRawJsonValueAsync(firstfav);

        Player player = new Player (newplayerData);

        string playerusername = JsonUtility.ToJson(player);
        mDatabaseRef.Child("players").SetRawJsonValueAsync(playerusername);

        //string playeremail = JsonUtility.ToJson(player);
        //mDatabaseRef.Child("players").SetRawJsonValueAsync(playeremail);

        // mDatabaseRef.Child("email").SetRawJsonValueAsync(email);
    }

    public class Favourite {
        public string animalname;
        public Favourite() {}

        public Favourite(string animalname) {
            this.animalname = animalname;
        }
    }

    public class Player {
        public string playername;
        public Player() {}

        public Player(string playername) {
            this.playername = playername;
        }
    }

    public void ReadData() {
        FirebaseDatabase.DefaultInstance.GetReference("users").GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted) {
                //Handle the error
                Debug.Log("task faulted");
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                //Do something with the snapshot
                foreach(DataSnapshot ds in snapshot.Children)
                {
                    Text txt = JsonUtility.FromJson<Text>(ds.GetRawJsonValue());
                    Debug.LogFormat("Text: {0}", txt);
                }
            }
        });
    }
    public void UpdateData() { // not used
        string updatedFav = inputUpdate.text;
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference("fav1/");
        //handle the error
        string id = "animalname";
        Dictionary<string, object> childUpdates = new Dictionary<string, object>();
        string newUpdate = inputUpdate.text;
        childUpdates[id] = newUpdate;

        reference.UpdateChildrenAsync(childUpdates);
    }
    public void DeleteData() { // not used
        mDatabaseRef.Child("users/U001/password").RemoveValueAsync();
        Debug.Log("need help with this");
    }

    // Start is called before the first frame update
    void Start()
    {
        //initialize our buttons with listeners 
        //methods are called based on the listener event (OnClick)
        //although unity inspector allows us to add in onClick. This gives us finer control, easier to manage

        btnCreate.onClick.AddListener(CreateData); //this calls my CreateData() function when user clicks it
        btnRead.onClick.AddListener(ReadData);
        btnUpdate.onClick.AddListener(UpdateData);
        
        //get the root reference location of the database
        mDatabaseRef = FirebaseDatabase.DefaultInstance.RootReference;
        WriteNewUser("U001","apple","iloveapple35");

    }

    private void WriteNewUser(string userId, string name, string password) {
        User user = new User (name, password);
        string json = JsonUtility.ToJson(user);

        mDatabaseRef.Child("users").Child(userId).SetRawJsonValueAsync(json);
    }

    public class User {
        public string username;
        public string password;
        public User() {

        }

        public User(string username, string password) {
            this.username = username;
            this.password = password;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   
}
