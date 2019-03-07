namespace P01_HospitalDatabase.AppStructure.Models
{
    using System;

    public class Menu 
    {
        public void PrintMenu()
        {
            Console.WriteLine("1. Show me a patient.");
            Console.WriteLine("2. Add patient.");
            Console.WriteLine("3. Add visitation to a patient.");
            Console.WriteLine("4. Add diagnose to a patient.");
            Console.WriteLine("5. Add medicaments to a patient.");
            Console.WriteLine("6. Exit.");
            Console.Write("Please enter command (1-6): ");
        }

        public void Menu1()
        {
            Console.WriteLine(new string('-', 30));
            Console.Write("Enter patient: ");
        }

        public void Menu2()
        {

        }

        public void Menu3()
        {

        }

        public void Menu4()
        {

        }

        public void Menu5()
        {

        }

        public void Menu6()
        {
            Console.WriteLine("Goodbye!");
        }
    }
}
