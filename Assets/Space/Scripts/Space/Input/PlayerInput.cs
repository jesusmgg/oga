using System.Collections;
using Space.Logic;
using SwordGC.AirController;
using UnityEngine;
using UnityEngine.UI;

namespace Space.Input
{
    public class PlayerInput : MonoBehaviour
    {
        public int playerId;
        public GameObject ship;

        [Header("Parameters")]
        public float acceleration;

        public float speed;

        Vector2 velocity;
        public Player player;
        Rigidbody2D rigidbody2D;
        PlayerLogic logic;

        bool collidingWithWall;
        bool isPanning;
        bool isConnecting;

        void Start()
        {
            velocity = Vector2.zero;
            rigidbody2D = ship.GetComponent<Rigidbody2D>();
            logic = GetComponent<PlayerLogic>();
        }

        void OnEnable()
        {
            player = null;
            StartCoroutine(ConnectToPlayer());
        }

        void OnDisable()
        {
            player = null;
        }

        IEnumerator ConnectToPlayer()
        {
            if (isConnecting)
            {
                yield return null;
            }

            else
            {
                isConnecting = true;

                while (player == null)
                {
                    if (SwordGC.AirController.AirController.Instance == null)
                    {
                        yield return new WaitForSeconds(0.5f);
                        continue;
                    }

                    player = SwordGC.AirController.AirController.Instance.GetPlayer(playerId);

                    if (!player.HasDevice)
                    {
                        player = null;
                    }

                    yield return new WaitForEndOfFrame();
                }

                Debug.Log($"Connected to player {playerId}");
                isConnecting = false;
            }
        }

        void Update()
        {
            if (player != null)
            {
                if (!player.HasDevice)
                {
                    player = null;
                }
            }
            else
            {
                StartCoroutine(ConnectToPlayer());
            }

            ship.SetActive(player != null);

            if (player != null)
            {
                /*
                player.Input.Pan.TouchStart((Vector2 start) => { isPanning = true; });

                player.Input.Pan.Touching((Vector2 start, Vector2 cur) =>
                {
                    //Debug.Log($"playerId: {playerId}   start: {start}   cur: {cur}   dir: {(cur-start).normalized}");
                    velocity += (cur - start) * speed * Time.deltaTime;
                });

                player.Input.Pan.TouchEnd((Vector2 start, Vector2 cur) => { isPanning = false; });
                */

                if (player.Input.GetKey("DPadUp") || player.Input.GetKey("DPadDown") ||
                    player.Input.GetKey("DPadLeft") ||
                    player.Input.GetKey("DPadRight"))
                {
                    isPanning = true;
                    
                    Vector2 touchDirection = Vector2.zero;
                    
                    if (player.Input.GetKey("DPadUp")) {touchDirection += Vector2.up;}
                    if (player.Input.GetKey("DPadDown")) {touchDirection += Vector2.down;}
                    if (player.Input.GetKey("DPadLeft")) {touchDirection += Vector2.left;}
                    if (player.Input.GetKey("DPadRight")) {touchDirection += Vector2.right;}
                    
                    velocity += touchDirection * speed * Time.deltaTime;
                }
                else
                {
                    isPanning = false;
                }

                if (!isPanning)
                {
                    velocity = Vector2.Lerp(velocity, Vector2.zero, 2.0f * acceleration * Time.deltaTime);
                }

                velocity = Vector2.ClampMagnitude(velocity, speed);

                ship.transform.position = (Vector2) ship.transform.position + velocity * Time.deltaTime;

                if (isPanning)
                {
                    float angle = Vector2.SignedAngle(Vector2.up, velocity.normalized);
                    ship.transform.eulerAngles = new Vector3(0.0f, 0.0f, angle);
                }

                player.Device.SetData("shields", $"Shields: {logic.shields}%");
                player.Device.SetData("iron", $"Iron: {logic.iron}");
                player.Device.SetData("fluoride", $"Fluoride: {logic.fluoride}");
                player.Device.SetData("gas", $"Gas: {logic.gas}");
                player.Device.SetData("storage", $"Storage: {logic.storage}");

                string buildString =
                    $"Build ({logic.turretIronCost}i, {logic.turretGasCost}g, {logic.turretFluorideCost}f)";
                player.Device.SetData("build", buildString);

                if (player.Input.GetKeyUp("BuildButton"))
                {
                    logic.BuildTurret();
                }
            }
        }
    }
}