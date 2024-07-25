using AutoMapper;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using TaskOfManagingLibrary.DTOs.SubCategoryDtos;

namespace TaskOfManagingLibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCategoriesController : ControllerBase
    {
        private readonly IUnitOfWork _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<SubCategoriesController> _logger;

        public SubCategoriesController(IUnitOfWork unitOfWork,
                                           IMapper _mapper,
                                            ILogger<SubCategoriesController> logger)
        {
            this._repository = unitOfWork;
            this._mapper = _mapper;
            this._logger = logger;
        }



        /// <summary>
        /// Retrieves a paginated list of all main categories.
        /// </summary>
        /// <param name="pageNumber">The page number to retrieve. Default is 1.</param>
        /// <param name="pageSize">The number of items per page. Default is 10.</param>
        /// <returns>Returns a paginated list of main categories.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllMainCategories([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var Categories = await _repository.subCategories.GetAllAsync(pageNumber, pageSize);

            var CategoriesDto = _mapper.Map<PagedResult<SubCategoryDto>>(Categories);
            return Ok(CategoriesDto);


        }

        /// <summary>
        /// Retrieves a category by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the category.</param>
        /// <returns>Returns the category details if found, otherwise NotFound.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var Category = await _repository.subCategories.GetByIdAsync(id);

            if (Category == null)
            {
                return NotFound($"Category with Id {id} not found.");
            }

            var CategoryDto = _mapper.Map<SubCategoryDto>(Category);

            return Ok(CategoryDto);
        }


        /// <summary>
        /// Adds a new subcategory.
        /// </summary>
        /// <param name="model">The subcategory data to be added.</param>
        /// <returns>Returns the created subcategory details with its unique identifier.</returns>

        [HttpPost]
        public async Task<ActionResult> Add(SubCategoryForCreateDto model)
        {
            var SubCategory = _mapper.Map<SubCategory>(model);

            await _repository.subCategories.CreateAsync(SubCategory);
            await _repository.SaveAsync();

            _logger.LogInformation("main Category added successfully");

            var employeeDTo = _mapper.Map<SubCategoryForCreateDto>(SubCategory);

            return CreatedAtAction(nameof(GetById), new { id = SubCategory.SubCategoryId }, employeeDTo);

        }


        /// <summary>
        /// Updates an existing subcategory by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the subcategory to be updated.</param>
        /// <param name="model">The updated subcategory data.</param>
        /// <returns>Returns NoContent if the update is successful,
        /// otherwise NotFound if the subcategory does not exist.</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] SubCategoryForCreateDto model)
        {
            var SubCategory = await _repository.subCategories.GetByIdAsync(id);

            if (SubCategory == null)
            {
                return NotFound($"Main Category with Id {id} not found.");
            }

            _mapper.Map(model, SubCategory);
            await _repository.subCategories.UpdateAsync(SubCategory);
            SubCategory.UpdatedAt = DateTime.UtcNow;
            await _repository.SaveAsync();


            _logger.LogInformation($"Sub Category with Id {id} updated successfully", DateTime.UtcNow);

            return NoContent();

        }



        /// <summary>
        /// Soft deletes an existing subcategory by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the subcategory to be soft deleted.</param>
        /// <returns>Returns NoContent if the soft delete is successful, 
        /// otherwise NotFound if the subcategory does not exist.</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> SoftDelete(int id)
        {

            var SubCategory = await _repository.subCategories.GetByIdAsync(id);

            if (SubCategory == null)
            {
                return NotFound($"Sub Category with Id {id} not found.");
            }

            SubCategory.IsDeleted = true;

            await _repository.subCategories.UpdateAsync(SubCategory);

            await _repository.SaveAsync();

            _logger.LogInformation($"Sub Category with Id {id} deleted successfully.");

            return NoContent();

        }
    }
}
