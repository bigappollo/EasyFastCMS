﻿using Abp.Application.Services;
using System.Linq;
using System.Threading.Tasks;
using EasyFast.Application.Content.Dto;
using Abp.Domain.Repositories;
using EasyFast.Core.Entities;
using System.Linq.Dynamic;
using System.Data.Entity;
using Abp.Linq.Extensions;
using EasyFast.Application.Common.Dto;
using AutoMapper.QueryableExtensions;
using System.Collections.Generic;
using Abp.AutoMapper;
using Abp.Web.Models;

namespace EasyFast.Application.Content
{
    /// <summary>
    /// 内容资源
    /// </summary>
    public class ContentAppService : ApplicationService, IContentAppService
    {

        private readonly IRepository<Common_Model> _commonModelRepository;

        public ContentAppService(IRepository<Common_Model> modelRepository)
        {
            _commonModelRepository = modelRepository;
        }

        /// <summary>
        /// 删除内容
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task DeleteContent(int[] ids)
        {
            await _commonModelRepository.DeleteAsync(o => ids.Contains(o.Id));
        }

        /// <summary>
        /// 分页并搜索获取内容信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [DontWrapResult]
        public async Task<EasyUIGridOutput<GridContentOutput>> GetGridContents(DataGridInput input)
        {
            var query = _commonModelRepository.GetAll()
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), o => o.Title.Contains(input.Filter) || o.Info.Contains(input.Filter))
                .WhereIf(input.ColumnId.HasValue, o => o.ColumnId == input.ColumnId);
            var count = await query.CountAsync();
            var list = await query.OrderBy($"{input.Sort} {input.Order}").Skip((input.Page - 1) * input.Rows).Take(input.Rows).ProjectTo<GridContentOutput>().ToListAsync();
            return new EasyUIGridOutput<GridContentOutput> { total = count, rows = list };
        }

        /// <summary>
        /// 根据id获取内容
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<AddContentDto> GetContent(int id)
        {
            var model = await _commonModelRepository.GetAsync(id);
            return model.MapTo<AddContentDto>();
        }


        public async Task<List<GenerateContentOutput>> GetGenerateContentsByCIds(List<int> ids)
        {
            return
                await _commonModelRepository.GetAll()
                    .Where(o => ids.Contains(o.ColumnId) && o.Column.IsContentHtml)
                    .ProjectTo<GenerateContentOutput>()
                    .ToListAsync();
        }

    }
}
