using System.Threading.Tasks;
using Aufnet.Backend.Api.Validation;
using Aufnet.Backend.ApiServiceShared.Models.Admin;
using Aufnet.Backend.Services.Admin.Configs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Aufnet.Backend.Api.Controllers.Admin.Configs
{
    [Route("api/adm/configs/categories")]
    public class CategoryController : BaseController
    {
        private IAdminConfigsCategoryService _adminService;

        public CategoryController(IAdminConfigsCategoryService adminService)
        {
            _adminService = adminService;
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] CreateCategoryDto createCategoryDto)
        {
            var result = await _adminService.CreateCategoryAsync(createCategoryDto);
            if (result.HasError())
            {
                foreach (var error in result.GetErrors())
                {
                    ModelState.AddModelError(error.Code, error.Message);
                }

                return new ValidationFailedResult(ModelState);
            }

            return Ok(result.GetExteraData());
        }

        [HttpPost("{id}/upload")]
        public async Task<IActionResult> AddCategoryImage(long id, IFormFile file)
        {
            var result = await _adminService.AddOrUpdateCategoryImageAsync(id, file);

            if (result.HasError())
            {
                foreach (var error in result.GetErrors())
                {
                    ModelState.AddModelError(error.Code, error.Message);
                }

                return new ValidationFailedResult(ModelState);
            }
            return Ok(result.GetExteraData());
        }

        [HttpGet("{parentId}/subcategories")]
        public async Task<IActionResult> GetAll(long parentId)
        {
            var result = await _adminService.GetCategoriesAsync(parentId);

            if (result.HasError())
            {
                foreach (var error in result.GetResult().GetErrors())
                {
                    ModelState.AddModelError(error.Code, error.Message);
                }

                return new ValidationFailedResult(ModelState);
            }

            return Ok(new { data = result.GetData() });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            var result = await _adminService.GetCategoryAsync(id);

            if (result.HasError())
            {
                foreach (var error in result.GetResult().GetErrors())
                {
                    ModelState.AddModelError(error.Code, error.Message);
                }

                return new ValidationFailedResult(ModelState);
            }
            return Ok(result.GetData());
        }

        [HttpPut("{id}/displayname")]
        public async Task<IActionResult> UpdateDisplayName(long id, [FromBody] UpdateCategoryDto updateCategoryDto )
        {
            var result = await _adminService.UpdateCategoryDisplayNameAsync(id, updateCategoryDto.NewDisplayName);

            if (result.HasError())
            {
                foreach (var error in result.GetErrors())
                {
                    ModelState.AddModelError(error.Code, error.Message);
                }

                return new ValidationFailedResult(ModelState);
            }
            return Ok();
        }
        [HttpPut("{id}/image")]
        public async Task<IActionResult> UpdateImage( long id, IFormFile file )
        {
            var result = await _adminService.AddOrUpdateCategoryImageAsync(id, file);

            if (result.HasError())
            {
                foreach (var error in result.GetErrors())
                {
                    ModelState.AddModelError(error.Code, error.Message);
                }

                return new ValidationFailedResult(ModelState);
            }
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var result = await _adminService.DeleteCategoryAsync(id);

            if (result.HasError())
            {
                foreach (var error in result.GetErrors())
                {
                    ModelState.AddModelError(error.Code, error.Message);
                }

                return new ValidationFailedResult(ModelState);
            }
            return Ok();
        }

    }
}
