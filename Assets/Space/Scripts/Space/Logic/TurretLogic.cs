using System.Collections;
using UnityEngine;

namespace Space.Logic
{
    public class TurretLogic : MonoBehaviour
    {
        [Header("Stats")]
        public int shields;
        public int damage;
        public float range;
        public int reloadFrames;

        int currentReload;

        [Header("Parameters")]
        public Vector2 defaultUpDirection;
        public Transform shootPoint;
        public GameObject bulletPrefab;
        public GameObject explosionPrefab;
        public GameObject target;

        EnemySpawner enemySpawner;

        AudioPlayer audioPlayer;
        public AudioClip builtSound;

        void Start()
        {
            enemySpawner = FindObjectOfType<EnemySpawner>();

            currentReload = reloadFrames;

            audioPlayer = FindObjectOfType<AudioPlayer>();
            
            audioPlayer.PlaySound(builtSound);
        }

        IEnumerator DoContinuousDamage()
        {
            while (shields > 0)
            {
                yield return new WaitForSeconds(1.0f);
                shields -= 1;
            }
        }

        void Update()
        {
            if (shields <= 0)
            {
                Instantiate(explosionPrefab, transform.position, Quaternion.identity, null);
                Destroy(gameObject, 0.1f);
            }
            
            if (target != null)
            {
                Vector2 direction = target.transform.position - transform.position;
                float angle = Vector2.SignedAngle(defaultUpDirection, direction.normalized);
                transform.eulerAngles = new Vector3(0.0f, 0.0f, angle);

                if (currentReload <= 0)
                {
                    var bullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity, null);
                    bullet.GetComponent<Bullet>().damage = damage;
                    bullet.GetComponent<Bullet>().target = target;

                    currentReload = reloadFrames;
                }
            }

            foreach (var enemy in enemySpawner.enemies)
            {
                if (Vector2.Distance(transform.position, enemy.transform.position) <= range)
                {
                    target = enemy;
                    break;
                }

                target = null;
            }

            currentReload--;
        }

        void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, range);
        }
    }
}