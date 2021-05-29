using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolCheckPlayerInRange : MonoBehaviour
{
    public float radiusSearch;
    public GameObject sprite;

    public bool rangeSearchActive;

    public GameObject prefabQTEEscape;
    private Animator animator;

    private void OnDrawGizmos()
    {
        if (rangeSearchActive)
            Gizmos.DrawSphere(sprite.transform.position, radiusSearch);
    }
    private void Start()
    {
        animator = this.GetComponent<Animator>();
    }
    public void GetOutOfHouse()
    {
        animator.SetTrigger("getOut");
    }
    public void BackInHouse()
    {
        animator.ResetTrigger("getOut");
        animator.SetTrigger("backIn");
    }
    public void SearchInRadius()
    {
        rangeSearchActive = true;
        var results = Physics2D.CircleCastAll(sprite.transform.position, radiusSearch, Vector2.zero);

        if(results.Length > 0)
        {
            var player = results[0].collider.GetComponent<Player>();
            if(player != null)
            {
                var tmp = Instantiate(prefabQTEEscape, player.transform.position, Quaternion.identity);/*.GetComponent<InteractionWithPlayerQTE>();*/
                //tmp.TriggerNewQTE();
            }
        }
    }
    public void StopSearchInRadius()
    {
        rangeSearchActive = false;
        BackInHouse();
    }
}
