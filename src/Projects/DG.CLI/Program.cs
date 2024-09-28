namespace DG
{
    internal static class Program
    {
        private static void Main()
        {
            Startup game = new();
            game.Start();
            game.Update();
            game.End();
        }
    }
}
