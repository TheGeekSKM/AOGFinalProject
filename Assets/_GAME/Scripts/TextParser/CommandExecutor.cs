using UnityEngine;
using SaiUtils.GameEvents;
using SaiUtils.Singleton;

public class CommandExecutor : Singleton<CommandExecutor>
{
    [Header("Vector 2 Events")]
    [SerializeField] VectorTwoEvent _moveEvent;
    [SerializeField] VectorTwoEvent _pushEvent;
    [SerializeField] VectorTwoEvent _scoutAheadEvent;

    [Header("Bool Events")]
    [SerializeField] BoolEvent _crouchEvent;

    [Header("String Events")]
    [SerializeField] StringEvent _useItemEvent;

    [Header("Void Events")]
    [SerializeField] VoidEvent _restEvent;
    [SerializeField] VoidEvent _ambushEvent;
    [SerializeField] VoidEvent _freezeEvent;
    [SerializeField] VoidEvent _lootEvent;
    [SerializeField] VoidEvent _inspectEvent;
    [SerializeField] VoidEvent _setTrapEvent;

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
            case "inventory":
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
        if (args.Length != 0)
        {
            Debug.LogWarning("Invalid number of arguments for loot command");
            return;
        }

        _lootEvent?.Raise();
    }

    void HandleInspect(string[] args)
    {
        if (args.Length != 0)
        {
            Debug.LogWarning("Invalid number of arguments for inspect command");
            return;
        }

        _inspectEvent?.Raise();
    }

    void HandleUseItem(string[] args)
    {
        if (args.Length != 1)
        {
            Debug.LogWarning("Invalid number of arguments for useItem command");
            return;
        }

        _useItemEvent?.Raise(args[0]);
    }

    void HandleSetTrap(string[] args)
    {
        if (args.Length != 0)
        {
            Debug.LogWarning("Invalid number of arguments for setTrap command");
            return;
        }

        _setTrapEvent?.Raise();
    }
}
