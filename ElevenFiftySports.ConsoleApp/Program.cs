using ElevenFiftySports.Controllers;
using ElevenFiftySports.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace ElevenFiftySports.ConsoleApp
{
    class Program
    {
        //static void Main(string[] args)
        //{
        //}

        private void UIMenu()
        {
            bool keepRunning = true;
            while (keepRunning)
            {
                Console.WriteLine("Enter the number associated with the menu item below:\n" +
                    "1. Open Customer Interface\n" +
                    "2. Exit Application\n");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        CustomerUI();
                        break;
                    case "2":
                        Console.WriteLine("Thank you for using ElevenFiftySports!");
                        keepRunning = false;
                        break;
                    default:
                        Console.WriteLine("Please enter a valid number.");
                        break;
                }

                Console.WriteLine("Press any key to continue.");
                Console.ReadKey();
                Console.Clear();
            }
        }

        private void CustomerUI()
        {
            Console.Clear();
            bool keepRunning = true;
            while (keepRunning)
            {
                Console.WriteLine("Enter the number associated with the menu item below:\n" +
                                    "1. Customer Login\n" +
                                    "2. Customer Registrations\n" +
                                    "3. Return to Main Menu\n" +
                                    "4. Exit application");

                string input = Console.ReadLine();

                switch(input)
                {
                    case "1":
                            CustomerLogin();
                        break;
                    case "2":
                            CustomerRegistration();
                        break;
                    case "3":
                        UIMenu();
                        break;
                    case "4":
                        Console.WriteLine("Thank you for using ElevenFiftySports!");
                        keepRunning = false;
                        break;
                    default:
                        Console.WriteLine("Please enter a valid number.");
                        break;
                }
            }
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
            Console.Clear();
        }

        private void CustomerLogin()
        {
            Console.WriteLine("Please enter your email address.");
            string username = Console.ReadLine();

            Console.WriteLine("Please enter your password.");
            string password = Console.ReadLine();


        }

        private async Task CustomerRegistration()
        {
            Console.Clear();

            RegisterBindingModel model = new RegisterBindingModel();

            Console.WriteLine("Enter your email address.");
            model.Email = Console.ReadLine();

            Console.WriteLine("Enter your password.");
            model.Password = Console.ReadLine();

            Console.WriteLine("Confirm your password.");
            model.ConfirmPassword = Console.ReadLine();
            
            //Below logic is already built into account controller (COMPARE)
            //while(!(model.ConfirmPassword==model.Password))
            //{
            //    Console.WriteLine("Your passwords do not match. Please re-enter your password.");
            //    model.Password = Console.ReadLine();

            //    Console.WriteLine("Confirm your password.");
            //    model.ConfirmPassword = Console.ReadLine();
            //}

            //AccountController accountController = new AccountController();

            //await accountController.Register(model);

            //HttpClient and request messages


        }
    }
}
