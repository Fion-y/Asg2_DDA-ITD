using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;

using Firebase;
using Firebase.Database;
using Firebase.Auth;
using Firebase.Extensions;

public class AuthManager : MonoBehaviour
{
    Firebase.Auth.FirebaseAuth auth;

    [SerializeField] private TMP_InputField inputEmail;

    [SerializeField] private TMP_InputField inputPassword;
    
    [SerializeField] private Button btnSignUp;
    [SerializeField] private Button btnProceed;

    [SerializeField] private TMP_Text errorMsgField;
    [SerializeField] private string textToDisplay;
    [SerializeField] private string textToDisplay2;

    public void ChangeErrorMsg()
    {
        //errorMsgField.SetActive(true);
        errorMsgField.text = textToDisplay;
       
        Debug.Log("error msg shown");
    }

    public void SignUp()
    {
        Debug.Log("Button is being clicked...");
        string email = inputEmail.text;
        string password = inputPassword.text;

        //validate email and password 

        SignUpUser(email, password);
    }

    
    private void SignUpUser(string email, string password)
    {
        //automatically pass user info to the firebase project
        //attempt to create new user or check with there's already one
        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
        {
            //perform task handling
            if(task.IsFaulted || task.IsCanceled)
            {
                ChangeErrorMsg();
                Debug.LogError("Sorry, there was an error creating your new account, ERROR: " + task.Exception);
                // error msg
                return;//exit from the attempt
            }
            else if (task.IsCompleted)
            {
            Firebase.Auth.AuthResult newPlayer = task.Result;
            Debug.LogFormat("Welcome to Piak! Piak! {0}", newPlayer.User.Email);
                errorMsgField.text = textToDisplay2;
                btnProceed.gameObject.SetActive(true);
                //do anything you want after player creation eg. create new player
            }
        });

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        //errorMsg.text = "Error";
    }
    private void Awake()
    {
        auth = FirebaseAuth.DefaultInstance;

    }

    
}
