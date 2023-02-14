abstract class Creator
{
    protected Random Rand;
    public Creator() =>
        Rand = new Random();

    public Field Init() =>
        new Field(RandomBackground(), RandomForeground());

    public abstract ConsoleColor RandomForeground();
    public abstract ConsoleColor RandomBackground();
}