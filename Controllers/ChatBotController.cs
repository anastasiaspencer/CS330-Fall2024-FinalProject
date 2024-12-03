using System;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

using CS330_Fall2024_FinalProject.Models;
using Azure.AI.OpenAI;
using CS330_Fall2024_FinalProject.Data;

namespace Fall2024_Assignment3_akspencer1.Controllers
{
    public class ChatBotController : Controller
    {
        private readonly ApplicationDbContext _context; //Represents the session with the database and is used to
        //interact with the data

        private readonly string apiUrl = "https://fall2024-akspencer1-openai.openai.azure.com/";
        private readonly string apiKey = "621a53b06eaa453396b06be276cbc4e4";
        private readonly ILogger<ChatBotController> _logger;


        public ChatBotController(ApplicationDbContext context, ILogger<ChatBotController> logger)
        {
            _context = context;
            _logger = logger;
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
            //Anonymous object 
            var requestBody = new
            {
                //Messages to send to openAI
                messages = new[]
            {
                //Inform system about its role 
                new { role = "system", content = "You are a chat bot." },
                //request for AI
                new { role = "user", content = prompt }
            },  //max number of words the AI can geneerate
                max_tokens = 1000
            };

            try
            {
                using var client = new HttpClient();
                //add key to header for verification
                client.DefaultRequestHeaders.Add("api-key", apiKey);
                //indicates that the client expects to recieve a response in JSON format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");
                //sends the post request
                var response = await client.PostAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();

                    // Use JsonDocument to parse the response
                    using (JsonDocument doc = JsonDocument.Parse(jsonResponse))
                    {
                        // Navigate the JSON structure to get the reviews
                        var choices = doc.RootElement.GetProperty("choices");
                        if (choices.GetArrayLength() > 0)
                        {
                            var reviewsText = choices[0].GetProperty("message").GetProperty("content").GetString();
                            if (!string.IsNullOrWhiteSpace(reviewsText))
                            {
                                var answer = reviewsText.Split("\n")
                                                            .Where(review => !string.IsNullOrWhiteSpace(review))
                                                            .ToList();
                                chatBot.response = answer;
                            }
                            // _logger.LogError("An error occurred while generating reviews for movie: {MovieTitle}", movieReviews.MovieTitle);
                        }
                        else
                        {
                            //  _logger.LogError("There were items returned", movieReviews.MovieTitle);
                        }

                    }
                }

                return View("Index", chatBot);
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "An error occurred while generating reviews for movie: {MovieTitle}", movieReviews.MovieTitle);
                throw; // Re-throw if you want to handle it further up the pipeline
            }



        }
    }
}

