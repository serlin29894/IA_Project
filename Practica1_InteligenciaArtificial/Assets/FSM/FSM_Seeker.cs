using UnityEngine;
using System.Collections;
using Steerings;

namespace FSM
{
    public class FSM_Seeker : MonoBehaviour
    {
        public enum State { Initial,Wandering, Follow, Hit_Target };

        public State currentState = State.Initial;

        private Seeker_Blackboard blackboard;

        private WanderAroundPlusAvoid wander;
        private Face face;
        private KinematicState myKinematicState;

        private GameObject target;

        void Start()
        {
            blackboard = GetComponent<Seeker_Blackboard>();
            if (blackboard == null)
            {
                blackboard = gameObject.AddComponent<Seeker_Blackboard>();
            }

            wander = GetComponent<WanderAroundPlusAvoid>();
            if (wander == null)
            {
                wander = gameObject.AddComponent<WanderAroundPlusAvoid>();
            }

            face = GetComponent<Face>();
            if (face == null)
            {
                face = gameObject.AddComponent<Face>();
            }

            myKinematicState = GetComponent<KinematicState>();
            if(myKinematicState == null)
            {
                myKinematicState = gameObject.AddComponent<KinematicState>();
            }

            wander.enabled = false;
            face.enabled = false;

        }

        public void Exit()
        {
            this.enabled = false;
        }

        public void ReEnter()
        {
            this.enabled = true;
        }

        // Update is called once per frame
        void Update()
        {

            switch (currentState)
            {
                case State.Initial:
                    ChangeState(State.Wandering);
                    break;
                case State.Wandering:
                    target = SensingUtils.FindInstanceWithinRadius(this.gameObject, "Player", blackboard.RadiusDetection);
                    if (target != null)
                    {
                        ChangeState(State.Follow);
                    }
                        break;
                case State.Follow:
                    if (SensingUtils.DistanceToTarget(this.gameObject, target) <= blackboard.HitDistance)
                    {
                        ChangeState(State.Hit_Target);
                    }

                    if (SensingUtils.DistanceToTarget(this.gameObject, target) > blackboard.EscapeDistance)
                    {
                        ChangeState(State.Wandering);
                    }

                    break;

                case State.Hit_Target:
                    if (SensingUtils.DistanceToTarget(this.gameObject, target) > blackboard.HitDistance)
                    {
                        ChangeState(State.Follow);
                    }

                    break;
            }
        }

        void ChangeState(State newState)
        {
            // exit logic
            switch (currentState)
            {
                case State.Wandering:
                    wander.attractor = null;
                    wander.enabled = false;

                    break;
                case State.Follow:
                    wander.attractor = null;
                    wander.enabled = false;

                    break;
                case State.Hit_Target:
                    myKinematicState.maxAcceleration = 60;
                    myKinematicState.maxSpeed = 20;
                    face.enabled = false;

                    break;
            }

            // enter logic
            switch (newState)
            {
                case State.Wandering:
                    wander.enabled = true;
                    wander.seekWeight = blackboard.WanderingSeekWeight;
                    wander.attractor = blackboard.Attractor;
                    break;
                case State.Follow:
                    wander.enabled = true;
                    wander.seekWeight = blackboard.FollowSeekWeight;
                    wander.attractor = target;

                    break;
                case State.Hit_Target:
                    face.enabled = true;
                    face.target = target;
                    myKinematicState.maxSpeed = 5;
                    myKinematicState.maxAcceleration = 5;

                    break;
            }

            currentState = newState;
        }



    }
}
