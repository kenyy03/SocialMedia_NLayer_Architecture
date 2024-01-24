using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.Interfaces.Services;
using SocialMedia.Core.Services.SocialMediaServices;
using SocialMedia.Infraestructure.Data;
using SocialMedia.Infraestructure.Extensions;
using SocialMedia.Infraestructure.Filters;
using SocialMedia.Core.Enumerations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddControllers(options => options.Filters.Add<GlobalExceptionsFilter>())
                .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
                //.ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);
builder.Services.AddDbContext<SocialMediaContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SocialMedia")));
builder.Services.AddUnitOfWorkBuilder((configuration, provider) =>
{
    configuration.RegisterUnitOfWork(UnitOfWorkType.SocialMedia, () => provider.GetRequiredService<SocialMediaContext>());
});
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddMvc(options => options.Filters.Add<ValidationFilter>());
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowOriginSocialMediaApi",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowOriginSocialMediaApi");

app.UseAuthorization();

app.MapControllers();

app.Run();
