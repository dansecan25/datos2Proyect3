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
        string testString= "00011000000110110010001000001000000011010001100000011010001100000011011001000100";
        Console.WriteLine("Original morse:  "+testString);
        string alphabet=morseControl.MorseToAlphabet(testString);
        //string alphabet="DANSECANDANDANSE";
        Console.WriteLine("Word:  "+alphabet);
        //Test alphabet and compress it
        var huffmanControl = new Huffman();
        Console.WriteLine("Compressed string: "+huffmanControl.compress(alphabet,"dansecan26"));
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
