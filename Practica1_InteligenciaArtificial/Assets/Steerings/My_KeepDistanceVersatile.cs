using UnityEngine;
using System.Collections;

namespace Steerings
{

    public class My_KeepDistanceVersatile : SteeringBehaviour
    {

        public float RequiredDistance = 0f;
        public float DesiredAngle = 180f;
        public GameObject target;

        private static GameObject surrogateTarget = null;

        public override SteeringOutput GetSteering()
        {
            // no KS? get it
            if (this.ownKS == null) this.ownKS = GetComponent<KinematicState>();

            if (this.target == null)
                Debug.Log("Null target in Seek of " + this.gameObject);

            return My_KeepDistanceVersatile.GetSteering(this.ownKS, this.target, this.RequiredDistance, this.DesiredAngle);
        }

        public static SteeringOutput GetSteering(KinematicState ownKS, GameObject target, float RequiredDsitance,float DesiredAngle)
        {
            Vector3 DirectionFromTarget;
            Vector3 DesiredPos;
            Vector3 OrientationVector;

            float targetOrientation = target.transform.eulerAngles.z;

            // Compute direction to target
            OrientationVector = Utils.OrientationToVector(targetOrientation + DesiredAngle);

            DirectionFromTarget = ((ownKS.position - target.transform.position)).normalized;
           
            DesiredPos = (target.transform.position + DirectionFromTarget * RequiredDsitance) + OrientationVector;
            
            
             //Matriz de rotacion 2d
            //DirectionFromTarget.y = OrientationVector.x * Mathf.Sin(Mathf.Deg2Rad * DesiredAngle) + OrientationVector.y * Mathf.Cos(Mathf.Deg2Rad * DesiredAngle);
            //DirectionFromTarget.x = OrientationVector.x * Mathf.Cos(Mathf.Deg2Rad * DesiredAngle) - OrientationVector.y * Mathf.Sin(Mathf.Deg2Rad * DesiredAngle);
            //DirectionFromTarget.Normalize();

            if (My_KeepDistanceVersatile.surrogateTarget == null)
            {
                My_KeepDistanceVersatile.surrogateTarget = new GameObject("Dummy (Surrogate Target for keep distance)");
            }
            My_KeepDistanceVersatile.surrogateTarget.transform.position = DesiredPos;


            return Seek.GetSteering(ownKS, surrogateTarget);
        }
    }
}

