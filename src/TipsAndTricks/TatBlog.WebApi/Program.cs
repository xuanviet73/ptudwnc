var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

<<<<<<< Updated upstream
app.Run();
=======
    app.MapAuthorEndpoints();

    //app.MapCategoryEndpoints();

    app.Run();
}
app.Run();
>>>>>>> Stashed changes
