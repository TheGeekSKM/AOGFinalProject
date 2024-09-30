using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class Command 
{
    public string CommandName { get; private set; }
    public string[] Args { get; private set; }

    public Command(string commandName, string[] args)
    {
        CommandName = commandName;
        Args = args;
    }

    public override string ToString()
    {
        string result = $"CommandName: {CommandName}";

        if (Args.Length > 0)
        {
            result += ", Args: ";
            for (int i = 0; i < Args.Length; i++)
            {
                result += Args[i];
                if (i < Args.Length - 1)
                {
                    result += ", ";
                }
            }
        }

        return result;
    }
}
