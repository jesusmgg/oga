using System.Collections;
using UnityEngine;

namespace Space.Logic
{
    public class Bullet : MonoBehaviour
    {
        public float speed;
        public float life;
        public int damage;
        public GameObject sparksPrefab;

        public string targetTag;
        public GameObject target;

        public AudioClip shootSound;

        Vector2 direction;

        bool startedMoving;

        IEnumerator Start()
        {
            startedMoving = false;
            
            FindObjectOfType<AudioPlayer>().PlaySound(shootSound);
            
            yield return new WaitUntil(() => target != null);

            Destroy(gameObject, life);

            direction = target.transform.position - transform.position;
            direction.Normalize();
        }

        void Update()
        {
            if (target != null)
            {
                startedMoving = true;
            }

            if (startedMoving)
            {
                transform.Translate(direction * speed * Time.deltaTime);
            }
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag(targetTag))
            {
                var sparks = Instantiate(sparksPrefab, transform.position, Quaternion.identity, null);
                other.GetComponent<EnemyLogic>().TakeDamage(damage);
                Destroy(sparks, 1.0f);
                Destroy(gameObject, 0.1f);
            }
        }
    }
}