using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    public void Hurt(int damage)
    {
        Managers.Player.ChangeHealth(-damage);
    }
}
