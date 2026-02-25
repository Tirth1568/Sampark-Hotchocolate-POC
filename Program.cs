using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Sampark.Authorization;
using Sampark.config;
using Sampark.Data;
using Sampark.GraphQL;
using Sampark.GraphQL.Inputs;
using Sampark.GraphQL.Mutations;
using Sampark.GraphQL.Validators;
using Sampark.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpContextAccessor();

// Add DbContext
builder.Services.AddDbContext<SamparkDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add FluentValidation validators
builder.Services.AddScoped<IValidator<PersonInput>, PersonValidator>();
builder.Services.AddScoped<IValidator<ProjectInput>, ProjectValidator>();
builder.Services.AddScoped<IValidator<EntityInput>, EntityValidator>();

// Demo external authorization integration.
builder.Services.AddScoped<IAsmAuthorizationService, DemoExternalAsmAuthorizationService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services
    .AddGraphQLServer()
    .AddQueryType(d => d.Name("Query"))
    .AddMutationType(d => d.Name("Mutation"))
    .AddTypeExtension<TestQuery>()
    .AddTypeExtension<PersonMutation>()
    .AddTypeExtension<ProjectMutation>()
    .AddTypeExtension<EntityMutation>()
    .AddProjections()
    .AddFiltering()
    .AddSorting()
    .ModifyRequestOptions(opt => opt.IncludeExceptionDetails = true);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseMiddleware<GlobalExceptionMiddleware>();

// Demo authentication based on request headers:
// x-user: any value (marks user as authenticated)
// x-asm-codes: comma separated ASM codes, e.g. ASM_PROJECT_READ
app.UseMiddleware<DemoAuthenticationMiddleware>();

app.UseAuthorization();

app.UseMiddleware<GraphQLExceptionStatusCodeMiddleware>();

app.MapControllers();

// Map GraphQL endpoint
app.MapGraphQL();

app.Run();
