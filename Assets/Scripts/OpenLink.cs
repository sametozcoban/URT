using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class OpenLink : MonoBehaviour
{
    string linkedinUrl = "linkedin.com/in/sametozcoban";
    string githubUrl = "https://github.com/sametozcoban?tab=repositories";

    public void OpenGithub()
    {
        Debug.Log("Tıkladın");
        Process.Start("chrome.exe", githubUrl);
    }
    public void OpenLinkedin()
    {
        Debug.Log("Tıkladın");
        Process.Start("chrome.exe", linkedinUrl);
    }
}
