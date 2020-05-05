using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace TestAPI
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
        static async Task<TodoItem> GetProductAsync(string path)
        {
            TodoItem product = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                product = await response.Content.ReadAs
            }
            return product;
        }
        static async Task<TodoItem> GetTodoItem(long id)
        {
            TodoItem todoItem = null;
            HttpResponseMessage response = await client.GetAsync("https://demo01-webapi.herokuapp.com/api/TodoItems");
            if (response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();
                Console.WriteLine(result);
            }
            return todoItem;
        }

        static void Main(string[] args)
        {

        }
    }
}
