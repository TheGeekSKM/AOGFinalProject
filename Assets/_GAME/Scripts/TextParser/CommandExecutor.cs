using UnityEngine;
using SaiUtils.GameEvents;
using SaiUtils.Singleton;

public class CommandExecutor : Singleton<CommandExecutor>
{
    [SerializeField] VectorTwoEvent _moveEvent;
    [SerializeField] VectorTwoEvent _pushEvent;
    [SerializeField] VectorTwoEvent _scoutAheadEvent;
    [SerializeField] BoolEvent _crouchEvent;
    [SerializeField] VoidEvent _restEvent;
    [SerializeField] VoidEvent _ambushEvent;
    [SerializeField] VoidEvent _freezeEvent;

    public void ExecuteCommand(Command command)
    {
        switch (command.CommandName)
        {
            case "move":
                HandleMove(command.Args);
                break;
            case "freeze":
                HandleFreeze(command.Args);
                break;
            case "scoutAhead":
                HandleScoutAhead(command.Args);
                break;
            case "rest":
                HandleRest(command.Args);
                break;
            case "ambush":
                HandleAmbush(command.Args);
                break;
            case "push":
                HandlePush(command.Args);
                break;
            case "loot":
                HandleLoot(command.Args);
                break;
            case "inspect":
                HandleInspect(command.Args);
                break;
            case "useItem":
                HandleUseItem(command.Args);
                break;
            case "setTrap":
                HandleSetTrap(command.Args);
                break;
            default:
                Debug.LogWarning("Invalid command");
                break;
        }

    }

    void HandleMove(string[] args)
    {
        if (args.Length != 2)
        {
            Debug.LogWarning("Invalid number of arguments for move command");
            return;
        }

        float x = float.Parse(args[0]);
        float z = float.Parse(args[1]);

        _moveEvent?.Raise(new Vector2(x, z));
        
    }

    void HandleFreeze(string[] args)
    {
        if (args.Length != 0)
        {
            Debug.LogWarning("Invalid number of arguments for freeze command");
            return;
        }

        _freezeEvent?.Raise();
    }

    void HandleScoutAhead(string[] args)
    {
        if (args.Length != 2)
        {
            Debug.LogWarning("Invalid number of arguments for scoutAhead command");
            return;
        }

        float x = float.Parse(args[0]);
        float z = float.Parse(args[1]);

        _scoutAheadEvent?.Raise(new Vector2(x, z));
    }

    void HandleRest(string[] args)
    {
        if (args.Length != 0)
        {
            Debug.LogWarning("Invalid number of arguments for rest command");
            return;
        }

        _restEvent?.Raise();
    }

    void HandleAmbush(string[] args)
    {
        if (args.Length != 0)
        {
            Debug.LogWarning("Invalid number of arguments for ambush command");
            return;
        }

        _ambushEvent?.Raise();
    }

    void HandlePush(string[] args)
    {
        if (args.Length != 2)
        {
            Debug.LogWarning("Invalid number of arguments for push command");
            return;
        }

        float x = float.Parse(args[0]);
        float z = float.Parse(args[1]);

        _pushEvent?.Raise(new Vector2(x, z));
    }

    void HandleLoot(string[] args)
    {
    }

    void HandleInspect(string[] args)
    {
    }

    void HandleUseItem(string[] args)
    {
    }

    void HandleSetTrap(string[] args)
    {
    }
}
