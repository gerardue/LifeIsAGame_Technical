using System.Collections.Generic;
using System.Threading.Tasks;

/// <summary>
/// This class is responsible for handling Commands
/// </summary>
public class CommandQueue
{
    public static CommandQueue Instance => instance ?? (instance = new CommandQueue());

    private readonly Queue<ICommand> commandsToExecute;
    private bool isRunningCommand;
    private static CommandQueue instance;

    private CommandQueue()
    {
        commandsToExecute = new Queue<ICommand>();
        isRunningCommand = false;
    }

    private void AddCommand(ICommand commandToEnqueue)
    {
        commandsToExecute.Enqueue(commandToEnqueue);
        RunNextCommand().WrapErrors(); 
    }

    /// <summary>
    /// Run a List of commands
    /// </summary>
    public void ExecuteCommands(List<ICommand> commands)
    {
        foreach (ICommand command in commands)
            AddCommand(command); 
    }

    /// <summary>
    /// Run one command
    /// </summary>
    public void ExecuteCommand(ICommand command)
    {
        AddCommand(command); 
    }

    //Responsible for run commands
    private async Task RunNextCommand()
    {
        if (isRunningCommand) return;

        while (commandsToExecute.Count > 0)
        {
            isRunningCommand = true;
            var commandToExecute = commandsToExecute.Dequeue();
            await commandToExecute.ExecuteAsync();
        }

        isRunningCommand = false;
    }
}
