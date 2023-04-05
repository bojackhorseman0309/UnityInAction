using System;
using UnityEngine;

public class BaseDevice : MonoBehaviour
{
    public float radius = 3.5f;

    private void OnMouseUp()
    {
        Transform player = GameObject.FindWithTag("Player").transform;
        Vector3 playerPosition = player.position;

        playerPosition.y = transform.position.y;

        if (Vector3.Distance(transform.position, playerPosition) < radius) 
        {
            Vector3 direction = transform.position - playerPosition;
            if (Vector3.Dot(player.forward, direction) > .5f)
            {
                Operate();
            }
        }
    }

    public virtual void Operate()
    {
        
    }
}