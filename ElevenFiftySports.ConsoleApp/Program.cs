using ElevenFiftySports.Controllers;
using ElevenFiftySports.Models;
using ElevenFiftySports.Models.CustomerModels;
using ElevenFiftySports.Models.Products;
using ElevenFiftySports.Models.SpecialModels;
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
        public int _currentOrderId = new int();

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
                    "2. Open Employee Interface\n" +
                    "3. Exit Application\n");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        CustomerAuthorizationMenu();
                        break;
                    case "2":
                        EmployeeAuthorizationMenu();
                        break;
                    case "3":
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

        private void EmployeeAuthorizationMenu()
        {
            Console.WriteLine("Sorry, the employee interface will be developed at a later date. Please work with your manager and IT to perform administrative tasks until further notice.");
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
                                    "3. Return to the Previous Menu\n" +
                                    "4. Exit application");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        Login().Wait();
                        break;
                    case "2":
                        CustomerRegistration().Wait();//Wait causes the async to complete before returning 
                        break;
                    case "3":
                        UIMenu();
                        break;
                    case "4":
                        Console.WriteLine("Thank you for using ElevenFiftySports!");
                        Thread.Sleep(1500);
                        Environment.Exit(-1);
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
            //_keepRunning = false;
            //UIMenu();
        }

        private async Task Login()
        {
            Console.Clear();
            Console.WriteLine("CUSTOMER LOGIN:\n" +
                "Enter your email address.");
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
                Thread.Sleep(2000);
                CustomerMenu();
            }
            else { Console.WriteLine($"Login failed. {tokenResponse.StatusCode}"); }
        }

        private async Task CustomerRegistration()
        {
            Console.Clear();

            Console.WriteLine("CUSTOMER REGISTRATION:\n" +
                "Enter your email address.");
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
                                    "1. Customer Profile\n" +
                                    "2. Food and Beverage Ordering\n" +
                                    "3. Logout and Exit application");

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
                        Thread.Sleep(1500);
                        Environment.Exit(-1);
                        //keepRunning = false;
                        break;
                    default:
                        Console.WriteLine("Please enter a valid number.");
                        break;
                }
            }
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
            Console.Clear();
            //UIMenu();
        }

        private async Task CustomerProfileMenu()
        {
            Console.Clear();
            bool keepRunning = true;
            while (keepRunning)
            {
                Console.WriteLine("Welcome to the Customer Profile Menu. Enter the number associated with the menu item below:\n" +
                                    "1. View Your Previous Orders\n" +
                                    "2. View Customer Profile\n" +
                                    "3. Update Your Customer Information\n" +
                                    "4. Return to the Previous Menu.\n" +
                                    "5. Logout and Exit application");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        GetOrders().Wait(); //The get method already filters by current customer.
                        break;
                    case "2":
                        ViewCustomer().Wait();
                        break;
                    case "3":
                        UpdateCustomer().Wait();
                        break;
                    case "4":
                        CustomerMenu();
                        break;
                    case "5":
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

        private async Task GetOrders()
        {
            Console.Clear();
            Task<HttpResponseMessage> getOrderResponse = _httpClient.GetAsync("https://localhost:44332/api/Order");
            //var ordersString = await getOrderResponse.Result.Content.ReadAsStringAsync();

            if (getOrderResponse.Result.IsSuccessStatusCode)
            {
                List<OrderListItem> orderList = _httpClient.GetAsync("https://localhost:44332/api/Order").Result.Content.ReadAsAsync<List<OrderListItem>>().Result;

                foreach (var order in orderList)
                {
                    string orderproducts = "\n";

                    foreach (var orderProduct in order.OrderProducts)
                    {
                        orderproducts = orderproducts + orderProduct.ProductCount + " X " + orderProduct.ProductName + "\n";
                    }

                    Console.WriteLine($"\n" +
                        $"Order Id: {order.OrderId}\n" +
                        $"Created Date: {order.CreatedOrderDate}\n" +
                        $"Products: {orderproducts} Total Cost: {order.Cost.ToString("C")}\n\n");
                }
                Console.WriteLine("Press any key to continue.");
                Console.ReadKey();
                Console.Clear();
            }
            else
            {
                Console.WriteLine($"Something is wrong... Please try again. {getOrderResponse.Result.StatusCode}");
                Thread.Sleep(2000);
                //Environment.Exit(-1);
            }
        }

        private async Task ViewCustomer()
        {
            Console.Clear();
            Task<HttpResponseMessage> getCustomerResponse = _httpClient.GetAsync("https://localhost:44332/api/Customer/GetLoggedInCustomer");

            if (getCustomerResponse.Result.IsSuccessStatusCode)
            {
                CustomerDetail customer = _httpClient.GetAsync("https://localhost:44332/api/Customer/GetLoggedInCustomer").Result.Content.ReadAsAsync<CustomerDetail>().Result;

                Console.WriteLine($"\n" +
                    //$"Customer Id: {customer.CustomerId}\n" +
                    $"First Name: {customer.FirstName}\n" +
                    $"Last Name: {customer.LastName}\n" +
                    $"Email: {customer.Email}\n" +
                    $"Cellphone Number: {customer.CellPhoneNumber}\n" +
                    $"Customer Since: {customer.CreatedUtc}\n");

                Console.WriteLine("Press any key to continue.");
                Console.ReadKey();
                Console.Clear();

            }
            else
            {
                Console.WriteLine($"Something is wrong... Please try again. {getCustomerResponse.Result.StatusCode}");
                Thread.Sleep(2000);
            }
        }

        private async Task UpdateCustomer()
        {
            ViewCustomer();
            Dictionary<string, string> updatedCustomer = new Dictionary<string, string>();

            CustomerDetail originalCustomer = _httpClient.GetAsync("https://localhost:44332/api/Customer/GetLoggedInCustomer").Result.Content.ReadAsAsync<CustomerDetail>().Result;

            updatedCustomer.Add("CustomerId", originalCustomer.CustomerId.ToString());
            updatedCustomer.Add("Email", originalCustomer.Email);

            Console.WriteLine("Enter your first name.");
            updatedCustomer.Add("FirstName", Console.ReadLine());

            Console.WriteLine("Enter your last name.");
            updatedCustomer.Add("LastName", Console.ReadLine());

            Console.WriteLine("Enter your 10-digit cell phone number in the format XXX-XXX-XXXX");
            updatedCustomer.Add("CellPhoneNumber", Console.ReadLine());

            HttpContent httpContent = new FormUrlEncodedContent(updatedCustomer);
            Task<HttpResponseMessage> updateResponse = _httpClient.PutAsync("https://localhost:44332/api/Customer", httpContent);

            if (updateResponse.Result.IsSuccessStatusCode)
            {
                Console.WriteLine("Your customer profile has been updated!");
            }
            else
            {
                Console.WriteLine($"Something is wrong... Please try again. {updateResponse.Result.StatusCode}");
                Thread.Sleep(2000);
            }
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
                Console.WriteLine($"Something is wrong... The application will end now. {createOrderResponse.Result.StatusCode}");
                Thread.Sleep(2000);
                Environment.Exit(-1);
            }
            var orderIdInfo = new HttpRequestMessage(HttpMethod.Get, "https://localhost:44332/api/Order/MostRecent");
            var orderIdResponse = await _httpClient.SendAsync(orderIdInfo);
            var currentOrderString = await orderIdResponse.Content.ReadAsStringAsync();
            var currentOrderId = JsonConvert.DeserializeObject<CurrentOrderId>(currentOrderString).Value;
            _currentOrderId = int.Parse(currentOrderId);

            Console.WriteLine($"Your current order ID is {_currentOrderId}.");
            Thread.Sleep(2500);


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
                        GetTodaysSpecials().Wait();
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
                        FinalizeOrder().Wait();
                        break;
                    case "7":
                        DeleteOrder().Wait();
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

        private async Task GetProducts()
        {

            Console.WriteLine("Today's Specials:");
            GetTodaysSpecials();
            Console.WriteLine("\nFull Menu:");
            Task<HttpResponseMessage> getProductResponse = _httpClient.GetAsync("https://localhost:44332/api/Product");

            if (getProductResponse.Result.IsSuccessStatusCode)
            {
                List<ProductListItem> productList = _httpClient.GetAsync("https://localhost:44332/api/Product").Result.Content.ReadAsAsync<List<ProductListItem>>().Result;

                string tableHeaders = String.Format("{0,-20} {1,-40} {2,-60}", "Product ID:", "Product:", "Cost:");
                Console.WriteLine(tableHeaders);

                foreach (var product in productList)
                {
                    string tableBody = String.Format("{0,-20} {1,-40}, {2,-60}", product.ProductId, product.ProductName, product.ProductPrice.ToString("C"));
                    Console.WriteLine(tableBody);
                }
                Thread.Sleep(2000);
            }
            else
            {
                Console.WriteLine($"Something is wrong... Please try again. {getProductResponse.Result.StatusCode}");
                Thread.Sleep(2000);
            }
        }

        private async Task GetTodaysSpecials()
        {
            var dayOfWeek = DateTime.Now.DayOfWeek;
            Task<HttpResponseMessage> getSpecialResponse = _httpClient.GetAsync($"https://localhost:44332/api/Special/{dayOfWeek}");

            if (getSpecialResponse.Result.IsSuccessStatusCode)
            {
                List<SpecialListItem> specialList = _httpClient.GetAsync($"https://localhost:44332/api/Special/{dayOfWeek}").Result.Content.ReadAsAsync<List<SpecialListItem>>().Result;

                string tableHeaders = String.Format("{0,-20} {1,-40} {2,-60}", "Product ID:", "Product:", "Cost:");
                Console.WriteLine(tableHeaders);

                foreach (var special in specialList)
                {
                    string tableBody = String.Format("{0,-20} {1,-40}, {2,-60}", special.ProductId, special.ProductName, special.ProductSpecialPrice.ToString("C"));
                    Console.WriteLine(tableBody);
                }
                Thread.Sleep(2000);
            }
            else
            {
                Console.WriteLine($"Something is wrong... Please try again. {getSpecialResponse.Result.StatusCode}");
                Thread.Sleep(2000);
            }
        }

        private async Task CreateOrderProduct()
        {
            GetProducts();
            Console.WriteLine("Enter the product ID of the product you'd like to add to your order. This must be a single whole number.");
            Dictionary<string, string> orderProduct = new Dictionary<string, string>();

            orderProduct.Add("OrderId", _currentOrderId.ToString());
            orderProduct.Add("ProductId", Console.ReadLine());
            Console.WriteLine("How many would you like to add to the order? Enter a whole number.");
            orderProduct.Add("ProductCount", Console.ReadLine());

            HttpContent httpContent = new FormUrlEncodedContent(orderProduct);
            Task<HttpResponseMessage> orderProductResponse = _httpClient.PostAsync("https://localhost:44332/api/OrderProduct", httpContent);

            if (orderProductResponse.Result.IsSuccessStatusCode)
            {
                Console.WriteLine("Your request has been added to your order!");
            }
            else
            {
                Console.WriteLine($"Please restart customer registration process. {orderProductResponse.Result.StatusCode}");
                Thread.Sleep(2000);
                CustomerAuthorizationMenu();
            }
        }

        private async Task ViewCurrentOrder()
        {
            Console.Clear();
            Task<HttpResponseMessage> getCurrentOrderResponse = _httpClient.GetAsync($"https://localhost:44332/api/Order/{_currentOrderId}");

            if (getCurrentOrderResponse.Result.IsSuccessStatusCode)
            {
                OrderListItem order = _httpClient.GetAsync($"https://localhost:44332/api/Order/{_currentOrderId}").Result.Content.ReadAsAsync<OrderListItem>().Result;

                string orderproducts = " ";

                foreach (var orderProduct in order.OrderProducts)
                {
                    orderproducts = orderproducts + orderProduct.ProductCount + " X " + orderProduct.ProductName + "\n";
                }

                Console.WriteLine($"\n" +
                    $"Order Id: {order.OrderId}\n" +
                    $"Created Date: {order.CreatedOrderDate}\n" +
                    $"Products: {orderproducts} Total Cost: {order.Cost.ToString("C")}\n\n");
                Thread.Sleep(2000);
            }
            else
            {
                Console.WriteLine($"Something is wrong... Please try again. {getCurrentOrderResponse.Result.StatusCode}");
                Thread.Sleep(2000);
            }
        }

        private async Task UpdateOrderProduct() //BUILD (must include order get (to display all OPs), OP update and OP delete functionality)
        {

        }

        private async Task FinalizeOrder()
        {
            Console.Clear();
            ViewCurrentOrder();

            Console.WriteLine("Are you ready to finalize this order?\n" +
                "1. Yes\n" +
                "2. No\n");

            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    FinalizeOrderHelper().Wait();
                    break;
                case "2":
                    Console.WriteLine("You will be returned to the previous menu.");
                    Thread.Sleep(2000);
                    break;
                default:
                    Console.WriteLine("You didn't enter a valid number. You will be returned to the previous menu.");
                    Thread.Sleep(2000);
                    break;
            }
        }

        private async Task FinalizeOrderHelper()
        {
            Console.Clear();

            //StringContent content = new StringContent("empty");

            Task<HttpResponseMessage> getFinalizeResponse = _httpClient.PutAsync($"https://localhost:44332/api/Order/{_currentOrderId}", null);//content);
            //var finalizeResponseString = await getFinalizeResponse.Result.Content.ReadAsStringAsync();

            if (getFinalizeResponse.Result.IsSuccessStatusCode)
            {
                Console.WriteLine("Your order has been finalized! See the finalized order below. It will be coming out to you shortly. Please contact your waiter with any questions, comments or concerns. Thank you for using ElevenFiftySports!\n");
                //ViewCurrentOrder();
                Thread.Sleep(5000);
                Environment.Exit(-1);
            }
            else
            {
                Console.WriteLine($"There was an issue... {getFinalizeResponse.Result.StatusCode}");//{finalizeResponseString}.");//{getFinalizeResponse.Result.StatusCode}");
                Thread.Sleep(2000);
            }

        }

        private async Task DeleteOrder()
        {
            Console.Clear();
            var deleteResponse = await _httpClient.DeleteAsync($"https://localhost:44332/api/Order/{ _currentOrderId}");
            var deleteResponseString = await deleteResponse.Content.ReadAsStringAsync();

            Console.WriteLine($"{deleteResponseString}");
            Thread.Sleep(2500);

            Console.WriteLine("Thank you for using ElevenFiftySports! Please come again.");
            Thread.Sleep(1500);
            Environment.Exit(-1);
        }
    }
}
public class Token //this sets the format for the return of the token (to only pull the token value)
{
    [JsonProperty("access_token")]
    public string Value { get; set; }

}
public class CurrentOrderId
{
    [JsonProperty("OrderId")]
    public string Value { get; set; }

}

