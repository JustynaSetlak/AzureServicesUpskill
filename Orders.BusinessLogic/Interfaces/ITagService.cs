﻿using System.Threading.Tasks;
using Orders.BusinessLogic.Dtos.Tag;
using Orders.BusinessLogic.Interfaces;
using Orders.Models;
using Orders.Results;

namespace Orders.Services.Interfaces
{
    public interface ITagService : IService
    {
        Task<bool> Delete(string id);
        Task<bool> UpdateDescription(UpdateTagDto tag);
        Task<DataResult<Tag>> Get(string id);
        Task<DataResult<string>> InsertTag(CreateTagDto tagToAdd);
    }
}
