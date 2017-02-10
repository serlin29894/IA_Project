using UnityEngine;
using System.Collections;

namespace Steerings
{

    public class My_Keep_Distance : SteeringBehaviour
    {

        public float RequiredDistance = 0f;

        public GameObject target;

        private static GameObject surrogateTarget = null;

        public override SteeringOutput GetSteering()
        {
            // no KS? get it
            if (this.ownKS == null) this.ownKS = GetComponent<KinematicState>();

            if (this.target == null)
                Debug.Log("Null target in Seek of " + this.gameObject);

            return My_Keep_Distance.GetSteering(this.ownKS, this.target, this.RequiredDistance);
        }

        public static SteeringOutput GetSteering(KinematicState ownKS, GameObject target,float RequiredDsitance)
        {
            Vector3 DirectionFromTarget;
            Vector3 DesiredPos;

        // Compute direction to target
            DirectionFromTarget = (ownKS.position - target.transform.position).normalized;
            DesiredPos = target.transform.position + DirectionFromTarget * RequiredDsitance;


            if (My_Keep_Distance.surrogateTarget == null)
                My_Keep_Distance.surrogateTarget = new GameObject("Dummy (Surrogate Target for Pursue)");
            My_Keep_Distance.surrogateTarget.transform.position = DesiredPos;


            return Seek.GetSteering(ownKS,surrogateTarget);
        }
    }
}

