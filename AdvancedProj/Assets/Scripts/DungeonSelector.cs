using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class DungeonSelector : NetworkBehaviour
{
    [SerializeField] private GameObject dungeonSelectionPanel;
    [SerializeField] private Button tutorialDungeonButton, dungeon1Button;
    private int numPlayers = 0;


    private void Start()
    {
        tutorialDungeonButton.onClick.AddListener(() =>
        {
            LoadDungeon("Dungeon1");
        });
        dungeon1Button.onClick.AddListener(() =>
        {
            LoadDungeon("Dungeon2");
        });
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsServer && collision.CompareTag("Player"))
        {
            numPlayers++;
            if (numPlayers == NetworkManager.ConnectedClients.Count)
                activateDungeonSelectionClientRpc();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (IsServer && collision.CompareTag("Player"))
        {
            numPlayers--;
            deactivateDungeonSelectionClientRpc();
        }
    }

    [ClientRpc]
    private void activateDungeonSelectionClientRpc()
    {
        if (IsHost)
        {
            dungeonSelectionPanel.SetActive(true);
        }
    }

    [ClientRpc]
    private void deactivateDungeonSelectionClientRpc()
    {
        if (IsHost)
        {
            dungeonSelectionPanel.SetActive(false);
        }
    }

    public void LoadDungeon(string name)
    {
        NetworkManager.SceneManager.LoadScene(name, UnityEngine.SceneManagement.LoadSceneMode.Single);
    }
}
