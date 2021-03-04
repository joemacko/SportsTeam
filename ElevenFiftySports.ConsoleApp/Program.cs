using ElevenFiftySports.Controllers;
using ElevenFiftySports.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace ElevenFiftySports.ConsoleApp
{
    class Program
    {
        private static readonly HttpClient _httpClient = new HttpClient();

        static void Main(string[] args)
        {
            Program program = new Program();
            program.UIMenu();
        }

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
                                    "2. Customer Registration\n" +
                                    "3. Return to Main Menu\n" +
                                    "4. Exit application");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        Login().Wait();
                        break;
                    case "2":
                        CustomerRegistration().Wait();//Wait causes the async to complete before returning to CustomerUIS
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

        private async Task CustomerRegistration()
        {
            Console.Clear();

            Console.WriteLine("Enter your email address.");
            Dictionary<string, string> register = new Dictionary<string, string>()
            {
                {"Email", Console.ReadLine() }
            };

            Console.WriteLine("Enter your password.");
            register.Add("Password", Console.ReadLine());

            Console.WriteLine("Confirm your password.");
            register.Add("ConfirmPassword", Console.ReadLine());

            HttpClient client = new HttpClient();

            var registration = new HttpRequestMessage(HttpMethod.Post, "https://localhost:44332/api/Account/Register");
            registration.Content = new FormUrlEncodedContent(register.AsEnumerable());
            var response = await client.SendAsync(registration);


            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Initial registration completed.");
            }
            else
            {
                Console.WriteLine($"Nope {response.StatusCode}");
            }

            //Login();

            Console.WriteLine();
        }

        private static async Task Login()
        {
            Console.WriteLine("Enter your email address.");
            Dictionary<string, string> token = new Dictionary<string, string>()
            {
                {"grant_type", "password" }
            };
            token.Add("username", Console.ReadLine());

            Console.WriteLine("Enter your password");
            token.Add("password", Console.ReadLine());

            var tokenInfo = new HttpRequestMessage(HttpMethod.Post, "https://localhost:44332/token");
            tokenInfo.Content = new FormUrlEncodedContent(token.AsEnumerable());
            var tokenResponse = await _httpClient.SendAsync(tokenInfo);
            var tokenString = await tokenResponse.Content.ReadAsStringAsync();

            var tokenObject = JsonConvert.DeserializeObject<Tokens>(tokenString).Value;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenObject);

            if (tokenResponse.IsSuccessStatusCode)
            {
                Console.WriteLine("You're logged in.");
            }
            else { Console.WriteLine("Login failed."); }
        }


    }
    public class Tokens
    {
        [JsonProperty("access_token")]
        public string Value { get; set; }

    }
}
