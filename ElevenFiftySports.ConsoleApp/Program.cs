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
        private static readonly int _currentOrderId = new int();


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
                        CustomerAuthorizationMenu();
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

        private void CustomerAuthorizationMenu()
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

        private async Task Login()
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

            var tokenObject = JsonConvert.DeserializeObject<Token>(tokenString).Value;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenObject);

            if (tokenResponse.IsSuccessStatusCode)
            {
                Console.WriteLine("You're logged in.");
                CustomerMenu();
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
            var registration = new HttpRequestMessage(HttpMethod.Post, $"https://localhost:44332/api/Account/Register");
            registration.Content = new FormUrlEncodedContent(register.AsEnumerable());
            var response = await client.SendAsync(registration);


            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Initial registration completed.");
            }
            else
            {
                Console.WriteLine($"{response.StatusCode}. Please restart customer registration process.");
                Thread.Sleep(2000);
                CustomerAuthorizationMenu();
            }

            Thread.Sleep(1500);

            Console.WriteLine("Now, you are being logged into the application to complete your registration.\n"
                );
            Dictionary<string, string> token = new Dictionary<string, string>()
            {
                {"grant_type", "password" }
            };
            token.Add("username", register["Email"]);

            token.Add("password", register["Password"]);

            var tokenInfo = new HttpRequestMessage(HttpMethod.Post, "https://localhost:44332/token");
            tokenInfo.Content = new FormUrlEncodedContent(token.AsEnumerable());
            var tokenResponse = await _httpClient.SendAsync(tokenInfo);
            var tokenString = await tokenResponse.Content.ReadAsStringAsync();

            var tokenObject = JsonConvert.DeserializeObject<Token>(tokenString).Value;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenObject);

            if (tokenResponse.IsSuccessStatusCode)
            {
                Console.WriteLine("You're logged in.");
            }
            else
            {
                Console.WriteLine("Login failed. Please restart customer registration process."); //***EVENTUALLY WILL NEED TO FIGURE OUT HOW TO FINISH THIS.
                Thread.Sleep(2500);
                CustomerAuthorizationMenu();
            }

            CustomerController customerController = new CustomerController();
            Dictionary<string, string> customer = new Dictionary<string, string>();
            customer.Add("Email", email);

            Console.WriteLine("Let's finish your customer registration now.\n" +
                "Enter your first name.");
            customer.Add("FirstName", Console.ReadLine());

            Console.WriteLine("Enter your last name.");
            customer.Add("LastName", Console.ReadLine());

            Console.WriteLine("Enter your 10-digit cell phone number in the format XXX-XXX-XXXX");
            customer.Add("CellPhoneNumber", Console.ReadLine());

            HttpContent httpContent = new FormUrlEncodedContent(customer);
            Task<HttpResponseMessage> newResponse = _httpClient.PostAsync("https://localhost:44332/api/Customer", httpContent);

            if (newResponse.Result.IsSuccessStatusCode)
            {
                Console.WriteLine("Customer registration completed.");
                CustomerMenu();
            }
            else
            {
                Console.WriteLine($"Customer registration failed. Please restart customer registration process. {newResponse.Result.StatusCode}");
                Thread.Sleep(2000);
                CustomerAuthorizationMenu();
            }
        }
        private async Task CustomerMenu()
        {
            Console.Clear();
            bool keepRunning = true;
            while (keepRunning)
            {
                Console.WriteLine("Welcome to the Customer Menu. Enter the number associated with the menu item below:\n" +
                                    "1. View Customer Profile\n" +
                                    "2. Proceed to Food and Beverage Ordering\n" +
                                    "3. Exit application");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        CustomerProfileMenu().Wait();
                        break;
                    case "2":
                        OrderingMenu().Wait();//Wait causes the async to complete before returning to this method
                        break;
                    case "3":
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

        private async Task CustomerProfileMenu()
        {
            Console.Clear();
            bool keepRunning = true;
            while (keepRunning)
            {
                Console.WriteLine("Welcome to the Customer Profile Menu. Enter the number associated with the menu item below:\n" +
                                    "1. View Your Previous Orders\n" +
                                    "2. Update Your Customer Information\n" +
                                    "3. Return to the Previous Menu.\n" +
                                    "4. Logout and Exit application");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        GetOrders().Wait(); //The get method already filters by current customer.
                        break;
                    case "2":
                        UpdateCustomer().Wait();
                        break;
                    case "3":
                        CustomerMenu();
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

        private async Task GetOrders() //START HERE
        {

        }

        private async Task UpdateCustomer() //Need functional customer endpoints
        {

        }

        private async Task OrderingMenu()
        {
            Console.Clear();
            Console.WriteLine("Let's get an order started for you.");
            Task<HttpResponseMessage> createOrderResponse = _httpClient.PostAsync("https://localhost:44332/api/Order", null);

            if (createOrderResponse.Result.IsSuccessStatusCode)
            {
                Console.WriteLine("You are ready to add items to your order.");
            }
            else
            {
                Console.WriteLine($"Something is wrong... Please exit the application and try again. {createOrderResponse.Result.StatusCode}");
                Thread.Sleep(2000);
                UIMenu();
            }

            bool keepRunning = true;
            while (keepRunning)
            {
                Console.WriteLine("Enter the number associated with the menu item below:\n" +
                                    "1. View All Products\n" +
                                    "2. View Today's Specials\n" +
                                    "3. Add Item(s) to Your Order\n" +
                                    "4. View All Item(s) on Your Order\n" +
                                    "5. Update Item(s) on Your Order\n" +
                                    "6. Complete Your Order.\n" +
                                    "7. Discard Order and Exit the Application");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        GetProducts().Wait();
                        break;
                    case "2":
                        GetTodaysSpecials().Wait();//Wait causes the async to complete before returning to CustomerUIS
                        break;
                    case "3":
                        CreateOrderProduct().Wait();
                        break;
                    case "4":
                        ViewCurrentOrder().Wait();
                        break;
                    case "5":
                        UpdateOrderProduct().Wait();
                        break;
                    case "6":
                        SubmitOrderTotalCost().Wait(); //REQUIRES BUILD OUT OF SUPER SIMPLE ORDERUPDATE METHOD THAT ONLY SETS TOTALCOST.
                        Console.WriteLine("Your order has been submitted and will be arriving soon. Please contact your waiter with any questions, comments or concerns. Thank you for using ElevenFiftySports!");
                        keepRunning = false;
                        break;
                    case "7":
                        DeleteOrder().Wait();
                        Console.WriteLine("Thank you for using ElevenFiftySports! Please come again.");
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

        private async Task GetProducts() //Need functional product endpoints
        {

        }

        private async Task GetTodaysSpecials() //Need functional productspecial endpoints
        {

        }

        private async Task CreateOrderProduct() //BUILD
        {

        }

        private async Task ViewCurrentOrder() //BUILD
        {

        }

        private async Task UpdateOrderProduct() //BUILD (must include order get (to display all OPs), OP update and OP delete functionality)
        {

        }

        private async Task SubmitOrderTotalCost() //Need order update method to create (SIMPLE JUST DOES THE MATH FOR THE TOTALCOSTS AND SETS EVERYTHING ELSE THE SAME)
        {

        }

        private async Task DeleteOrder() //BUILD
        {

        }
    }
}
public class Token //this sets the format for the return of the token (to only pull the token value)
{
    [JsonProperty("access_token")]
    public string Value { get; set; }

}
public class CurrentOrderId //this sets the format for the return of the token (to only pull the token value)
{
    [JsonProperty("OrderId")]
    public string Value { get; set; }

}

    //NOTES
    //Console.WriteLine("Enter an id");
    //int id = int.Parse(Console.ReadLine());
    //string url = $"https://localhost:44332/api/Order/{id}"; //note: this is to be referenced for any id method [fromURI] 
    //var registration = new HttpRequestMessage(HttpMethod.Post, $"https://localhost:44332/api/Account/Register/{id}");

