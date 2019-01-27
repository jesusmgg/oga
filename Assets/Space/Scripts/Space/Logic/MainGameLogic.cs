using System.Collections.Generic;
using Space.Input;
using SwordGC.AirController;
using UnityEngine;
using UnityEngine.UI;

namespace Space.Logic
{
    public class MainGameLogic : MonoBehaviour
    {
        public Canvas titleCanvas;
        
        void OnEnable()
        {
            SwordGC.AirController.AirController.Instance.onPlayerClaimed += OnPlayerClaimed;
            SwordGC.AirController.AirController.Instance.onPlayerUnclaimed += OnPlayerUnclaimed;
        }

        void OnDisable()
        {
            SwordGC.AirController.AirController.Instance.onPlayerClaimed -= OnPlayerClaimed;
            SwordGC.AirController.AirController.Instance.onPlayerUnclaimed -= OnPlayerUnclaimed;
        }

        void Update()
        {
            SwordGC.AirController.AirController.Instance.UpdateDeviceStates();
        }

        void OnPlayerClaimed(Player player)
        {
            if (SwordGC.AirController.AirController.Instance.PlayersAvailable < 4)
            {
                titleCanvas.gameObject.SetActive(false);
            }

            SwordGC.AirController.AirController.Instance.UpdateDeviceStates();
        }

        void OnPlayerUnclaimed(Player player)
        {
            if (SwordGC.AirController.AirController.Instance.PlayersAvailable != 0)
            {
                //StopGame();
            }
            
            SwordGC.AirController.AirController.Instance.UpdateDeviceStates();
        }
    }
}