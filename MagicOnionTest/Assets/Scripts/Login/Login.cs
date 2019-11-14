using System.Collections;
using UnityEngine;

public class Login : MonoBehaviour
{
    //Player Prefs
    /*private string u = "username";
    private string p = "password";*/

    private string usernameString = string.Empty;
    private string passwordString = string.Empty;

    private Rect windowRect = new Rect(0, 0, Screen.width, Screen.height);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnGUI()
    {
        GUI.Window(0, windowRect, WindowFunction, "Login");
    }

    void WindowFunction(int windowID)
    {
        //User input log-in
        usernameString =
            GUI.TextField(new Rect(Screen.width / 3, 2 * Screen.height / 5, Screen.width / 3, Screen.height / 10),
                usernameString, 10);

        passwordString =
            GUI.PasswordField(new Rect(Screen.width / 3, 2 * Screen.height / 3, Screen.width / 3, Screen.height / 10),
                passwordString, "*"[0], 10);

        if (GUI.Button(new Rect(Screen.width / 2, 4 * Screen.height / 5, Screen.width / 8, Screen.height / 8), "Login"))
        {
            if (usernameString == "test" && passwordString == "test")
            {
                Debug.Log("Welcome");
            }
            else
            {
                Debug.Log("Wrong username or password.");
            }
            /*
             * if(Password is correct)
             *      debug.log("LoggedIn, switch scene)
             * else
             *      retry
             */
        }

        GUI.Label(new Rect(Screen.width/3, 35*Screen.height/100, Screen.width/5, Screen.height/8), "Username");
        GUI.Label(new Rect(Screen.width/3, 62*Screen.height/100, Screen.width/5, Screen.height/8), "Password");
    }
}
