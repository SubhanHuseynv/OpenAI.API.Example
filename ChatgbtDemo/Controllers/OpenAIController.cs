using Microsoft.AspNetCore.Mvc;
using OpenAI.Chat;

namespace ChatgbtDemo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OpenAIController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public OpenAIController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpPost]
        public async Task<IActionResult> GetAIBasedResult(string prompt)
        {
            if (string.IsNullOrWhiteSpace(prompt))
                return BadRequest();

            string APIKey = _configuration["OpenAI:SecretKey"];
            string answer = string.Empty;

            string modelName = "gpt-4o";
            var client = new ChatClient(modelName, APIKey);

            var messages = new List<ChatMessage>();

            messages.Add(new UserChatMessage(prompt));
                var response = await client.CompleteChatAsync(messages);
            return Ok(response.Value.Content[0].Text);
        }
    }
}
