﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Shared;
using ToDo.Shared.Contact;
using ToDo.Shared.Parameters;

namespace ToDo.Services.ServiceImpl
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class
    {
        private readonly HttpRestClient httpRestClient;
        private readonly string serviceName;

        public BaseService(HttpRestClient httpRestClient, string serviceName)
        {
            this.httpRestClient = httpRestClient;
            this.serviceName = serviceName;
        }

        public async Task<ApiResponseShared<TEntity>> AddAsync(TEntity entity)
        {
            BaseRequest request = new BaseRequest();
            request.Method = RestSharp.Method.Post;
            request.Route = $"api/{serviceName}/Add";
            request.Parameter = entity;
            return await httpRestClient.ExecuteAsyncx<TEntity>(request);
        }

        public async Task<ApiResponseShared> DeletedAsync(int id)
        {
            BaseRequest request = new BaseRequest();
            request.Method = RestSharp.Method.Delete;
            request.Route = $"api/{serviceName}/Delete?id={id}";
            return await httpRestClient.ExecuteAsyncx(request);
        }

        public async Task<ApiResponseShared<TEntity>> GetFirstOfDefaultAsync(int id)
        {
            BaseRequest request = new BaseRequest();
            request.Method = RestSharp.Method.Get;
            request.Route = $"api/{serviceName}/GetById?id={id}";
            return await httpRestClient.ExecuteAsyncx<TEntity>(request);
        }

        public async Task<ApiResponseShared<PagedList<TEntity>>> GetPageListAsync(QueryParameter parameter)
        {
            BaseRequest request = new BaseRequest();
            request.Method = RestSharp.Method.Post;
            request.Route = $"api/{serviceName}/GetPageList?pageIndex={parameter.PageIndex}" +
                $"&pageSize={parameter.PageSize}" +
                $"&search={parameter.Search}";
            return await httpRestClient.ExecuteAsyncx<PagedList<TEntity>>(request);
        }


        public async Task<ApiResponseShared<TEntity>> UpdateAsync(TEntity entity)
        {
            BaseRequest request = new BaseRequest();
            request.Method = RestSharp.Method.Post;
            request.Route = $"api/{serviceName}/Update";
            request.Parameter = entity;
            return await httpRestClient.ExecuteAsyncx<TEntity>(request);
        }
    }
}
