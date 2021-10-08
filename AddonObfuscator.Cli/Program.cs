using AddonObfuscator.Core;
using System;
using System.Diagnostics;

namespace AddonObfuscator.Cli
{
    internal class Program
    {
        private static void Main()
        {
            Console.Title = "Add-on Obfuscator";

            string? Input(string message)
            {
                Console.WriteLine(message);
                return Console.ReadLine();
            }

            var source = Input("Enter source folder path: ");
            var target = Input("Enter target folder path: ");
            var formatting = Input("Enter formatting type (0: Default, 1: Minify): ");

            if (string.IsNullOrEmpty(source) || string.IsNullOrEmpty(target) || string.IsNullOrEmpty(formatting))
            {
                Input("Invalid input.");
                return;
            }

            if (int.TryParse(formatting, out var format) && format is 0 or 1)
            {
                var obfuscator = new Obfuscator(source, target, (Formatting)format);
                var watch = new Stopwatch();
                watch.Start();
                obfuscator.Run();
                watch.Stop();

                Console.WriteLine($"Finished. Process took {watch.Elapsed}");
                Input("Press any key to exit...");
                return;
            }

            Input("Invalid input.");
        }
    }
}
