using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enableOnTriggerEnter : MonoBehaviour {
    public LayerMask layer;
    public MonoBehaviour[] gameObjectsToEnable;
    public GameObject[] _gameObjectsToEnable;
    // Use this for initialization

    void Start()
    {
        for (int i = 0; i < gameObjectsToEnable.Length; i++)
        {
            gameObjectsToEnable[i].enabled = (false);
        }
        for (int i = 0; i < _gameObjectsToEnable.Length; i++)
        {
            _gameObjectsToEnable[i].SetActive(false);
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(layer.Contains(collision.gameObject.layer))
        {
            for (int i = 0; i < gameObjectsToEnable.Length; i++)
            {
                gameObjectsToEnable[i].enabled=(true);
            }
            for (int i = 0; i < _gameObjectsToEnable.Length; i++)
            {
                _gameObjectsToEnable[i].SetActive(true);
            }
        }
    }
}
