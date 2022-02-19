//MS Breakfast analogy
//https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/async/
main().Wait();

async Task main()
{

    Task<string> textTask = SavePictureAsync();
    Task saveUserTeamRelationsTask;
    Task saveUserTask;
    //using (DbContext context = new DbContext())
    {
        saveUserTeamRelationsTask = SaveTeamRelationsAsync();
        saveUserTask = SaveUserAsync();
    }

    SomeTaskStarter();

    await Task.WhenAll(textTask, saveUserTeamRelationsTask, saveUserTask);
    WriteMessageWithColour("All tasks have finished!", ConsoleColor.Cyan);
    await SendUserCreatedNotificationAsync();
}

async Task SomeTaskStarter()
{
    await Task.Run(() => {
        Thread.Sleep(5000);
        WriteMessageWithColour("I'm written in another thread!", ConsoleColor.Magenta);
    });
}

async Task<string> SavePictureAsync()
{
    WriteMessageWithColour("Saving the picture...", ConsoleColor.White);
    WriteMessageWithColour("Spinning up really old hard drive...", ConsoleColor.White);
    await Task.Delay(2000);
    WriteMessageWithColour("Found file location...", ConsoleColor.White);
    await Task.Delay(1000);
    WriteMessageWithColour("Writing to drive...", ConsoleColor.White);
    await Task.Delay(1000);
    WriteMessageWithColour("Succesfully saved the picture!", ConsoleColor.White);
    return "/some/path/to/picture.png";
}

async Task SaveTeamRelationsAsync()
{
    WriteMessageWithColour("Saving team relations...", ConsoleColor.Red);
    WriteMessageWithColour("Contacting database...", ConsoleColor.Red);
    await Task.Delay(500);
    WriteMessageWithColour("Executing insert query...", ConsoleColor.Red);
    await Task.Delay(3000);
    WriteMessageWithColour("Succesfully saved team relations!", ConsoleColor.Red);
}

async Task SaveUserAsync()
{
    WriteMessageWithColour("Saving user...", ConsoleColor.Green);
    WriteMessageWithColour("Contacting database...", ConsoleColor.Green);
    await Task.Delay(500);
    WriteMessageWithColour("Executing insert query...", ConsoleColor.Green);
    await Task.Delay(1500);
    WriteMessageWithColour("Succesfully saved user!", ConsoleColor.Green);
}

async Task SendUserCreatedNotificationAsync()
{
    WriteMessageWithColour("Sending email notification...", ConsoleColor.Blue);
    WriteMessageWithColour("Contacting SMTP server...", ConsoleColor.Blue);
    await Task.Delay(1000);
    WriteMessageWithColour("Sending SMTP request...", ConsoleColor.Blue);
    await Task.Delay(2000);
    WriteMessageWithColour("Succesfully saved user!", ConsoleColor.Blue);
}

void WriteMessageWithColour(string message, ConsoleColor color)
{
    Console.ForegroundColor = color;
    Console.WriteLine(Thread.CurrentThread.ManagedThreadId + " - "  + message);
    Console.ForegroundColor = ConsoleColor.White;
}