using UnityEngine;

namespace Space.Logic
{
    public class EnemyLogic : MonoBehaviour
    {
        [Header("Stats")]
        public int shields;
        public float speed;
        public int impactDamage;

        [Header("Shooting")]
        public bool shoots;
        public int shootingDamage;

        [Header("Parameters")]
        public GameObject explosionPrefab;
        public GameObject targetPlanet;
        public GameObject currentTarget;

        void Update()
        {
            if (shields <= 0)
            {
                Instantiate(explosionPrefab, transform.position, Quaternion.identity, null);
                Destroy(gameObject);
            }

            if (targetPlanet != null)
            {
                Vector2 translation = (targetPlanet.transform.position - transform.position).normalized;
                translation *= speed * Time.deltaTime;

                transform.position = (Vector2) transform.position + translation;
                
                float angle = Vector2.SignedAngle(Vector2.up, translation);
                transform.eulerAngles = new Vector3(0.0f, 0.0f, angle);
            }
        }

        public void TakeDamage(int damage)
        {
            shields -= damage;
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("PlanetEnemy"))
            {
                other.transform.parent.GetComponent<Planet>().LoseResources(impactDamage);
                TakeDamage(shields);
            }
        }
    }
}