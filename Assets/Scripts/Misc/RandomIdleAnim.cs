using UnityEngine;

public class RandomIdleAnim : MonoBehaviour
{
    private Animator myAnimator;

    private void Awake() {
        myAnimator = GetComponent<Animator>();
    }

    void Start()
    {
        AnimatorStateInfo stateInfo = myAnimator.GetCurrentAnimatorStateInfo(0);
        myAnimator.Play(stateInfo.fullPathHash, -1, Random.Range(0f, 1f));
    }
}
