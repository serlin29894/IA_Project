using UnityEngine;
using System.Collections;

namespace Steerings
{
    public class KeepDistanceVersatilePlusRepulsion : SteeringBehaviour
    {
        public string idTag = "BOID";

        public float RequiredDistance = 0f;
        public float DesiredAngle = 180f;
        public float repulsionThreshold = 10f;

        public GameObject target;

        private static GameObject surrogateTarget;



        public override SteeringOutput GetSteering()
        {
            // no KS? get it
            if (this.ownKS == null) this.ownKS = GetComponent<KinematicState>();

            if (this.target == null)
                Debug.Log("Null target in Seek of " + this.gameObject);

            return KeepDistanceVersatilePlusRepulsion.GetSteering(this.ownKS, this.target, this.idTag, this.RequiredDistance, this.DesiredAngle, this.repulsionThreshold);
        }

        public static SteeringOutput GetSteering(KinematicState ownKS, GameObject target, string idTag, float RequiredDistance, float DesiredAngle, float RepulsionThreshold)
        {
           
            SteeringOutput rp = LinearRepulsion.GetSteering(ownKS, idTag, RepulsionThreshold);
            SteeringOutput Kep = My_KeepDistanceVersatile.GetSteering(ownKS, target, RequiredDistance, DesiredAngle);

            if (rp == null)
            {
                rp = new SteeringOutput();
            }

            SteeringOutput result = new SteeringOutput();

            result.linearAcceleration = rp.linearAcceleration * 0.6f + Kep.linearAcceleration * 0.4f;

            if (result.linearAcceleration.magnitude > ownKS.maxAcceleration)
                result.linearAcceleration = result.linearAcceleration.normalized * ownKS.maxAcceleration;

            return result;
        }
    }
}