using System.Collections;
using System.Collections.Generic;
using Space.Input;
using SwordGC.AirController;
using UnityEngine;
using UnityEngine.UI;

namespace Space.Logic
{
    public class SplitScreenLogic : MonoBehaviour
    {
        public List<PlayerInput> playerInputs;
        public List<Camera> cameras;

        IEnumerator Start()
        {
            yield return new WaitForSeconds(0.3f);
            
            while (isActiveAndEnabled)
            {
                List<Camera> activeCameras = new List<Camera>();
                for (int i = 0; i < playerInputs.Count; i++)
                {
                    if (playerInputs[i].player != null)
                    {
                        activeCameras.Add(cameras[i]);
                        cameras[i].enabled = true;
                    }
                    else
                    {
                        cameras[i].enabled = false;
                    }
                    
                    yield return new WaitForEndOfFrame();
                }

                if (activeCameras.Count == 1)
                {
                    activeCameras[0].rect = new Rect(0, 0, 1.0f, 1.0f);
                }
                else if (activeCameras.Count == 2)
                {
                    activeCameras[0].rect = new Rect(0, 0.5f, 1f, .5f);
                    activeCameras[1].rect = new Rect(0, 0, 1f, .5f);
                }
                else if (activeCameras.Count == 3)
                {
                    activeCameras[0].rect = new Rect(0, 0.5f, .5f, .5f);
                    activeCameras[1].rect = new Rect(0.5f, 0.5f, .5f, .5f);
                    activeCameras[2].rect = new Rect(0, 0, .5f, .5f);
                }
                else if (activeCameras.Count == 4)
                {
                    activeCameras[0].rect = new Rect(0, 0.5f, .5f, .5f);
                    activeCameras[1].rect = new Rect(0.5f, 0.5f, .5f, .5f);
                    activeCameras[2].rect = new Rect(0, 0, .5f, .5f);
                    activeCameras[3].rect = new Rect(0.5f, 0, .5f, .5f);
                }
                
                yield return new WaitForSeconds(1.0f);
            }
        }
    }
}