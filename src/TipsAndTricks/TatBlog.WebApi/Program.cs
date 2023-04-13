var builder = WebApplication.CreateBuilder(args);
<<<<<<< Updated upstream

=======
{
    builder
        .ConfigureCors()
        .ConfigureNLog()
        .ConfigureServices()
        .ConfigureSwaggerOpenApi()
        .ConfigureMapster()
        .ConfigureFluentValidation(); 
        
}
>>>>>>> Stashed changes
var app = builder.Build();

<<<<<<< Updated upstream
app.Run();
=======
    app.MapAuthorEndpoints();
<<<<<<< Updated upstream

    app.MapCategoryEndpoints();

    app.MapPostEndpoints();
=======
    app.MapCategoriesEndpoints();
>>>>>>> Stashed changes

    app.Run();
}
app.Run();
>>>>>>> Stashed changes
