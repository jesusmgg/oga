using UnityEngine;

namespace Space.Logic
{
    public class ShipResourceAbsorber : MonoBehaviour
    {
        public PlayerLogic logic;

        [Header("Resources Absorb")]
        public int absorbSpeed;

        public int absorbReloadFrames;
        int currentAbsorbReload;

        public AudioClip absorbSound;
        AudioPlayer audioPlayer;

        void Start()
        {
            currentAbsorbReload = absorbReloadFrames;

            audioPlayer = FindObjectOfType<AudioPlayer>();
        }

        void Update()
        {
            if (currentAbsorbReload > 0)
            {
                currentAbsorbReload--;
            }
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Iron") || other.gameObject.CompareTag("Gas") ||
                other.gameObject.CompareTag("Fluoride"))
            {
                audioPlayer.PlaySound(absorbSound);
            }
        }

        void OnTriggerStay2D(Collider2D other)
        {
            Debug.Log(gameObject.tag);

            if (other.gameObject.CompareTag("Iron"))
            {
                if (currentAbsorbReload <= 0 && logic.GetTotalResources() < 100)
                {
                    logic.iron += absorbSpeed;
                    currentAbsorbReload = absorbReloadFrames;
                }
            }

            else if (other.gameObject.CompareTag("Gas"))
            {
                if (currentAbsorbReload <= 0 && logic.GetTotalResources() < 100)
                {
                    logic.gas += absorbSpeed;
                    currentAbsorbReload = absorbReloadFrames;
                }
            }

            else if (other.gameObject.CompareTag("Fluoride"))
            {
                if (currentAbsorbReload <= 0 && logic.GetTotalResources() < 100)
                {
                    logic.fluoride += absorbSpeed;
                    currentAbsorbReload = absorbReloadFrames;
                }
            }
        }
    }
}