using HotPotToYou.Service.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Repository.ActivityTypeRepository;
using Repository.Customers;
using Repository.DbContexts;
using Repository.HotPots;
using Repository.HotPotType;
using Repository.IngredientGroupRepository;
using Repository.PaymentRepository;
using Repository.Roles;
using Repository.Users;
using Repository.Utensils;

using Service.ActivityType;
using Repository.Utensils;
using Service.CurrentUser;
using Service.Customers;
using Service.HotPots;
using Service.HotPotType;
using Service.IngredientGroup;
using Service.Password;
using Service.Payment;
using Service.Roles;
using Service.Users;
using Service.Utensils;
using System.Reflection;
using System.Text;
using Repository.Order;
using Service.Order;
using HotPotToYou.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

#region New Config
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Server")));

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "HotPotToYou API", Version = "v1" });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "test",
        ValidAudience = "api",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("H0t P0t T0 Y0u @lways R3@dy 4 U 2 R3nt!!!"))
    };
});
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.WithOrigins("*")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
        });
});
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
builder.Services.AddScoped<IPasswordService, PasswordService>();
builder.Services.AddScoped<IJwtService, JwtService>();

builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IRoleService, RoleService>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomerService, CustomerService>();

builder.Services.AddScoped<IHotPotTypeRepository, HotPotTypeRepository>();
builder.Services.AddScoped<IHotPotTypeService, HotPotTypeService>();

builder.Services.AddScoped<IActivityTypeRepository, ActivityTypeRepository>();
builder.Services.AddScoped<IActivityTypeService, ActivityTypeService>();

builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IPaymentService, PaymentService>();


builder.Services.AddScoped<IHotPotRepository, HotPotRepository>();
builder.Services.AddScoped<IHotPotService, HotPotService>();

builder.Services.AddScoped<IIngredientGroupRepository, IngredientGroupRepository>();
builder.Services.AddScoped<IIngredientGroupService, IngredientGroupService>();

builder.Services.AddScoped<IUtensilRepository, UtensilRepository>();
builder.Services.AddScoped<IUtensilService, UtensilService >();

builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();



#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "HotPotToYou API V1");
        c.RoutePrefix = string.Empty;
    });
}
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
