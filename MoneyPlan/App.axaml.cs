using System;
using System.Linq;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Grpc.Net.Client;
using Microsoft.Extensions.DependencyInjection;
using MoneyPlan.Contracts;
using MoneyPlan.Infrastructure;
using MoneyPlan.Intefaces;
using MoneyPlan.ViewModels;
using MoneyPlan.Views;

namespace MoneyPlan;

public partial class App : Application
{
    public new static App Current => (App)Application.Current!;
    public IServiceProvider Services { get; private set; } = null!;

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        // 1. Создаем коллекцию сервисов
        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection);

        // 2. Создаем провайдер
        Services = serviceCollection.BuildServiceProvider();

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = Services.GetRequiredService<MainWindowViewModel>()
            };
        }

        base.OnFrameworkInitializationCompleted();
    }

    private void ConfigureServices(IServiceCollection services)
    {
        // Регистрируем gRPC инфраструктуру
        services.AddSingleton(sp =>
            GrpcChannel.ForAddress("http://localhost:5215")); // Твой адрес бэкенда

        services.AddSingleton(sp =>
        {
            var channel = sp.GetRequiredService<GrpcChannel>();
            return new MoneyGoalsGrpcService.MoneyGoalsGrpcServiceClient(channel);
        });

        // Регистрируем наш новый сервисный слой
        services.AddSingleton<IGoalsService, GrpcMoneyGoalsService>();

        // 2. Автоматическая регистрация всех ViewModels
        var assembly = typeof(App).Assembly; // Берем текущую сборку

        var viewModels = assembly.GetTypes()
            .Where(t => t.Name.EndsWith("ViewModel") && !t.IsAbstract && !t.IsInterface);

        foreach (var vm in viewModels)
        {
            services.AddTransient(vm);
        }
    }
}