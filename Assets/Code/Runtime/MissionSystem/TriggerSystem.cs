using UnityEngine;
using BabuDriver.GameEvents;

namespace BabuDriver.TriggerSystem
{
    public class TriggerSystem : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Warehouse"))
            {
                GameEventManager.TriggerEvent("WarehouseEntered", this.gameObject);
            }
            else if (other.CompareTag("BuyerBlip"))
            {
                GameEventManager.TriggerEvent("DeleverProduct", this.gameObject);
            }
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("PedVehicle"))
            {
                GameEventManager.TriggerEvent("PedVehicleCollsion", this.gameObject);
            }
        }
    }
}
