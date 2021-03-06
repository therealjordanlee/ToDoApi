﻿using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ToDoApi.Models;
using ToDoApi.Services;

namespace ToDoApi.Controllers
{
    [ApiController]
    [Route("todo")]
    public class ToDoController : ControllerBase
    {
        private readonly IToDoService _toDoService;

        public ToDoController(IToDoService toDoService)
        {
            _toDoService = toDoService;
        }

        /// <summary>
        /// Gets all ToDo items
        /// </summary>
        /// <returns>All ToDo items</returns>
        /// <response code="200">Returns all ToDo items</response>
        [HttpGet("")]
        public async Task<IActionResult> GetAllToDoItemsAsync()
        {
            var results = await _toDoService.GetAllToDoItemsAsync();
            return Ok(results);
        }

        /// <summary>
        /// Creates a new ToDo Item
        /// </summary>
        /// <param name="newItem"></param>
        /// <returns></returns>
        /// <response code="200">Item was created successfully</response>
        /// <response code="400">Invalid ToDo item</response>
        [HttpPost("")]
        public async Task<IActionResult> AddToDoItemAsync([FromBody] ToDoModel newItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _toDoService.AddToDoItemAsync(newItem);
            return Ok();
        }

        /// <summary>
        /// Gets a ToDo item based on Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A ToDo item</returns>
        /// <response code="200">Returns the ToDo item</response>
        /// <response code="404">ToDo item does not exist</response>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetToDoItemByIdAsync([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await _toDoService.GetToDoItemByIdAsync(id);
            return Ok(result);
        }

        /// <summary>
        /// Delete a ToDo item based on Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Item deleted</response>
        /// <response code="404">Item does not exist</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteToDoItemById([FromRoute]int id)
        {
            await _toDoService.DeleteToDoItemByIdAsync(id);
            return Ok();
        }

        /// <summary>
        /// Update an existing ToDo item
        /// </summary>
        /// <param name="id"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        /// <response code="200">Item updated</response>
        /// <response code="404">Item does not exist</response>
        /// <response code="400">Invalid ToDo Item</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateToDoItemById([FromRoute]int id, [FromBody] ToDoModel update)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _toDoService.UpdateToDoItemByIdAsync(id, update);
            return Ok();
        }
    }
}