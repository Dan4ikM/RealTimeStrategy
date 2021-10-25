using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject landingPage = null;

    public void HostLobby()
    {
        landingPage.SetActive(false);

        NetworkManager.singleton.StartHost();
    }
}
