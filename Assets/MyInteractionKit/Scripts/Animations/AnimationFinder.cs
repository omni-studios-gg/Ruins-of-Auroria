using UnityEngine;

namespace MyInteractionKit
{
    public class AnimationFinder : MonoBehaviour
    {
        [HideInInspector]
        public AnimationClip[] animationClips; // This will hold the list of animation clips
        public Animator animator;

        void OnValidate()
        {
            // Get the Animator component attached to this GameObject
            animator = GetComponent<Animator>();

            if (animator != null)
            {
                // Get the RuntimeAnimatorController associated with the Animator
                RuntimeAnimatorController controller = animator.runtimeAnimatorController;

                if (controller != null)
                {
                    // Assign all animation clips to the public array
                    animationClips = controller.animationClips;
                }
            }
        }
    }
}
