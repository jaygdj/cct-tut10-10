using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HitscanScript : MonoBehavior 
{
    [SerializeField]
    private float maxRange;
       
    [SerializeField]
    private int damage;
    
    [SerializeField]
    private TrailRenderer trailPrefab;

    [SerializeField]
    private float trailSpeed;
    
    // ...
 
    private IEnumerator CreateTrail(Vector3 initialPos, Vector3 endPos, RaycastHit hitObj)
    {
        TrailRenderer trail = Instantiate(trailPrefab, initialPos, Quaternion.identity);

        float remainingDist = Vector3.Distance(initialPos, endPos);
        while (remainingDist > 0)
        {
            trail.transform.position += (endPos - initialPos).normalized * trailSpeed * Time.deltaTime;

            remainingDist -= trailSpeed * Time.deltaTime;
            yield return null;
        }
        
        if (hitObj.collider != null)
        {
            Collider[] nearbyObjs = Physics.OverlapSphere(endPos, 0.1f);
            if (Array.Exists(nearbyObjs, nearbyObj => nearbyObj.name == hitObj.collider.name))
            {
                if (hitObj.collider.GetComponent<HealthScript>())
                {
                    Debug.Log($"Dealing {damage} damage");
                    hitObj.collider.GetComponent<HealthScript>().ApplyDamage(damage);
                }
            }
        }
        
        Destroy(trail.gameObject, trail.time);
    }
	    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
            if (Mouse.current.leftButton.wasPressedThisFrame) 
    { 
        Health += 10; 
    } 
    }
	
}