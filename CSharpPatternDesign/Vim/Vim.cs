class Vim : Creator
{
    public override ConsoleColor RandomBackground() =>
        (ConsoleColor)Rand.Next(0, 10);

    public override ConsoleColor RandomForeground() =>
        (ConsoleColor)Rand.Next(0, 10);
}