using IssueStock.Demo.API.Models;
using IssueStock.Demo.API.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace IssueStock.Demo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IDataRepository<StockItem> _dataRepository;

        public StockController(IDataRepository<StockItem> dataRepository)
        {
            _dataRepository = dataRepository;
        }

        /// <summary>
        ///  Get All Items
        /// </summary>
        /// <returns></returns>
        /// GET: api/Stock
        [HttpGet]
        [Authorize(Roles = "Admin,User,Auditor")]
        public IActionResult Get()
        {
            IEnumerable<StockItem> stockItems = _dataRepository.GetAll();
            return Ok(stockItems);
        }

        /// <summary>
        /// Get Item
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// GET: api/Stock/5
        [HttpGet("{id}", Name = "Get")]
        [Authorize(Roles = "Admin,User,Auditor")]
        public IActionResult Get(int id)
        {
            var stockItem = _dataRepository.Get(id);
            if (stockItem == null)
            {
                return NotFound("The StockItem record couldn't be found.");
            }

            return Ok(stockItem);
        }

        /// <summary>
        /// Create Item
        /// </summary>
        /// <param name="stockItem"></param>
        /// <returns></returns>
        /// POST: api/Stock
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Post([FromBody] StockItem stockItem)
        {
            if (stockItem == null)
                return BadRequest("StockItem is null.");

            _dataRepository.Add(stockItem);

            return CreatedAtRoute(
                  "Get",
                  new { Id = stockItem.Id },
                  stockItem);
        }

        /// <summary>
        /// Update Item
        /// </summary>
        /// <param name="id"></param>
        /// <param name="stockItem"></param>
        /// <returns></returns>
        /// PUT: api/Stock/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Auditor")]
        public IActionResult Put(int id, [FromBody] StockItem stockItem)
        {
            if (stockItem == null)
                return BadRequest("StockItem is not available.");
            
            var stockItemToUpdate = _dataRepository.Get(id);
            if (stockItemToUpdate == null)
                return NotFound("The Stock item record couldn't be found.");

            _dataRepository.Update(stockItemToUpdate, stockItem);
            return NoContent();
        }
    }
}
