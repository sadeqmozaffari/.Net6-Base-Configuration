using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using App.DataLayer.Contracts;
using App.ViewModels.DynamicAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App.Areas.Admin.Controllers
{
    [DisplayName("داشبورد")]
    public class DashboardController : BaseController
    {
        private readonly IUnitOfWork _uw;
        public DashboardController(IUnitOfWork uw)
        {
            _uw = uw;
        }

        [HttpGet,DisplayName("مشاهده")]
        [Authorize(Policy = ConstantPolicies.DynamicPermission)]
        public IActionResult Index()
        {

            return View();
        }
    }
}