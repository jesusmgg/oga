using System.Collections.Generic;
using UnityEngine;

namespace Space.Logic
{
    public class Planet : MonoBehaviour
    {
        [Header("Stats")]
        public int iron;

        public int fluoride;
        public int gas;

        [Header("Parameters")]
        public List<GameObject> layers;

        public int absorbSpeed;
        public int absorbReloadFrames;
        public int resourcesRequired;

        public bool isBuilt;

        public AudioClip builtSound;
        public AudioClip absorbSound;

        AudioPlayer audioPlayer;

        int currentReload;

        void Start()
        {
            currentReload = absorbReloadFrames;
            isBuilt = false;

            audioPlayer = FindObjectOfType<AudioPlayer>();
        }

        void Update()
        {
            if (!isBuilt)
            {
                if (GetTotalResources() > resourcesRequired / 4)
                {
                    layers[0].SetActive(true);
                }

                if (GetTotalResources() > resourcesRequired / 2)
                {
                    layers[1].SetActive(true);
                }

                if (GetTotalResources() > resourcesRequired * .75f)
                {
                    layers[2].SetActive(true);
                }

                if (GetTotalResources() >= resourcesRequired)
                {
                    layers[3].SetActive(true);
                    isBuilt = true;
                    audioPlayer.PlaySound(builtSound);
                }
            }

            if (currentReload > 0)
            {
                currentReload--;
            }
        }

        int GetTotalResources()
        {
            return iron + fluoride + gas;
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player") && !isBuilt)
            {
                other.GetComponent<ShipPlayerComponents>().playerLogic.absorb = true;
                audioPlayer.PlaySound(absorbSound);
            }
        }

        void OnTriggerStay2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (currentReload <= 0)
                {
                    if (iron < 100)
                    {
                        iron += other.GetComponent<ShipPlayerComponents>().playerLogic.AbsorbIron(1);
                    }

                    else if (gas < 100)
                    {
                        gas += other.GetComponent<ShipPlayerComponents>().playerLogic.AbsorbGas(absorbSpeed);
                    }

                    else if (fluoride < 100)
                    {
                        fluoride += other.GetComponent<ShipPlayerComponents>().playerLogic.AbsorbFluoride(absorbSpeed);
                    }

                    else
                    {
                        other.GetComponent<ShipPlayerComponents>().playerLogic.absorb = false;
                    }

                    currentReload = absorbReloadFrames;
                }
            }

            if (isBuilt)
            {
                other.GetComponent<ShipPlayerComponents>().playerLogic.absorb = false;
            }
        }

        void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                other.GetComponent<ShipPlayerComponents>().playerLogic.absorb = false;
            }
        }

        public void LoseResources(int damage)
        {
            int remainingDamage = damage;

            int ironDamage = Random.Range(0, remainingDamage);
            remainingDamage -= ironDamage;
            remainingDamage = Mathf.Clamp(remainingDamage, 0, damage);
            
            int gasDamage = Random.Range(0, remainingDamage);
            remainingDamage -= gasDamage;
            remainingDamage = Mathf.Clamp(remainingDamage, 0, damage);
            
            int fluorideDamage = Random.Range(0, remainingDamage);
            remainingDamage -= fluorideDamage;
            remainingDamage = Mathf.Clamp(remainingDamage, 0, damage);

            iron -= ironDamage;
            gas -= gasDamage;
            fluoride -= fluorideDamage;

            iron = Mathf.Clamp(iron, 0, resourcesRequired);
            gas = Mathf.Clamp(gas, 0, resourcesRequired);
            fluoride = Mathf.Clamp(fluoride, 0, resourcesRequired);
        }
    }
}