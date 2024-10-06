using System;
using SaiUtils.Extensions;
using TMPro;
using UnityEngine;

public class TextParseManager : MonoBehaviour
{
    [SerializeField] TMP_InputField _inputField;

    void OnValidate()
    {
        _inputField = gameObject.GetOrAdd<TMP_InputField>();
    }

    void OnEnable()
    {
        _inputField.onEndEdit.AddListener(OnEndEdit);
    }

    void OnDisable()
    {
        _inputField.onEndEdit.RemoveListener(OnEndEdit);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            _inputField.text = CommandExecutor.Instance.GetLastCommand();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            _inputField.text = CommandExecutor.Instance.GetNextCommand();
        }
    }

    void OnEndEdit(string text)
    {
        Debug.Log(text);
        Command command = ParseCommand(text);
        if (command != null)
        {
            // Do something with the command
            CommandExecutor.Instance.ExecuteCommand(command);
        }

        _inputField.text = "";
        _inputField.ActivateInputField();  
    }

    public Command ParseCommand(string input)
    {
        if (string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input) || input.Length < 2 ||
            !input.Contains("(") || !input.Contains(")") || input.IndexOf("(") > input.IndexOf(")"))
        {
            Debug.LogWarning("Invalid command");
            return null;
        }


        // Remove leading and trailing white spaces
        input = input.Trim();

        string[] parts = input.Split(new[] { '(', ')' }, StringSplitOptions.RemoveEmptyEntries);

        if (parts.Length == 0) 
        {
            Debug.LogWarning("Invalid command");
            return null;
        }

        CommandExecutor.Instance.AddCommandToHistory(input);   
        string commandName = parts[0];
        string[] args = new string[parts.Length - 1];

        if (parts.Length > 1)
        {
            args = parts[1].Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < args.Length; i++)
            {
                args[i] = args[i].Trim(); // Remove any whitespace around arguments
            }
        }

        var command = new Command(commandName, args);
        Debug.Log(command.ToString());
        return command;
    }
    
}
