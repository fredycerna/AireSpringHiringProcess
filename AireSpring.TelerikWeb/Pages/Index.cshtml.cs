using AireSpring.Domain.Models;
using AireSpring.Domain.Services;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AireSpring.TelerikWeb.Pages
{
    public class IndexModel : PageModel
    {

        public static IList<EmployeeModel> employees;
        private readonly IEmployeeService _service;

        public IndexModel(IEmployeeService service)
        {
            _service = service;
        }

        public async Task OnGet()
        {
            if (employees == null)
            {
                employees = await _service.GetEmployeeList();

            }
        }

        public JsonResult OnPostRead([DataSourceRequest] DataSourceRequest request)
        {
            return new JsonResult(employees.ToDataSourceResult(request));
        }

        public async Task<JsonResult> OnPostCreate([DataSourceRequest] DataSourceRequest request, EmployeeModel model)
        {
            if (ModelState.IsValid)
            {
                await _service.CreateEmployee(model);
                employees = await _service.GetEmployeeList();
            }


            return new JsonResult(new[] { model }.ToDataSourceResult(request, ModelState));
        }

        public async Task<JsonResult> OnPostUpdate([DataSourceRequest] DataSourceRequest request, EmployeeModel model)
        {
            await _service.UpdateEmployee(model);
            employees = await _service.GetEmployeeList();
            return new JsonResult(new[] { model }.ToDataSourceResult(request, ModelState));
        }

        public async Task<JsonResult> OnPostDestroy([DataSourceRequest] DataSourceRequest request, EmployeeModel model)
        {
            var result = await _service.DeleteEmployee(model.Id);
            employees = await _service.GetEmployeeList();
            return new JsonResult(new[] { model }.ToDataSourceResult(request, ModelState));
        }
    }
}
