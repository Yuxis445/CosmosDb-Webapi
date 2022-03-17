using CosmosDbTest.Model;
using CosmosDbTest.Services;
using Microsoft.AspNetCore.Mvc;


namespace CosmosDbTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoGameController : ControllerBase
    {
        private readonly IVideoGameService _videoGameService;

        public VideoGameController(IVideoGameService videoGameService){
            _videoGameService = videoGameService;
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _videoGameService.GetAllAsync());
        }
        [HttpPost]
        public async Task<IActionResult> SaveAsync([FromBody] VideoGame item)
        {
            if (item == null)
                return BadRequest(new { message = "Item may not be null" });

            // Verify whether Cosmos container already has the item received
            var videoGames = await _videoGameService.GetAllAsync();
            if (videoGames.Any(x => x.Equals(item)))
                return BadRequest(new { message = "Esee Item ja existe na Collection do CosmosDB" });

            await _videoGameService.SaveAsync(item);
            return Ok("Item criado com sucesso!");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> GetById([FromBody] VideoGame item){
            await _videoGameService.Put(item);
            return Ok($"VideoGme de Id = {item.Id} atualizado com sucesso");
        }

        [HttpGet("id/{id}")]
        public async Task<IActionResult> GetById(string id){
            return Ok(await _videoGameService.GetById(id));
        }

        [HttpGet("name/{id}")]
        public async Task<IActionResult> GetNameById(string id){
            return Ok(await _videoGameService.GetName(id));
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteById(string id){
            await _videoGameService.DeleteById(id);
            return Ok($"VideoGame de Id = {id} foi deletado com sucesso" );
        }
    }
}