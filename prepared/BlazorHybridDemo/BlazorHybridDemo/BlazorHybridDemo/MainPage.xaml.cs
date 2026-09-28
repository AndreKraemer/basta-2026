using System.Text;
using System.Text.Json;

namespace BlazorHybridDemo
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void blazorWebView_WebResourceRequested(object sender, WebViewWebResourceRequestedEventArgs e)
        {
            //return;
            if (e.Uri.AbsolutePath == "/_content/BlazorHybridDemo.Shared/users.json")
            {
                e.Handled = true;
                var customUsers = new[]
                {
                new { id = 1, name = "Dr. Holger Schwichtenberg" },
                new { id = 2, name = "Rainer Stropek" },
                new { id = 3, name = "Christian Wenz" },
                new { id = 4, name = "Thomas Claudius Huber" },
                new { id = 5, name = "André Krämer" },
            };

                var json = JsonSerializer.Serialize(
                    customUsers,
                    new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                        WriteIndented = true
                    });

                var bytes = Encoding.UTF8.GetBytes(json);
                var stream = Task.FromResult<Stream>(new MemoryStream(bytes));

                e.SetResponse(
                    200,
                    "OK",
                    "application/json",
                    stream);
            }
        }
    }
}
