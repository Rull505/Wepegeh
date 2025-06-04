using UnityEngine;
using System.Collections.Generic;
using System.Collections;
public class CoinScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(20 * Time.deltaTime, 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
           PlayerManager.numberOfCoins += 1;
           Debug.Log("Coins:" + PlayerManager.numberOfCoins);
           Destroy(gameObject);
        }
    }    
}
