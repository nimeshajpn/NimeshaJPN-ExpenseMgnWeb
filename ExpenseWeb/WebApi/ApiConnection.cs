namespace ExpenseWeb.WebApi
{
    public class ApiConnection
    {
        static async Task Main(string[] args)
        {

            using (System.Net.Http.HttpClient client = new System.Net.Http.HttpClient())
            {

                var Get = await client.GetAsync("https://localhost:44358/Expense/");
                
            }

              
        }        


    }
}
