using AutoMapper;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskOfManagingLibrary.DTOs.AuthorDtos;
using TaskOfManagingLibrary.DTOs.BookDtos;

namespace TaskOfManagingLibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IUnitOfWork _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<BooksController> _logger;

        public BooksController(IUnitOfWork unitOfWork,
                                           IMapper _mapper,
                                            ILogger<BooksController> logger)
        {
            this._repository = unitOfWork;
            this._mapper = _mapper;
            this._logger = logger;
        }

        /// <summary>
        /// Retrieves a paginated list of books.
        /// </summary>
        /// <param name="pageNumber">The page number to retrieve. Defaults to 1 if not provided.</param>
        /// <param name="pageSize">The number of books to retrieve per page. Defaults to 10 if not provided.</param>
        /// <returns>Returns a paginated list of books with their details. If no books are found, returns an empty list.</returns>

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var Books = await _repository.books.GetAllAsync(pageNumber, pageSize);

            var BooksDto = _mapper.Map<PagedResult<BookDto>>(Books);
            return Ok(BooksDto);


        }

        /// <summary>
        /// Retrieves a book by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the book.</param>
        /// <returns>Returns the book details if found. If the book is not found, returns a NotFound result.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var Book = await _repository.books.GetByIdAsync(id);

            if (Book == null)
            {
                return NotFound($"book with Id {id} not found.");
            }

            var BookDto = _mapper.Map<AuthorDto>(Book);

            return Ok(BookDto);
        }
        /// <summary>
        /// Adds a new book to the system.
        /// </summary>
        /// <param name="bookDto">The details of the book to be added.</param>
        /// <returns>Returns a CreatedAtAction response with the details of the added book if successful.</returns>
        [HttpPost]
        public async Task<ActionResult> Add(BookForCreateDto bookDto)
        {
            var Book = _mapper.Map<Book>(bookDto);

            await _repository.books.CreateAsync(Book);
            await _repository.SaveAsync();


            _logger.LogInformation("Book added successfully");

            var BookDTo = _mapper.Map<BookForCreateDto>(Book);

            return CreatedAtAction(nameof(GetById), new { id = Book.BookId }, BookDTo);

        }

        /// <summary>
        /// Updates the details of an existing book.
        /// </summary>
        /// <param name="id">The unique identifier of the book to be updated.</param>
        /// <param name="model">The updated details of the book.</param>
        /// <returns>Returns NoContent if the update is successful; otherwise,
        /// returns NotFound if the book with the specified Id is not found.</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] BookForCreateDto model)
        {
            var Book = await _repository.books.GetByIdAsync(id);

            if (Book == null)
            {
                return NotFound($"Book with Id {id} not found.");
            }

            _mapper.Map(model, Book);
            await _repository.books.UpdateAsync(Book);
            Book.UpdatedAt = DateTime.UtcNow;
            await _repository.SaveAsync();


            _logger.LogInformation($"Book with Id {id} updated successfully", DateTime.UtcNow);

            return NoContent();

        }
        /// <summary>
        /// Marks a book as deleted without physically removing it from the database.
        /// </summary>
        /// <param name="id">The unique identifier of the book to be marked as deleted.</param>
        /// <returns>Returns NoContent if the book is successfully marked as deleted; otherwise, 
        /// returns NotFound if the book with the specified Id is not found.</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> SoftDelete(int id)
        {
            var Book = await _repository.books.GetByIdAsync(id);

            if (Book == null)
            {
                return NotFound($"Book with Id {id} not found.");
            }

            Book.IsDeleted = true;

            await _repository.books.UpdateAsync(Book);

            await _repository.SaveAsync();

            _logger.LogInformation($"Book with Id {id} deleted successfully.");

            return NoContent();

        }
    }
}
