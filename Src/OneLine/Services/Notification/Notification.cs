﻿using BlazorNotification;
using Microsoft.Extensions.DependencyInjection;

namespace OneLine.Services
{
    public class Notification : INotification
    {
        public IBlazorNotificationService BlazorNotificationService { get; set; }
        public Notification(IBlazorNotificationService blazorNotificationService)
        {
            BlazorNotificationService = blazorNotificationService;
        }
    }
    public static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection AddNotification(this IServiceCollection services)
        {
            return services.AddScoped<INotification, Notification>();
        }
    }
}
