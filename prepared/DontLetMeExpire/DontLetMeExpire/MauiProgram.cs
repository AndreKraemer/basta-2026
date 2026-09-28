using Azure.AI.OpenAI;
using Camera.MAUI;
using CommunityToolkit.Maui;
using DontLetMeExpire.OpenFoodFacts;
using DontLetMeExpire.Services;
using DontLetMeExpire.ViewModels;
using DontLetMeExpire.Views;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Logging;
using Syncfusion.Maui.Toolkit.Hosting;
using Microsoft.Maui.Essentials.AI;


namespace DontLetMeExpire
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
              .UseMauiApp<App>()
              .ConfigureSyncfusionToolkit()
              .UseMauiCommunityToolkit()
              .UseMauiCameraView() // Camera.Maui
              .ConfigureFonts(fonts =>
              {
                  fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                  fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                  fonts.AddFont("MaterialSymbols-Rounded.ttf", "MaterialSymbolsRounded");
              });

#if DEBUG
            builder.Logging.AddDebug();
#endif
#if WINDOWS
            // DatePicker unter Windows: Erster Tag der Woche anpassen
            Microsoft.Maui.Handlers.DatePickerHandler.Mapper.AppendToMapping("FixFirstDayOfWeek", (handler, view) =>
            {
                handler.PlatformView.FirstDayOfWeek = (Windows.Globalization.DayOfWeek)(int)
              System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
            });
#endif

            //    builder.Services.AddSingleton<IStorageLocationService, DummyStorageLocationService>();
            //    builder.Services.AddSingleton<IItemService, DummyItemService>();
            builder.Services.AddSingleton<IStorageLocationService, SqliteStorageLocationService>();
            builder.Services.AddSingleton<IItemService, SqliteItemService>();
            builder.Services.AddSingleton<IAlertService, AlertService>();
            builder.Services.AddSingleton<ISettingsService, SettingsService>();
            builder.Services.AddSingleton<IPreferences>(Preferences.Default);
            builder.Services.AddSingleton<INavigationService, NavigationService>();
            builder.Services.AddSingleton<IOpenFoodFactsApiClient, OpenFoodFactsApiClient>();
            builder.Services.AddTransient<MainViewModel>();
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<ItemViewModel>();
            builder.Services.AddTransient<ItemPage>();
            builder.Services.AddTransient<ItemsViewModel>();
            builder.Services.AddTransient<ItemsPage>();
            builder.Services.AddTransient<SettingsViewModel>();
            builder.Services.AddTransient<SettingsPage>();


#if IOS || MACCATALYST
		builder.AddAppleIntelligenceServices();
#else
            builder.AddAzureOpenAIServices();
#endif
            return builder.Build();
        }


#if IOS || MACCATALYST
#pragma warning disable CA1416 // Validate platform compatibility - this sample requires iOS/macCatalyst 26.0+
	private static MauiAppBuilder AddAppleIntelligenceServices(this MauiAppBuilder builder)
	{
		// Register the base Apple Intelligence client
		builder.Services.AddSingleton<AppleIntelligenceChatClient>();

		// Register the Apple Intelligence client as IChatClient to allow direct use
		builder.Services.AddSingleton<IChatClient>(sp =>
		{
			var appleClient = sp.GetRequiredService<AppleIntelligenceChatClient>();
			return appleClient
				.AsBuilder()
				.Build();
		});

		
		return builder;
	}
#pragma warning restore CA1416
#else
        private static MauiAppBuilder AddAzureOpenAIServices(this MauiAppBuilder builder)
        {

            var endpoint = Environment.GetEnvironmentVariable("AI_Foundry_Endpoint") ?? "http://localhost/make-sure-to-add-your-endpoint-here";
            var apiKey = Environment.GetEnvironmentVariable("AI_Foundry_ApiKey") ?? "add-your-api-key-here";
            var foundryClient = new AzureOpenAIClient(new Uri(endpoint), new System.ClientModel.ApiKeyCredential(apiKey));
            var chatClient = foundryClient.GetChatClient("gpt-4o").AsIChatClient();

            builder.Services.AddChatClient(chatClient);
            return builder;
        }
#endif
    }
}
