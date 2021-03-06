﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Orleans.Providers.EntityFramework.Extensions
{
    public static class GrainStorageServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureGrainStorageOptions<TContext, TGrain, TGrainState>(
            this IServiceCollection services,
            Action<GrainStorageOptions<TContext, TGrain, TGrainState>> configureOptions = null)
            where TContext : DbContext
            where TGrain : Grain<TGrainState>
            where TGrainState : class, new()
        {
            return services
                .AddSingleton<IPostConfigureOptions<GrainStorageOptions<TContext, TGrain, TGrainState>>,
                    GrainStoragePostConfigureOptions<TContext, TGrain, TGrainState>>()
                .Configure<GrainStorageOptions<TContext, TGrain, TGrainState>>(typeof(TGrain).FullName, options =>
                {
                    configureOptions?.Invoke(options);
                });
        }


    }
}