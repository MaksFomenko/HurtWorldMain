using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
   
    public GameObject butten;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("YourColliderTag"))
        {
            butten.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("YourColliderTag"))
        {
            butten.SetActive(false);
        }
    }
}
