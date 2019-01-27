using UnityEngine;

namespace Space.Logic
{
    public class Explosion : MonoBehaviour
    {
        Animator animator;

        public AudioClip explosionSound;

        void Start()
        {
            animator = GetComponent<Animator>();
            
            FindObjectOfType<AudioPlayer>().PlaySound(explosionSound);
        }

        void Update()
        {
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                Destroy(gameObject);
            }
        }
    }
}