using AppWebNaiara.Components;
using AppWebNaiara.Configs;
using AppWebNaiara.DAO;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Conexão com o banco
builder.Services.AddScoped<Conexao>();

// DAO dos processos
builder.Services.AddScoped<ProcessoDAO>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler(
        "/Error",
        createScopeForErrors: true);
}

app.UseStatusCodePagesWithReExecute("/not-found");

app.UseAntiforgery();

app.MapStaticAssets();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();