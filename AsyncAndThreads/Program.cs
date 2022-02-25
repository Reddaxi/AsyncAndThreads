//MS Breakfast analogy
//https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/async/


//Async: You go to the store and buy pasta. Let me know when you get back, to make dinner together. Meanwhile, I’ll prepare sauce and drinks.

//Threads: You boil the water. I’ll heat the tomato sauce. While the water is boiling, ask me and I’ll put the pasta in. When the sauce is hot, you can add cheese.
//When both are done, I’ll sit down and you serve dinner. Then we eat.

//Multithreading is about workers, Asynchronous is about tasks
//https://www.baeldung.com/cs/async-vs-multi-threading


//Noter:

main().Wait();
// Async er "bare" et flag til compileren om at der bruges "await" i denne metode.
// Uden "await", gør "async" ikke noget.
async Task main()
{

    Task saveUserTeamRelationsTask;
    Task saveUserTask;
    Task saveUserPictureTask;
    //using (DbContext context = new DbContext())
    {
        saveUserTeamRelationsTask = SaveTeamRelationsAsync(); //4 sekunder
        saveUserTask = SaveUserAsync();                       //2 sekunder
        saveUserPictureTask = SavePictureAsync();             //5 sekunder
    }
    await saveUserTask;
    SomeTaskStarter(); //Do when save user is done, but you're still waiting for the other 2 tasks.

    await Task.WhenAll(saveUserPictureTask, saveUserTeamRelationsTask);
    WriteMessageWithColour("All tasks have finished!", ConsoleColor.Cyan);
    await SendUserCreatedNotificationAsync();
}

void SomeTaskStarter()
{
    WriteMessageWithColour("I'm spinning up another thread!", ConsoleColor.Magenta);
    Task.Run(() =>
    {
        WriteMessageWithColour("I'm written in another thread!", ConsoleColor.Magenta);
        Thread.Sleep(5000);
        WriteMessageWithColour("I'm also written in another thread!", ConsoleColor.Magenta);
    });
}

async Task SavePictureAsync()
{
    WriteMessageWithColour("Saving the picture...", ConsoleColor.White);
    WriteMessageWithColour("Spinning up really old hard drive...", ConsoleColor.White);
    await Task.Delay(2000);
    WriteMessageWithColour("Found file location...", ConsoleColor.White);
    await Task.Delay(1000);
    WriteMessageWithColour("Writing to drive...", ConsoleColor.White);
    await Task.Delay(2000);
    WriteMessageWithColour("Succesfully saved the picture!", ConsoleColor.White);
    return;
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
    Console.WriteLine(Thread.CurrentThread.ManagedThreadId + " - " + message);
    Console.ForegroundColor = ConsoleColor.White;
}