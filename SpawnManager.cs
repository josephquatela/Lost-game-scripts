using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] obstacles;
    private PlayerControl playerControl;
    // Start is called before the first frame update
    void Start()
    {
      playerControl = GameObject.Find("Player").GetComponent<PlayerControl>();
      InvokeRepeating("spawnMethod", 2.0f, 2.0f);  
    }

    private void spawnMethod() {
      if (playerControl.gameOver == false) {
        int obstacle = Random.Range(0, obstacles.Length);
        Instantiate(obstacles[obstacle]);
      }
    }
}
