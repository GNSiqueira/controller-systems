// using statements necessários para as correções
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

// using statements que você já tinha
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using ControlSystems.Data;
using ControlSystems.Objects.Contracts.Exceptions;
using ControlSystems.Authentication;
using ControlSystems.Services.Entities;
using ControlSystems.Services.Interfaces;
using ControlSystems.Data.Interfaces;
using ControlSystems.Objects.Models;
using ControlSystems.Data.Repositories;


namespace ControlSystems;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // Este método é chamado em tempo de execução. Use este método para adicionar serviços ao contêiner.
    public void ConfigureServices(IServiceCollection services)
    {
        // BOA PRÁTICA: Removido o if/else de ambiente. O .NET escolherá a string de conexão
        // do appsettings.Development.json em ambiente de dev, e do appsettings.json em produção.
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

        // CORREÇÃO: Bloco único e completo para configuração do Swagger.
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "SeniorCareManager", Version = "v1" });

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "Autenticação via Token JWT. Insira 'Bearer' [espaço] e o seu token. Exemplo: 'Bearer 12345abcdef'",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                    new string[] {}
                }
            });
        });

        services.Configure<JwtSettings>(Configuration.GetSection("JwtSettings"));

        // Adiciona controllers e trata a serialização Json
        services.AddControllers(options =>
        {
            options.Filters.Add<HttpExceptionFilter>();
        }).AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
            options.JsonSerializerOptions.WriteIndented = true;
        });

        services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
        {
            builder.WithOrigins("http://localhost:3000", "http://localhost:5173")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
        }));

        // CORREÇÃO: Configuração completa da autenticação JWT Bearer.
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            var jwtSettings = Configuration.GetSection("JwtSettings");
            var jwtKey = jwtSettings["Key"];
            if (string.IsNullOrEmpty(jwtKey))
            {
                throw new InvalidOperationException("JWT Key is not configured.");
            }
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings["Issuer"],
                ValidAudience = jwtSettings["Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
            };
        });

        // Serviços essenciais e específicos da sua aplicação
        services.AddHttpContextAccessor();
        services.AddScoped<JwtService>();

        // AutoMapper (se for usar no futuro)
        // services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        // Scoped SERVICIES
        services.AddScoped<IAuthService, UsuarioService>();

        // Scoped REPOSITORIES
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        services.AddScoped<IDispositivoRepository, DispositivoRepository>();

        services.AddEndpointsApiExplorer();
    }

    // Este método é chamado em tempo de execução. Use este método para configurar o pipeline de requisições HTTP.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "SeniorCareManager Web API V1");
                c.DocExpansion(DocExpansion.None); // Mantém os endpoints recolhidos
            });
        }
        else
        {
            app.UseExceptionHandler("/home/Error");
            app.UseHsts();
        }

        // BOA PRÁTICA: Habilitando o redirecionamento para HTTPS.
        app.UseHttpsRedirection();

        app.UseRouting();

        // A ordem aqui é importante: CORS primeiro.
        app.UseCors("MyPolicy");

        // Depois Autenticação e Autorização.
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}