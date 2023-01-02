using Microsoft.AspNetCore.Mvc;
using Play.Catalog.Service.Dtos;
using Play.Catalog.Service.Entities;
using Play.Catalog.Service.Repository;

namespace Play.Catalog.Service.Controllers
{
    [ApiController]
    public class ItemsController : ControllerBase
    {
        //private static readonly List<ItemDto> items = new List<ItemDto>()
        //    {
        //        new ItemDto(Guid.NewGuid(), "Potion", "Restores a small amount of HP", 5, DateTimeOffset.UtcNow),
        //        new ItemDto(Guid.NewGuid(), "Antidote", "Cures poiso", 7, DateTimeOffset.UtcNow),
        //        new ItemDto(Guid.NewGuid(), "Bronze sword", "Deals a small amount of damage", 20, DateTimeOffset.UtcNow)
        //    };

        //[HttpGet]
        //[Route("items")]
        //public IEnumerable<ItemDto> Get()
        //{
        //    return items;
        //}

        //[HttpGet]
        //[Route("items")]
        //public async Task<IEnumerable<ItemDto>> Get()
        //{
        //    var items = (await itemsRepository.GetAllAsync())
        //                               .Select(item => item.AsDto());
        //    return items;
        //}

        //[HttpGet("{id}")]
        //public ActionResult<ItemDto> GetByID(Guid id)
        //{
        //    var item = items.Where(item => item.id == id).FirstOrDefault(); 

        //    if (item== null)
        //    {
        //        return NotFound();
        //    }
        //    return item;
        //}

        //[HttpPost]
        //[Route("items")]
        //public ActionResult<ItemDto> Post(CreateItemDto createItemDto)
        //{
        //    var item = new ItemDto(Guid.NewGuid(), createItemDto.Name, createItemDto.Description, createItemDto.Price, DateTimeOffset.UtcNow);

        //    items.Add(item);

        //    return CreatedAtAction(nameof(GetByID), new { id = item.id }, item);

        //}

        //[HttpPut("{id}")]
        //public IActionResult Put(Guid id, UpdateItemDto updateItemDto)
        //{
        //    var existingItem = items.Where(item => item.id == id).SingleOrDefault();

        //    var updateItem = existingItem with
        //    {
        //        Name = updateItemDto.Name,
        //        Description = updateItemDto.Description,
        //        Price = updateItemDto.Price
        //    };

        //    var index = items.FindIndex(existingItem => existingItem.id == id);

        //    items[index] = updateItem;

        //    return NoContent();
        //}

        //public readonly ItemsRepository itemsRepository = new();

        public readonly IItemsRepository itemsRepository;

        public ItemsController(IItemsRepository itemsRepository)
        {
            this.itemsRepository = itemsRepository; 
        }

        [HttpGet]
        [Route("items")]
        public async Task<IEnumerable<ItemDto>> GetAsync()
        {
            var items = (await itemsRepository.GetAllAsync())
                                       .Select(item => item.AsDto());
            return items;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDto>> GetByIDAsync(Guid id)
        {
            var item = await itemsRepository.GetAsync(id);

            if (item == null)
            {
                return NotFound();
            }
            return item.AsDto();
        }

        [HttpPost]
        [Route("items")]
        public async Task<ActionResult<ItemDto>> PostAsync(CreateItemDto createItemDto)
        {
            var item = new Item
            {
                Name = createItemDto.Name,
                Description = createItemDto.Description,
                Price = createItemDto.Price,
                CreatedDate = DateTimeOffset.UtcNow
            };

            await itemsRepository.CreateAsync(item);

            return CreatedAtAction(nameof(GetByIDAsync), new { id = item.id }, item);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, UpdateItemDto updateItemDto)
        {
            var existingItem = await itemsRepository.GetAsync(id);

            if (existingItem == null)
            { 
                return NotFound();
            }

            existingItem.Name = updateItemDto.Name;
            existingItem.Description = updateItemDto.Description;
            existingItem.Price = updateItemDto.Price;

            await itemsRepository.UpdateAsync(existingItem);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var existingItem = await itemsRepository.GetAsync(id);

            if (existingItem == null)
            {
                return NotFound();
            }

            await itemsRepository.RemoveAsync(existingItem);
            return NoContent();
        }
    }
}
