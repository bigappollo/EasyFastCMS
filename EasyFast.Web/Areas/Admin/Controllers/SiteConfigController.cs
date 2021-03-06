﻿using EasyFast.Application.Config;
using EasyFast.Application.Config.Dto;
using EasyFast.Web.Controllers;
using System.Threading.Tasks;
using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;

namespace EasyFast.Web.Areas.Admin.Controllers
{

    [AbpMvcAuthorize]
    public class SiteConfigController : EasyFastControllerBase
    {
        #region 依赖注入
        private ISiteConfigAppService _siteConfigAppService;
        public SiteConfigController(ISiteConfigAppService siteConfigAppService)
        {
            _siteConfigAppService = siteConfigAppService;
        }
        #endregion


        public async Task<ActionResult> SiteInfo()
        {
            var data = await _siteConfigAppService.GetSiteConfig();
            return View(data);
        }


        public async Task<ActionResult> SiteOption()
        {
            var data = await _siteConfigAppService.GetSiteConfig();
            return View(data);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateSiteConfig(SiteConfigDto dto)
        {
            await _siteConfigAppService.UpdateSiteConfig(dto);
            return Json(dto);
        }


    }
}