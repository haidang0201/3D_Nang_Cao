using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    public Transform player;
    private void Update() {
        Vector3 newPos = player.position;
        newPos.y = tranform.position.y;
        transform.position = newPos;
    }
}
