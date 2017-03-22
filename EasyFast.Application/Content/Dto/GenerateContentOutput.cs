﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;

namespace EasyFast.Application.Content.Dto
{
    /// <summary>
    /// 生成内容静态文件所需Dto
    /// </summary>
    public class GenerateContentOutput : EntityDto
    {
        /// <summary>
        /// 内容页规则
        /// </summary>
        public string ColumnContentHtmlRule { get; set; }

        /// <summary>
        /// 内容页模板
        /// </summary>
        public string ColumnContentTemplate { get; set; }

        /// <summary>
        /// 栏目路径
        /// </summary>
        public string ColumnDir { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
    }
}
