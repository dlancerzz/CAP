﻿using System;
using DotNetCore.CAP.RabbitMQ;
using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace DotNetCore.CAP
{
    internal sealed class RabbitMQCapOptionsExtension : ICapOptionsExtension
    {
        private readonly Action<RabbitMQOptions> _configure;

        public RabbitMQCapOptionsExtension(Action<RabbitMQOptions> configure)
        {
            _configure = configure;
        }

        public void AddServices(IServiceCollection services)
        {
            services.AddSingleton<CapMessageQueueMakerService>();

            var options = new RabbitMQOptions();
            _configure?.Invoke(options);
            services.AddSingleton(options);

            services.AddSingleton<IConsumerClientFactory, RabbitMQConsumerClientFactory>();
            services.AddSingleton<IConnectionChannelPool, ConnectionChannelPool>();
            services.AddSingleton<IQueueExecutor, PublishQueueExecutor>();
            services.AddSingleton<IPublishExecutor, PublishQueueExecutor>();
        }
    }
}