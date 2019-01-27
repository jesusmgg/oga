using UnityEngine;

namespace Space.Logic
{
    public class PlayerLogic : MonoBehaviour
    {
        [Header("Stats")]
        public int shields;
        public int iron;
        public int fluoride;
        public int gas;
        public int storage;  // Limit is set to 100

        [Header("Turret")]
        public Transform buildPoint;
        public GameObject turretPrefab;
        public int turretIronCost;
        public int turretGasCost;
        public int turretFluorideCost;

        [Header("Parameters")]
        public ParticleSystem absorbParticles;
        public bool absorb = false;

        void Start()
        {
            iron = 0;
            fluoride = 0;
            gas = 0;
            storage = 100;
            shields = 100;

            absorb = false;
        }

        void Update()
        {
            storage = 100 - iron - fluoride - gas;
            
            if (GetTotalResources() > 0 && absorb)
            {
                absorbParticles.gameObject.SetActive(true);
            }
            else
            {
                absorbParticles.gameObject.SetActive(false);
            }
        }

        public int GetTotalResources()
        {
            return iron + fluoride + gas;
        }

        public int AbsorbIron(int amount)
        {
            if (iron < amount)
            {
                int absorbed = iron;
                iron = 0;
                return absorbed;
            }

            else
            {
                iron -= amount;
                return amount;
            }
        }
        
        public int AbsorbGas(int amount)
        {
            if (gas < amount)
            {
                int absorbed = gas;
                gas = 0;
                return absorbed;
            }

            else
            {
                gas -= amount;
                return amount;
            }
        }
        
        public int AbsorbFluoride(int amount)
        {
            if (fluoride < amount)
            {
                int absorbed = fluoride;
                fluoride = 0;
                return absorbed;
            }

            else
            {
                fluoride -= amount;
                return amount;
            }
        }

        public void BuildTurret()
        {
            if (turretGasCost <= gas && turretIronCost <= iron && turretFluorideCost <= fluoride)
            {
                gas -= turretGasCost;
                iron -= turretIronCost;
                fluoride -= turretFluorideCost;

                Instantiate(turretPrefab, buildPoint.position, Quaternion.identity, transform);
            }
        }
    }
}