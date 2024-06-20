﻿using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebBanHang.Data;

[assembly: HostingStartup(typeof(WebBanHang.Areas.Identity.IdentityHostingStartup))]
namespace WebBanHang.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<WebBanHangContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("WebBanHangContextConnection")));

                services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<WebBanHangContext>();
            });
        }
    }
}