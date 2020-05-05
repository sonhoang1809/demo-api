using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Nancy.Json;

namespace TestAPIDemo01
{
    public class TodoItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
    }

    class Program
    {
        static HttpClient client = new HttpClient();

        static void ShowTodoItem(TodoItem todoItem)
        {
            Console.WriteLine("ID: "+todoItem.Id + "Name: "+todoItem.Name+" IsComplete: "+todoItem.IsComplete);
        }

        static async Task<List<TodoItem>> GetTodoItems()
        {
            List<TodoItem> listResult = null;
            HttpResponseMessage response = await client.GetAsync("api/TodoItems/");
            if (response.IsSuccessStatusCode)
            {
                listResult = await response.Content.ReadAsAsync<List<TodoItem>>();
                var json = new JavaScriptSerializer();
                Console.WriteLine(json.Deserialize<List<TodoItem>>(await response.Content.ReadAsStringAsync()));
                foreach (TodoItem x in listResult)
                {
                    ShowTodoItem(x);
                }
            }
            return listResult;
        }
        static async Task<TodoItem> GetTodoItem(long id)
        {
            TodoItem todoItem = null;
            HttpResponseMessage response = await client.GetAsync("api/TodoItems/"+id);
            if (response.IsSuccessStatusCode)
            {
                todoItem = await response.Content.ReadAsAsync<TodoItem>();
                var json = new JavaScriptSerializer();
                Console.WriteLine(json.Deserialize<TodoItem>(await response.Content.ReadAsStringAsync()));
                ShowTodoItem(todoItem);
            }
            return todoItem;
        }
        static async Task CreateProductAsync(TodoItem product)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(
                "api/TodoItems", product);
            response.EnsureSuccessStatusCode();
            Console.WriteLine(response.Headers.Location);            
        }
        static void Main(string[] args)
        {
            string requestUri = "https://demo01-webapi.herokuapp.com/weatherforecast";
            string data = GetResponse(requestUri);
            Console.WriteLine(data);
            RunAPI().GetAwaiter().GetResult();

        }
        static async Task RunAPI()
        {
            client.BaseAddress = new Uri("https://demo01-webapi.herokuapp.com/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            try
            {
               // TodoItem item = new TodoItem
               // {
               //     Name = "demo 1",
               //     IsComplete = true
               // };
               // TodoItem item2 = new TodoItem
               // {
               //     Name = "demo 2",
               //     IsComplete = false
               // };
               // TodoItem item3 = new TodoItem
               // {
               //     Name = "demo 3",
               //     IsComplete = false
               // };
               // TodoItem item4 = new TodoItem
                //{
               //     Name = "demo 4",
               //     IsComplete = true
               // };
               // await CreateProductAsync(item);
               // await CreateProductAsync(item2);
               // await CreateProductAsync(item3);
               // await CreateProductAsync(item4);
               
                Console.WriteLine("get by 1");
                await GetTodoItem(1);
                await GetTodoItem(2);
                await GetTodoItem(3);
                await GetTodoItem(4);
                Console.WriteLine("get all");
                await GetTodoItems();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        
        static string GetResponse(string url)
        {
            WebRequest wr = WebRequest.Create(url);
            
            WebResponse ws = wr.GetResponse();
            Stream st = ws.GetResponseStream();
            StreamReader sr = new StreamReader(st);
            string str = sr.ReadToEnd();
            return str;
        }
    }
}
