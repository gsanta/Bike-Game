using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerSpawner : MonoBehaviour
{

    public static PlayerSpawner instance;
    public UIController canvasController;
    public GameObject homeControllerObject;

    private List<GameObject> players = new List<GameObject>();

    private void Awake()
    {
        instance = this;
    }

    public GameObject playerPrefab;
    private GameObject player;
    public GameObject deathEffect;
    public float respawnTime = 5f;

    void Start()
    {
        if (PhotonNetwork.IsConnected)
        {
            SpawnPlayer();
        }
    }

    public void SpawnPlayer()
    {
        Transform spawnPoint = SpawnManager.instance.GetSpawnPoint();

        player = PhotonNetwork.Instantiate(playerPrefab.name, spawnPoint.position, spawnPoint.rotation);
        PlayerController playerController = player.GetComponent<PlayerController>();
        players.Add(player);

        playerController.canvasController = canvasController;
        playerController.homeControllerObject = homeControllerObject;
    }

    public void Die(string damager)
    {
        PhotonNetwork.Instantiate(deathEffect.name, player.transform.position, Quaternion.identity);

        UIController.instance.deathText.text = "You were killed by " + damager;

        if (player != null)
        {
            StartCoroutine(DieCo());
        }
    }

    public IEnumerator DieCo()
    {
        PhotonNetwork.Instantiate(deathEffect.name, player.transform.position, Quaternion.identity);
        PhotonNetwork.Destroy(player);
        UIController.instance.deathScreen.SetActive(true);

        yield return new WaitForSeconds(respawnTime);

        UIController.instance.deathScreen.SetActive(false);
        SpawnPlayer();
    }

    public List<GameObject> GetPlayers()
    {
        return players;
    }
}
