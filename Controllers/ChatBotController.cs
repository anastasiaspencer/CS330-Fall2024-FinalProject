using System;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

using CS330_Fall2024_FinalProject.Models;
using Azure.AI.OpenAI;
using CS330_Fall2024_FinalProject.Data;

namespace CS330_Fall2024_FinalProject.Controllers;


// namespace Fall2024_Assignment3_akspencer1.Controllers

public class ChatBotController : Controller
{
    private readonly ApplicationDbContext _context; 

    private readonly string apiUrl = "https://fall2024-akspencer1-openai.openai.azure.com/openai/deployments/gpt-35-turbo/chat/completions?api-version=2024-08-01-preview";
    private readonly string apiKey;
    private readonly IConfiguration _configuratiodotnn;
    private readonly ILogger<ChatBotController> _logger;


    public ChatBotController(ApplicationDbContext context, ILogger<ChatBotController> logger, IConfiguration configuration)
    {
        _context = context;
        _logger = logger;
        apiKey = configuration.GetValue<string>("OpenAi:ApiKey");
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> GenerateResponse(string userPrompt)
    {
        
        var chatBot = new ChatBot { response = new List<string>() };

        var prompt = $"'{userPrompt}'.";
        // var introductoryMessage = "Hi! Welcome to Ski Bot, a helpful tool for all users!\n";
        // decided to move introductoryMessage to index.cshtml, as this should be added as persistent div not only when user submits a prompt

        var requestBody = new
        {
            messages = new[]
            {
            new { role = "system", content = "You are a Ski Bot, chat bot for Snow Ski team. then add a new line, and your response" },
            new { role = "user", content = prompt }
        },
            max_tokens = 100
        };

        try
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("api-key", apiKey);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(apiUrl, content);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrWhiteSpace(jsonResponse))
                {
                    using (JsonDocument doc = JsonDocument.Parse(jsonResponse))
                    {
                        var choices = doc.RootElement.GetProperty("choices");
                        if (choices.GetArrayLength() > 0)
                        {
                            var reviewsText = choices[0].GetProperty("message").GetProperty("content").GetString();
                            if (!string.IsNullOrWhiteSpace(reviewsText))
                            {
                                var answer = reviewsText.Split("\n")
                                                        .Where(review => !string.IsNullOrWhiteSpace(review))
                                                        .ToList();

                                // chatBot.response.Add(introductoryMessage);
                                chatBot.response.AddRange(answer);
                            }
                        }
                    }
                }
            }
            else
            {
                // chatBot.response.Add(introductoryMessage);
                chatBot.response.Add("I'm sorry, there was an error processing your request.");
            }

            return View("Index", chatBot);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while generating a response.");
            // chatBot.response.Add(introductoryMessage);
            chatBot.response.Add("I'm sorry, something went wrong. Please try again.");
            return View("Index", chatBot);
        }
    }

}
