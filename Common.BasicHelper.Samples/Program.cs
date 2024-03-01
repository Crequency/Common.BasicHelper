using System.Web;
using Common.BasicHelper.Core.Shell;
using Common.BasicHelper.Graphics.Screen;
using Common.BasicHelper.Utils.Extensions;

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

static bool parameterNullCheck(string? param, string? name = null)
{
    return param == "," || param == $"{{{name}}}" || param.IsNullOrWhiteSpace();
}

app.MapGet("/", () => Results.Redirect("/swagger/index.html"));

app.MapGet("/Graphics/Screen/Resolution/Parse/{resolution}.{descr?}",
    (string resolution, string? descr) => Resolution.Parse(
        resolution, parameterNullCheck(descr, nameof(descr)) ? null : descr
    )
)
.WithName("ParseSolution")
.WithOpenApi();

app.MapGet("/Utils/Extensions/StringHelper/ExecuteAsCommand/{cmd}.{args?}",
    (string cmd, string? args) => cmd.ExecuteAsCommand(
        parameterNullCheck(args, nameof(args)) ? null : HttpUtility.UrlDecode(args)
    )
)
.WithName("ExecuteAsCommand")
.WithOpenApi();

app.MapGet("/Utils/Extensions/StringHelper/ExecuteAsCommandAsync/{cmd}.{args?}",
    async (string cmd, string? args) => await cmd.ExecuteAsCommandAsync(
        parameterNullCheck(args, nameof(args)) ? null : HttpUtility.UrlDecode(args)
    )
)
.WithName("ExecuteAsCommandAsync")
.WithOpenApi();

app.Run();
