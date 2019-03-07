namespace P01_HospitalDatabase.AppStructure.Models
{
    using System;

    public class MyConsole
    {
        public MyConsole(ConsoleColor consoleColor, ConsoleColor fontColor, string title)
        {
            this.ConsoleColor = consoleColor;
            this.FontColor = fontColor;
            this.Title = title;

            this.GenerateConsole();
        }

        private ConsoleColor ConsoleColor { get; set; }

        private ConsoleColor FontColor { get; set; }

        private string Title { get; set; }

        private void GenerateConsole()
        {
            Console.BackgroundColor = ConsoleColor;
            Console.Clear();
            Console.ForegroundColor = FontColor;
            Console.Title = Title;
        }
    }
}
