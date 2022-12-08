using UnityEngine;

public class PlayerMagneticTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Magnetic magnetic = other.gameObject.GetComponent<Magnetic>();
        if(magnetic != null)
        {
            magnetic.SetActive();
        }
    }
}
