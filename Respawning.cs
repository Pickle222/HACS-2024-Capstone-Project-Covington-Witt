using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointsAndRespawning : MonoBehaviour
{
    //Referances Developer Input
    [SerializeField] GameObject Player;
    [SerializeField] Vector3 Respawn;
    [SerializeField] float fall;
    [SerializeField] AudioClip CheckpointNoise;

    void Update()
    {
        if (Player.transform.position.y < fall)
        {
            Player.transform.position = Respawn;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Checkpoint")
        {
            
            Respawn = other.transform.position;
            Destroy(other.gameObject);
            AudioSource.PlayClipAtPoint(CheckpointNoise, transform.position);
        }
        else if (other.gameObject.tag == "trap")
        {
            Player.transform.position = Respawn;
        }
    }
}