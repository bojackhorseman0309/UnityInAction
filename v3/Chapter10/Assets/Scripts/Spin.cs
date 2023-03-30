using UnityEngine;

public class Spin : MonoBehaviour
{
    public float speed = 3.0f;

    // Update is called once per frame
    private void Update()
    {
        transform.Rotate(0, speed, 0);
    }
}