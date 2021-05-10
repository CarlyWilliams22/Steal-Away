using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardLookScript : MonoBehaviour
{
    public GameObject head;
    public LayerMask guardLayer;
    public LayerMask playerLayer;
    public Transform player;
    private Transform _transform;

    public float randomLookOffset;
    public int numLooksPerUpdate;
    public float maxLookDist;

    private bool alarmHasSounded;

    // Start is called before the first frame update
    void Start()
    {
        _transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Vector3.Distance(_transform.position, player.position) <= maxLookDist)
        {
            for (int i = 0; i < numLooksPerUpdate; i++)
            {
                Vector3 offset = new Vector3(Random.Range(-randomLookOffset, randomLookOffset), Random.Range(-randomLookOffset, randomLookOffset), Random.Range(-randomLookOffset, randomLookOffset));
                // Debug.DrawRay(head.transform.position, head.transform.forward + offset, Color.red);
                RaycastHit hit;
                if (Physics.Raycast(head.transform.position, head.transform.forward + offset, out hit, Mathf.Infinity, ~guardLayer))
                {
                    if (ContainsLayer(playerLayer, hit.collider.gameObject.layer))
                    {
                        if (!alarmHasSounded)
                        {
                            Messenger.Broadcast(GameEvent.ALARM_SOUNDED);
                            alarmHasSounded = true;
                        }
                    }
                }
            }
        }
    }

    private static bool ContainsLayer(LayerMask mask, int layer)
    {
        return mask == (mask | (1 << layer));
    }
}
