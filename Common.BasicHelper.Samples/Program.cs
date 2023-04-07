using Common.BasicHelper.Core.Shell;
using Common.BasicHelper.Utils.Extensions;
using System.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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

app.MapGet("/Utils/Extensions/StringHelper/ExecuteAsCommand/{cmd}.{args?}",
    (string cmd, string? args) => cmd.ExecuteAsCommand(
        args == "," || args == "{args}" || args.IsNullOrWhiteSpace()
        ? null : HttpUtility.UrlDecode(args)
    )
)
.WithName("ExecuteAsCommand")
.WithOpenApi();

app.MapGet("/Utils/Extensions/StringHelper/ExecuteAsCommandAsync/{cmd}.{args?}",
    async (string cmd, string? args) => await cmd.ExecuteAsCommandAsync(
        args == "," || args == "{args}" || args.IsNullOrWhiteSpace()
        ? null : HttpUtility.UrlDecode(args)
    )
)
.WithName("ExecuteAsCommandAsync")
.WithOpenApi();

app.Run();
