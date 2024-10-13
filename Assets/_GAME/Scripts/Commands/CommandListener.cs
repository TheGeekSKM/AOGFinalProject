using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CommandListener : MonoBehaviour
{
    [SerializeField] string _desiredCommand = "Enter Command Here: ";
    public UnityEvent OnDesiredCommandReceived;

    public void ListenForCommand(string command)
    {
        if (command == _desiredCommand) OnDesiredCommandReceived?.Invoke();
    }
}
