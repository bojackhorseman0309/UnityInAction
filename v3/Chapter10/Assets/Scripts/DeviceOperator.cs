using UnityEngine;

public class DeviceOperator : MonoBehaviour
{
    public float radius = 1.5f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            var hitColliders = Physics.OverlapSphere(transform.position, radius);

            foreach (var hitCollider in hitColliders)
            {
                var hitPosition = hitCollider.transform.position;
                hitPosition.y = transform.position.y;

                var direction = hitPosition - transform.position;
                if (Vector3.Dot(transform.forward, direction.normalized) > .5f)
                    hitCollider.SendMessage("Operate", SendMessageOptions.DontRequireReceiver);
            }
        }
    }
}