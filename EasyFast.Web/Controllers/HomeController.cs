﻿using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;

namespace EasyFast.Web.Controllers
{
    [AbpMvcAuthorize]
    public class HomeController : EasyFastControllerBase
    {
        public ActionResult Index()
        {
            var aaa = LocalizationManager.GetAllSources();

            var bbb = L("Column");
            return View();
        }
    }
}