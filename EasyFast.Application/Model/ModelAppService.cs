﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.Web.Models;
using EasyFast.Application.Model.Dto;
using EasyFast.Application.Common.Dto;

namespace EasyFast.Application.Model
{
    /// <summary>
    /// 内容模型记录资源
    /// </summary>
    public class ModelAppService : ApplicationService, IModelAppService
    {

        private readonly IRepository<Core.Entities.Model> _modelRepository;

        public ModelAppService(IRepository<Core.Entities.Model> modelrRepository)
        {
            this._modelRepository = modelrRepository;
        }



        /// <summary>
        /// 分页获取内容模型记录基本信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [DontWrapResult]
        public async Task<EasyUIGridOutput<BasicModelOutput>> GetModels(TreeGridInput input)
        {
            var result = _modelRepository.GetAll();

            int totalCount = await result.CountAsync();
            var list = await result.OrderBy($"{input.Sort} {input.Order}").Skip((input.Page - 1) * input.Rows).ToListAsync();

            return new EasyUIGridOutput<BasicModelOutput> { total = totalCount, rows = list.MapTo<List<BasicModelOutput>>() };
        }

        /// <summary>
        /// 删除模型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteModel(int id)
        {
            //这里应该把相应的表删除
            await _modelRepository.DeleteAsync(id);
        }



        /// <summary>
        /// 创建模型
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task CreateModel(ModelDto model)
        {
            await _modelRepository.InsertAsync(model.MapTo<Core.Entities.Model>());
        }

        /// <summary>
        /// 修改模型
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task UpdateModel(ModelDto model)
        {
            var cModel = await _modelRepository.GetAsync(model.Id);
            model.MapTo(cModel);
            await _modelRepository.UpdateAsync(cModel);
        }

        /// <summary>
        /// 删除或修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task CreateOrUpdate(ModelDto model)
        {
            if (model.Id == 0)
            {
                await CreateModel(model);
            }
            else
            {
                await UpdateModel(model);
            }

        }

        /// <summary>
        /// 根据Id获取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ModelDto> GetAsync(int id)
        {
            var model = await _modelRepository.GetAsync(id);
            return model.MapTo<ModelDto>();
        }
    }
}
