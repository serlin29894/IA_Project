using UnityEngine;
using System.Collections;
using Steerings;

namespace FSM
{
    public class FSM_ANT_TwoPointsWander : MonoBehaviour
    {
        public enum State { INITIAL, GOING_TO_A, GOING_TO_B };

        public State currentState = State.INITIAL;


        void Start()
        {
            this.enabled = true;

        }

        public  void Exit()
        {
            this.enabled = false;
        }

        public  void ReEnter()
        {
            this.enabled = true;
        }

        // Update is called once per frame
        void Update()
        {

            switch (currentState)
            {
                case State.INITIAL:
                   
                    break;
                case State.GOING_TO_A:

                    break;
                case State.GOING_TO_B:

                  
                    break;
            }
        }

        void ChangeState(State newState)
        {
            // exit logic
            switch (currentState)
            {
                case State.GOING_TO_A:
                    

                    break;
                case State.GOING_TO_B:
                    

                    break;
            }

            // enter logic
            switch (newState)
            {
                case State.GOING_TO_A:
                    

                    break;
                case State.GOING_TO_B:
                  

                    break;
            }

            currentState = newState;
        }



    }
}
