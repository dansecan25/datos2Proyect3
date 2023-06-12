using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using angular_project;
using angular_project.Models;

public class Program
{
    public static void Main(string[] args)
    {
        var morseControl = new MorseManager();
        string testString= "01111010000101010010";
        Console.WriteLine("Original morse:  "+testString);
        string alphabet=morseControl.MorseToAlphabet(testString);
        Console.WriteLine("Word:  "+alphabet);
        //Test alphabet and compress it
        var huffmanControl = new Huffman();

        Console.WriteLine("Reconverted string:  "+morseControl.alphabetToMorse(alphabet));
        var  MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();
        builder.Services.AddCors(options =>
        {
            options.AddPolicy(name: MyAllowSpecificOrigins,
            policy  =>
                      {
                           policy.WithOrigins("http://localhost:4200/register")
                                .AllowAnyOrigin()
                                .AllowAnyMethod()
                                .AllowAnyHeader();
                      });
        });

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseCors(MyAllowSpecificOrigins); // Specify the policy name "default"

        app.UseRouting();

    
        app.UseAuthorization();

        app.MapControllers();

        app.MapFallbackToFile("index.html");

        app.Run();
    }
}
