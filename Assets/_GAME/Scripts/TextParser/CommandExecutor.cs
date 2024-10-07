using UnityEngine;
using SaiUtils.GameEvents;
using SaiUtils.Singleton;
using System.Collections.Generic;

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
    
    [Header("Int Events")]
    [SerializeField] IntEvent _shootEvent;

    [Header("Void Events")]
    [SerializeField] VoidEvent _restEvent;
    [SerializeField] VoidEvent _mapOpenEvent;
    [SerializeField] VoidEvent _ambushEvent;
    [SerializeField] VoidEvent _freezeEvent;
    [SerializeField] VoidEvent _lootEvent;
    [SerializeField] VoidEvent _inspectEvent;
    [SerializeField] VoidEvent _setTrapEvent;

    [SerializeField] List<string> _previousCommands = new List<string>();
    int _commandIndex = 0;

    public void AddCommandToHistory(string command)
    {
        //if the command is the same as the last one, don't add it
        if (_previousCommands.Count > 0 && _previousCommands[_previousCommands.Count - 1] == command) return;
        _previousCommands.Add(command);
        _commandIndex = _previousCommands.Count - 1;
    }

    public string GetLastCommand()
    {
        if (_previousCommands.Count == 0) return "";
        var answer = _previousCommands[_commandIndex];
        if (_commandIndex > 0) _commandIndex--;
        return answer;
    }

    public string GetNextCommand()
    {
        if (_previousCommands.Count == 0) return "";
        var answer = _previousCommands[_commandIndex];
        if (_commandIndex < _previousCommands.Count - 1) _commandIndex++;
        return answer;
    }

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
            case "shoot":
                HandleShoot(command.Args);
                break;
            case "map":
                HandleMap(command.Args);
                break;
            default:
                Debug.LogWarning("Invalid command");
                WarningManager.Instance.ShowWarning("Invalid command", 3f);
                break;
        }

    }

    void HandleMove(string[] args)
    {
        if (args.Length != 2)
        {
            Debug.LogWarning("Invalid number of arguments for move command");
            WarningManager.Instance.ShowWarning("Invalid number of arguments for move command", 3f);
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
            WarningManager.Instance.ShowWarning("Invalid number of arguments for freeze command", 3f);
            return;
        }

        _freezeEvent?.Raise();
    }

    void HandleScoutAhead(string[] args)
    {
        if (args.Length != 2)
        {
            Debug.LogWarning("Invalid number of arguments for scoutAhead command");
            WarningManager.Instance.ShowWarning("Invalid number of arguments for scoutAhead command", 3f);
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
            WarningManager.Instance.ShowWarning("Invalid number of arguments for rest command", 3f);
            return;
        }

        _restEvent?.Raise();
    }

    void HandleAmbush(string[] args)
    {
        if (args.Length != 0)
        {
            Debug.LogWarning("Invalid number of arguments for ambush command");
            WarningManager.Instance.ShowWarning("Invalid number of arguments for ambush command", 3f);
            return;
        }

        _ambushEvent?.Raise();
    }

    void HandlePush(string[] args)
    {
        if (args.Length != 2)
        {
            Debug.LogWarning("Invalid number of arguments for push command");
            WarningManager.Instance.ShowWarning("Invalid number of arguments for push command", 3f);
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
            WarningManager.Instance.ShowWarning("Invalid number of arguments for loot command", 3f);
            return;
        }

        _lootEvent?.Raise();
    }

    void HandleInspect(string[] args)
    {
        if (args.Length != 0)
        {
            Debug.LogWarning("Invalid number of arguments for inventory command");
            WarningManager.Instance.ShowWarning("Invalid number of arguments for inventory command", 3f);
            return;
        }

        _inspectEvent?.Raise();
    }

    void HandleUseItem(string[] args)
    {
        if (args.Length != 1)
        {
            Debug.LogWarning("Invalid number of arguments for useItem command");
            WarningManager.Instance.ShowWarning("Invalid number of arguments for useItem command", 3f);
            return;
        }

        _useItemEvent?.Raise(args[0]);
    }

    void HandleSetTrap(string[] args)
    {
        if (args.Length != 0)
        {
            Debug.LogWarning("Invalid number of arguments for setTrap command");
            WarningManager.Instance.ShowWarning("Invalid number of arguments for setTrap command", 3f);
            return;
        }

        _setTrapEvent?.Raise();
    }

    void HandleShoot(string[] args)
    {
        if (args.Length != 1)
        {
            Debug.LogWarning("Invalid number of arguments for shoot command");
            WarningManager.Instance.ShowWarning("Invalid number of arguments for shoot command", 3f);
            return;
        }

        int enemyIndex = int.Parse(args[0]);
        _shootEvent?.Raise(enemyIndex);
    }

    void HandleMap(string[] args)
    {
        if (args.Length != 0)
        {
            Debug.LogWarning("Invalid number of arguments for map command");
            WarningManager.Instance.ShowWarning("Invalid number of arguments for map command", 3f);
            return;
        }

        _mapOpenEvent?.Raise();
    }
}
