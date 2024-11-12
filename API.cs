using System.ComponentModel;
using System.Net;
using System.Net.Http.Headers;
using System.Runtime.InteropServices.ComTypes;
using CSharpQuoteAPI;

public class DataObject
{
    public string? q;
    public string? a;
}
public class QuoteAPI
{
    private const string url = "https://zenquotes.io/api/";
    const string requestUri = "quotes/";

    public QuoteAPI()
    {
        HttpClient client = new HttpClient();
        client.BaseAddress = new Uri(url);

        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        var response = client.GetAsync(requestUri).Result;
        if (response.IsSuccessStatusCode)
        {
            var dataObjects = response.Content.ReadAsAsync<IEnumerable<DataObject>>().Result;
            foreach (var d in dataObjects)
            {
                Console.WriteLine("{0}", d.q);
                Console.WriteLine("- {0}", d.a);
                Console.WriteLine();
            }
        }
        else Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
    }
}