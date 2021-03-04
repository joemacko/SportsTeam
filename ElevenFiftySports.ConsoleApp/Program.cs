using ElevenFiftySports.Controllers;
using ElevenFiftySports.Models;
using ElevenFiftySports.Models.CustomerModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
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

        private async Task CustomerRegistration()
        {
            Console.Clear();

            Console.WriteLine("Enter your email address.");
            string email = Console.ReadLine();
            Dictionary<string, string> register = new Dictionary<string, string>()
            {
                {"Email", email }
            };

            Console.WriteLine("Enter your password.");
            register.Add("Password", Console.ReadLine());

            Console.WriteLine("Confirm your password.");
            register.Add("ConfirmPassword", Console.ReadLine());

            HttpClient client = new HttpClient();
            Console.WriteLine("Enter an id");
            //int id = int.Parse(Console.ReadLine());
            //string url = $"https://localhost:44332/api/Order/{id}"; //note: this is to be referenced for any id method [fromURI] 
            var registration = new HttpRequestMessage(HttpMethod.Post, $"https://localhost:44332/api/Account/Register");
            registration.Content = new FormUrlEncodedContent(register.AsEnumerable());
            var response = await client.SendAsync(registration);


            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Initial registration completed.");
            }
            else
            {
                Console.WriteLine($"Nope {response.StatusCode}");
                Thread.Sleep(2000); //This pauses the program for 500 milliseconds (this is a shortcut on the .wait());
                CustomerUI();
            }

            Thread.Sleep(1500); //This pauses the program for 500 milliseconds (this is a shortcut on the .wait());

            Login();

            CustomerController customerController = new CustomerController();
            Dictionary<string, string> customer = new Dictionary<string, string>(); //Question... dictionary won't work for this right?? I need to reference the models class to use those?? Because properties are not just string/string...
            //CustomerCreate customer = new CustomerCreate();
            //customer.CustomerId = Guid.Parse(User.Identity.GetUserId());//customerController.GetGuidHelper();
            //customer.Email = email;
                        customer.Add("Email", email);

            Console.WriteLine("Let's finish your customer registration now.\n" +
                "Enter your first name.");
            //customer.FirstName = Console.ReadLine();
            customer.Add("FirstName",Console.ReadLine());

            Console.WriteLine("Enter your last name.");
            //customer.LastName = Console.ReadLine();
            customer.Add("LastName",Console.ReadLine());

            Console.WriteLine("Enter your 10-digit cell phone number in the format XXX-XXX-XXXX");
            //customer.CellPhoneNumber = Console.ReadLine();
            customer.Add("CellPhoneNumber", Console.ReadLine());
            //customerController.Post(customer);
            HttpContent httpContent = new FormUrlEncodedContent(customer);
            //HttpClient httpClient = _httpClient;
            //httpClient.DefaultRequestHeaders.Authorization = _httpClient;
            Task<HttpResponseMessage> newResponse = _httpClient.PostAsync("https://localhost:44332/api/Customer", httpContent);

            if(newResponse.Result.IsSuccessStatusCode)
            {
                Console.WriteLine("Customer registration completed.");
            }
            else
            {
                Console.WriteLine($"Nope {response.StatusCode}");
                Thread.Sleep(2000); //This pauses the program for 500 milliseconds (this is a shortcut on the .wait());
                CustomerUI();
            }
        }




    }
    public class Tokens //this sets the format for the return of the token (to only pull the token value)
    {
        [JsonProperty("access_token")]
        public string Value { get; set; }

    }
}
