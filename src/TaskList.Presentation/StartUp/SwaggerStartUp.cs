using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace TaskList.Presentation.StartUp;

public static class SwaggerStartUp
{
    public static void SetSwaggerOptions(SwaggerGenOptions swagger)
    {  
        //This is to generate the Default UI of Swagger Documentation  
        swagger.SwaggerDoc("v1", new OpenApiInfo  
        {   
            Version= "v1",   
            Title = "JWT Token Authentication API",  
            Description="ASP.NET Core Web API" });  
        // To Enable authorization using Swagger (JWT)  
        swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()  
        {  
            Name = "Authorization",  
            Type = SecuritySchemeType.ApiKey,  
            Scheme = "Bearer",
            BearerFormat = "JWT",  
            In = ParameterLocation.Header,  
            Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345hello\"",  
        });  
        swagger.AddSecurityRequirement(new OpenApiSecurityRequirement  
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
    }
}