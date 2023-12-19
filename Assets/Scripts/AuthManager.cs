using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;

using Firebase;
using Firebase.Database;
using Firebase.Auth;

public class AuthManager : MonoBehaviour
{
    Firebase.Auth.FirebaseAuth auth;

    [SerializeField]
    private TMP_InputField inputEmail;

    [SerializeField]
    private TMP_InputField inputPassword;
    
    [SerializeField]
    private Button btnSignUp; 

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
        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            //perform task handling
            if(task.IsFaulted || task.IsCanceled)
            {
                Debug.LogError("Sorry, there was an error creating your new account, ERROR: " + task.Exception);
                return;//exit from the attempt
            }
            else if (task.IsCompleted)
            {
            Firebase.Auth.AuthResult newPlayer = task.Result;
            Debug.LogFormat("Welcome to Piak! Piak! {0}", newPlayer.User.Email);
            //do anything you want after player creation eg. create new player
            }
        });

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    private void Awake()
    {
        auth = FirebaseAuth.DefaultInstance;

    }

    
}
