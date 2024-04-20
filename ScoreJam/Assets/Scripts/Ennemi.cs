using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.UI.CanvasScaler;

public class Ennemi : MonoBehaviour
{
    public NavMeshAgent ennemi;
    public Transform PLayer;
    public bool Incombat;

    public GameObject enemiDetection;
    public bool agroed = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        agroed = enemiDetection.GetComponent<EnnemiDetection>().PlayerInColl;
        if(agroed || Incombat) { ennemi.SetDestination(PLayer.position);Incombat = true; }
        //transform.position = Vector3.MoveTowards(this.transform.position, PLayer.position, 3 * Time.deltaTime);
    }
}
