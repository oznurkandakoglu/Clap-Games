using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        ScoreText.coinAmount += 1;
        Destroy(gameObject);
    }
}
