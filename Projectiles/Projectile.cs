using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 0.5f;

    // Update is called once per frame
    private void Update()
    {//sets speed and direction, destroys in 3 seconds if gameobject is still in scene
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        Destroy(gameObject, 3);
    }

    //gets destroyed if hits player
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}