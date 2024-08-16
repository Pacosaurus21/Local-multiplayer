using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallManager : MonoBehaviour
{
    public PlayerControl _playerControl;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet" + _playerControl.enemyInputCode)
            Destroy(other.gameObject);
            
    }
}
