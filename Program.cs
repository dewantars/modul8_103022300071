using modul8_103022300071;
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        UIConfig config = new UIConfig();
        config.start();
        config.biaya_transfer();
        config.pilih_metode();
    }
}
