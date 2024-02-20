using System;
using System.IO;
using System.Linq;

namespace MinecraftDotGenerator
{
    /// <summary> Program class. </summary>
    internal static class Program
    {
        /// <summary> Defines the entry point of the application. </summary>
        /// <param name="args"> The arguments. </param>
        private static void Main(string[] args)
        {
            Console.WriteLine($"{DateTime.Now:yyyyMMdd HH:mm:ss.fff}");
            string src = args.FirstOrDefault();
            string dst = args.ElementAtOrDefault(1);

            if (!File.Exists(src))
            {
                Console.WriteLine("not found.");
                Environment.Exit(1);
            }

            if (string.IsNullOrWhiteSpace(dst))
            {
                Console.WriteLine("invalid args.");
                Environment.Exit(1);
            }

            byte[] data = File.ReadAllBytes(src);
            DirectoryInfo workDir = Directory.CreateDirectory(dst);

            MCGenerator.UsingFullColor = false;
            bool result = MCGenerator.Generate(workDir, data);
            Console.WriteLine($"result={result}");
            Console.WriteLine($"{DateTime.Now:yyyyMMdd HH:mm:ss.fff}");
            Environment.Exit(0);
        }
    }
}
