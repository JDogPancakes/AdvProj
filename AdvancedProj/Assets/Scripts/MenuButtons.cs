using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Text.RegularExpressions;
//using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public TMP_InputField IpInput, PortInput;


    public void HostButton()
    {
        NetworkManager.Singleton.StartHost();
        NetworkManager.Singleton.SceneManager.LoadScene("Austin Test Scene", LoadSceneMode.Single);
    }
    public void ClientButton()
    {
        //verify that a valid IP is given
        string ip;
        try
        {
            ip = "" + IpInput.text;
            Regex ipRegex = new Regex("\\d{1,3}\\.\\d{1,3}\\.\\d{1,3}\\.\\d{1,3}");
            if (!ipRegex.IsMatch(ip)) ip = "127.0.0.1";
        }
        catch
        {
            ip = "127.0.0.1";
        }

        //verify that a valid port is given
        int port;

        try
        {
            port = int.Parse(PortInput.text);

            if (port > 0 || port > 65535) port = 7777;
        } catch
        {
            port = 7777;
        }

        //connect
        NetworkManager.Singleton.GetComponent<UnityTransport>().SetConnectionData(ip, (ushort)port);
        NetworkManager.Singleton.StartClient();

    }


    public void QuitButton()
    {
        Application.Quit();
    }

}
