using AutoMapper;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using TaskOfManagingLibrary.DTOs.Domain.Models;
using TaskOfManagingLibrary.DTOs.MainCategoryDtos;
using TaskOfManagingLibrary.DTOs.SubCategoryDtos;

namespace TaskOfManagingLibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MainCategoriesFromEFController : ControllerBase
    {

        private readonly IUnitOfWork _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<MainCategoriesFromEFController> _logger;

        public MainCategoriesFromEFController(IUnitOfWork unitOfWork,
                                           IMapper _mapper,
                                            ILogger<MainCategoriesFromEFController> logger)
        {
            this._repository = unitOfWork;
            this._mapper = _mapper;
            this._logger = logger;
        }

        /// <summary>
        /// Retrieves all main categories with pagination support.
        /// </summary>
        /// <param name="pageNumber">The page number to retrieve. Default is 1.</param>
        /// <param name="pageSize">The number of items per page. Default is 10.</param>
        /// <returns>Returns a paged list of main categories.</returns>

        [HttpGet]
        public async Task<IActionResult> GetAllMainCategories()
        {
            var categories = await _repository.mainCategories.GetAllAsync();

            var mainCategoryDtos = _mapper.Map<List<MainCategoryDto>>(categories);
            return Ok(mainCategoryDtos);


        }
        /// <summary>
        /// Retrieves a main category by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the main category.</param>
        /// <returns>Returns the main category if found, otherwise returns a NotFound result.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var Category = await _repository.mainCategories.GetByIdAsync(id);

            if (Category == null)
            {
                return NotFound($"Category with Id {id} not found.");
            }

            var CategoryDto = _mapper.Map<MainCategoryDto>(Category);

            return Ok(CategoryDto);
        }

        /// <summary>
        /// Retrieves a list of subcategories by the main category identifier.
        /// </summary>
        /// <param name="mainCategoryId">The unique identifier of the main category.</param>
        /// <returns>Returns a list of subcategories associated with the main category.</returns>
        [HttpGet("subcategories/{mainCategoryId}")]
        public async Task<ActionResult<List<SubCategory>>> GetSubCategoriesByMainCategoryId(int mainCategoryId)
        {
            var subCategories = await _repository.subCategories.GetByConditionAsync(s => s.MainCategoryId == mainCategoryId);

            var subCategoryDtos = _mapper.Map<List<SubCategoryDto>>(subCategories);
            return Ok(subCategoryDtos);
        }


        /// <summary>
        /// Adds a new main category.
        /// </summary>
        /// <param name="model">The main category data transfer object containing the details of the main category to be created.</param>
        /// <returns>Returns the created main category along with its identifier.</returns>
        [HttpPost]
        public async Task<ActionResult> Add(MainCategoryForCreateDto model)
        {
            var MainCategory = _mapper.Map<MainCategory>(model);

            await _repository.mainCategories.CreateAsync(MainCategory);
            await _repository.SaveAsync();

            _logger.LogInformation("main Category added successfully");

            var employeeDTo = _mapper.Map<MainCategoryForCreateDto>(MainCategory);

            return CreatedAtAction(nameof(GetById), new { id = MainCategory.MainCategoryId }, employeeDTo);

        }

        /// <summary>
        /// Updates an existing main category.
        /// </summary>
        /// <param name="id">The unique identifier of the main category to be updated.</param>
        /// <param name="model">The data transfer object containing the updated details of the main category.</param>
        /// <returns>Returns a NoContent result if the update is successful.</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] MainCategoryForCreateDto model)
        {
            var MainCategory = await _repository.mainCategories.GetByIdAsync(id);

            if (MainCategory == null)
            {
                return NotFound($"Main Category with Id {id} not found.");
            }

            _mapper.Map(model, MainCategory);
            await _repository.mainCategories.UpdateAsync(MainCategory);
            MainCategory.UpdatedAt = DateTime.UtcNow;
            await _repository.SaveAsync();


            _logger.LogInformation($"Main Category with Id {id} updated successfully", DateTime.UtcNow);

            return NoContent();

        }

        /// <summary>
        /// Soft deletes a main category by setting its IsDeleted flag to true.
        /// </summary>
        /// <param name="id">The unique identifier of the main category to be soft deleted.</param>
        /// <returns>Returns a NoContent result if the soft delete is successful. 
        /// Returns NotFound if the category does not exist.</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> SoftDelete(int id)
        {

            var MainCategory = await _repository.mainCategories.GetByIdAsync(id);

            if (MainCategory == null)
            {
                return NotFound($"Main Category with Id {id} not found.");
            }

            MainCategory.IsDeleted = true;

            await _repository.mainCategories.UpdateAsync(MainCategory);

            await _repository.SaveAsync();

            _logger.LogInformation($"Main Category with Id {id} deleted successfully.");

            return NoContent();

        }

    }
}
