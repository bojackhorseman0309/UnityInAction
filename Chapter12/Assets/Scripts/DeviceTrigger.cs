using UnityEngine;

public class DeviceTrigger : MonoBehaviour
{
    [SerializeField] private GameObject[] targets;

    public bool requireKey;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(Managers.Inventory.equippedItem);
        if (requireKey && Managers.Inventory.equippedItem != "key")
        {
            return;
        }
        
        foreach (GameObject target in targets) {
            target.SendMessage("Activate");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        foreach (GameObject target in targets)
        {
            target.SendMessage("Deactivate");
        }
    }
}