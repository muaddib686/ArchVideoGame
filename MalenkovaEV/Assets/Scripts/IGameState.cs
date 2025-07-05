using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public interface IGameState
    {
        void EnterState();
        void ExitState();
        void UpdateState();
    }

