using AutoMapper;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using TaskOfManagingLibrary.DTOs.AuthorDtos;


namespace TaskOfManagingLibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {

        private readonly IUnitOfWork _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<AuthorsController> _logger;

        public AuthorsController(IUnitOfWork unitOfWork,
                                           IMapper _mapper,
                                            ILogger<AuthorsController> logger)
        {
            this._repository = unitOfWork;
            this._mapper = _mapper;
            this._logger = logger;
        }
        /// <summary>
        /// Retrieves a paginated list of authors.
        /// </summary>
        /// <param name="pageNumber">The page number to retrieve. Defaults to 1.</param>
        /// <param name="pageSize">The number of authors per page. Defaults to 10.</param>
        /// <returns>A paginated result containing a list of author details.</returns>
        /// <response code="200">Returns the paginated list of authors.</response>
        /// <response code="400">Returns a BadRequest if the request parameters are invalid.</response>
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var authors = await _repository.authors.GetAllAsync(pageNumber, pageSize);

            var authorsDto = _mapper.Map<PagedResult<AuthorDto>>(authors);
            return Ok(authorsDto);


        }
        /// <summary>
        /// Retrieves an author by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the author.</param>
        /// <returns>
        /// Returns the author details if found, otherwise returns a NotFound result.
        /// </returns>
        /// <response code="200">Returns the details of the author if found.</response>
        /// <response code="404">Returns a NotFound result if no author with the given ID is found.</response>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var  Author = await _repository.authors.GetByIdAsync(id);

            if (Author == null)
            {
                return NotFound($"Author with Id {id} not found.");
            }

            var authorDto = _mapper.Map<AuthorDto>(Author);

            return Ok(authorDto);
        }
        /// <summary>
        /// Adds a new author to the system.
        /// </summary>
        /// <param name="AuthorForCreateDto">The details of the author to be added.</param>
        /// <returns>
        /// Returns a CreatedAtAction result with the newly created author details if the addition is successful.
        /// </returns>
        /// <response code="201">Returns the details of the newly created author, along with a location header pointing to the new resource.</response>
        /// <response code="400">Returns a BadRequest if the input data is invalid.</response>
        [HttpPost]
        public async Task<ActionResult> Add(AuthorForCreateDto authorDto)
        {
            var Author = _mapper.Map<Author>(authorDto);

            await _repository.authors.CreateAsync(Author);
            await _repository.SaveAsync();

            _logger.LogInformation("author added successfully");

            var authorDTo = _mapper.Map<AuthorForCreateDto>(Author);

            return CreatedAtAction(nameof(GetById), new { id = Author.AuthorId }, authorDTo);

        }

        /// <summary>
        /// Updates an existing author in the system.
        /// </summary>
        /// <param name="id">The unique identifier of the author to be updated.</param>
        /// <param name="model">The updated details of the author.</param>
        /// <returns>
        /// Returns NoContent if the update is successful. 
        /// Returns NotFound if the author with the specified ID does not exist.
        /// </returns>
        /// <response code="204">Indicates that the update was successful and there is no content to return.</response>
        /// <response code="404">Indicates that the author with the specified ID was not found.</response>
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] AuthorDto model)
        {
            var Author = await _repository.authors.GetByIdAsync(id);

            if (Author == null)
            {
                return NotFound($"Author with Id {id} not found.");
            }

            _mapper.Map(model, Author);
            await _repository.authors.UpdateAsync(Author);
            Author.UpdatedAt = DateTime.UtcNow;
            await _repository.SaveAsync();


            _logger.LogInformation($"Author with Id {id} updated successfully", DateTime.UtcNow);

            return NoContent();

        }

        /// <summary>
        /// Marks an author as deleted without actually removing it from the database.
        /// </summary>
        /// <param name="id">The unique identifier of the author to be soft-deleted.</param>
        /// <returns>
        /// Returns NoContent if the soft delete operation is successful. 
        /// Returns NotFound if the author with the specified ID does not exist.
        /// </returns>
        /// <response code="204">Indicates that the soft delete was successful and there is no content to return.</response>
        /// <response code="404">Indicates that the author with the specified ID was not found.</response>

        [HttpDelete("{id}")]
        public async Task<ActionResult> SoftDelete(int id)
        {

            var Author = await _repository.authors.GetByIdAsync(id);

            if (Author == null)
            {
                return NotFound($"Author with Id {id} not found.");
            }

            Author.IsDeleted = true;

            await _repository.authors.UpdateAsync(Author);

            await _repository.SaveAsync();

            _logger.LogInformation($"Author with Id {id} deleted successfully.");

            return NoContent();

        }

    }
}
