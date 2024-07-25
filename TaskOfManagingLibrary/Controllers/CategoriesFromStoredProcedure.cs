using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using TaskOfManagingLibrary.DTOs.Domain.Models;
using AutoMapper;
using Domain.Interfaces;
using TaskOfManagingLibrary.DTOs.SubCategoryDtos;

namespace TaskOfManagingLibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesFromStoredProcedure : ControllerBase
    {

        private readonly IUnitOfWork _repository;
        private readonly IMapper _mapper;

        public CategoriesFromStoredProcedure(IUnitOfWork unitOfWork, IMapper _mapper)
        {
            this._repository = unitOfWork;
            this._mapper = _mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<MainCategory>>> GetAllMainCategories()
        {


            var Categories = await _repository.mainCategories.ExecuteStoredProcedureAsync("EXEC GetAllMainCategories");

            var mainCategoryDtos = _mapper.Map<List<MainCategoryDto>>(Categories);
            return Ok(mainCategoryDtos);
        }

        [HttpGet("subcategories/{mainCategoryId}")]
        public async Task<ActionResult<List<SubCategory>>> GetSubCategoriesByMainCategoryId(int mainCategoryId)
        {

            var parameter = new SqlParameter("@MainCategoryId", mainCategoryId);
            var subCategories = await _repository.subCategories.ExecuteStoredProcedureAsync("EXEC GetSubCategoriesByMainCategoryId @MainCategoryId", parameter);
            var subCategoryDtos = _mapper.Map<List<SubCategoryDto>>(subCategories);
            return Ok(subCategoryDtos);
        }
    }
}
