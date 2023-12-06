using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;

public class MyDatabase : MonoBehaviour
{

    //// to initialize UI
    [SerializeField]
    private TMP_InputField inputData;

    [SerializeField]
    private Button btnCreate;

    //[SerializeField]
    //private Button btnRead;

    //[SerializeField]
    //private Button btnUpdate;

    //[SerializeField]
    //private Button btnDelete;

    //[SerializeField]
    //private TMP_InputField inputUpdate;

    DatabaseReference mDatabaseRef;

    // button functions that we will handle
    // these are called when our listeners are triggers 

    public void CreateData()
    {
        string newData = inputData.text; //retrieve inputField text

        //let's write to database
        //@TODO call database to write.. use SET
        Name name = new Name(newData);
        string gdname = JsonUtility.ToJson(name);
        mDatabaseRef.Child("name1").Child(newData).SetRawJsonValueAsync(gdname);
    }



    public void ReadData()
    {
        FirebaseDatabase.DefaultInstance.GetReference("name1").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.Log("ERROR!");

            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                //do something with snapshot
                Debug.Log("at firebase");
            }
        });
    }


    //public void UpdateData()
    //{
    //    string updateName = inputUpdate.text;

    //    DatebaseReference reference = FirebaseDatabase.DefaultInstance.GetReference("users/" );
    //    // handle the error
    //    string id = "newName";
    //    Dictionary<string, object> childUpdates = new Dictionary<string, object>();
    //    string newUpdate = inputUpdate.text;
    //    childUpdates[id] = newUpdate;

    //    reference.UpdateChildrenAsync(childUpdates);
    //}

    public void DeleteData()
    {
        mDatabaseRef.Child("name1").RemoveValueAsync();
        Debug.Log("Delete");
    }

    // Start is called before the first frame update
    void Start()
    {
        btnCreate.onClick.AddListener(CreateData);
        //btnRead.onClick.AddListener(ReadData);
        ////  btnUpdate.onClick.AddListener(UpdateData);
        //btnDelete.onClick.AddListener(DeleteData);

        mDatabaseRef = FirebaseDatabase.DefaultInstance.RootReference;
    }


}
