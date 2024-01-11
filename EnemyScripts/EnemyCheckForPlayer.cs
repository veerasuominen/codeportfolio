using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyCheckForPlayer : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform checkForPlayer;
    public LayerMask playerLayerMask;
    public GameObject playerPrefab;
    public float range;
    public bool isInRange;
    public float movementSpeed = 1;

    private void Start()
    {
        isInRange = false;
    }

    // Update is called once per frame
    private void Update()
    {
        isInRange = Physics.CheckSphere(checkForPlayer.position, range, playerLayerMask);
        if (isInRange)
        {
            Debug.Log("checked");
            transform.Translate(playerPrefab.transform.position * Time.deltaTime * movementSpeed);
        }
    }
}