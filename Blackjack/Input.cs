using System;

public class Input
{

    public virtual ConsoleKeyInfo ReadKey()
    {
        return Console.ReadKey(true);
    }

    public virtual string? ReadLine()
    {
        return Console.ReadLine();
    }

}
