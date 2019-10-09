using UnityEngine;

public class IddleNpcController : MonoBehaviour
{
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetFloat("Horizontal", 0);
        animator.SetFloat("Vertical", 0);
    }
}
