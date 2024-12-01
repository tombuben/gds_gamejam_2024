using System.Collections;
using UnityEngine;

public class FollowDwarf : MonoBehaviour
{
    public DialogInstance dialog;
    private CharacterController controller;
    public CharacterController mainPlayer;
    public float speed;
    bool following = false;
    private Animator animator;
    
    public GameObject destroyOnFinish;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        
        dialog.DialogFinishedUnsuccessfully += StartFollowing;
        dialog.DialogFinishedSuccessfully += StartFollowing;
        GlobalManager.Instance.StopSmudlaFollowing += StopSmudlaFollowing;
    }

    private void StopSmudlaFollowing()
    {
        following = false;
        destroyOnFinish.SetActive(false);

        StartCoroutine(DelayScore());

    }

    IEnumerator DelayScore()
    {
        yield return new WaitForSeconds(0.5f);
        GlobalManager.Instance.apologizeCount += 1;
        GlobalManager.Instance.TrpaslikApologized?.Invoke();
    }

    private void StartFollowing()
    {
        following = true;
        dialog.enabled = false;
        GlobalManager.Instance.SmudlaIsFollowing = true;
        GlobalManager.Instance.SmudlaStartedFollowing?.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetMovement = controller.isGrounded ? Vector3.zero : Vector3.down * 10f;
        if (following && Vector3.Distance(mainPlayer.transform.position, transform.position) > 3f)
        {
            targetMovement += (mainPlayer.transform.position - transform.position).normalized * (speed * Time.deltaTime);
            
            Vector3 direction = targetMovement.normalized;
            animator.SetFloat("LeftRight", direction.x > 0 ? 1.0f : -1.0f);
            controller.Move(targetMovement);
        }
        else
        {
            animator.SetFloat("LeftRight", 0.0f);
        }
    }
}
