using UnityEngine;
using SaiUtils.GameEvents;

[CreateAssetMenu(fileName = "New Enemy Event", menuName = "GameEvents/EnemyEvent")]
    public class EnemyEvent : BaseGameEvent<EnemyBrain> {}

    // I don't need to add a Raise method here because it is already implemented in the BaseGameEvent class
    // because the T in the BaseGameEvent class is now a bool, the Raise method will now automatically take a bool as a parameter