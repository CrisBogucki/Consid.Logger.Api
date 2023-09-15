using AutoMapper;
using Azure.Data.Tables;
using Consid.Logger.Domain.Configuration;
using Consid.Logger.Domain.Entity;
using Consid.Logger.Domain.Persist.Repository;
using Consid.Logger.Persist.AzureStorageTable.Model;

namespace Consid.Logger.Persist.AzureStorageTable.Repository;

public class RedditLogAzureStorageTableRepository : IRedditLogRepository
{
    const string PartitionKey = "RedditLog";

    private readonly IMapper _mapper;
    private readonly TableClient _table;

    public RedditLogAzureStorageTableRepository(IMapper mapper, AppConfig appConfig)
    {
        _mapper = mapper;

        TableServiceClient serviceClient = new TableServiceClient(appConfig.AzureStorage.ConnectionString);
        _table = serviceClient.GetTableClient("RedditLog");
        _table.CreateIfNotExists();
    }

    public async Task<RedditLogEntity> AddAsync(RedditLogEntity entity)
    {
        entity.Id = Guid.NewGuid();
        
        RedditLogTableEntity tableEntity = _mapper.Map<RedditLogTableEntity>(entity);
        
        tableEntity.PartitionKey = PartitionKey;
        tableEntity.RowKey = entity.Id.ToString();
        
        await _table.AddEntityAsync(tableEntity);

        return entity;
    }


    public async Task<RedditLogEntity> GetAsync(Guid id)
    {
        var tableEntity = await _table.GetEntityIfExistsAsync<RedditLogTableEntity>(PartitionKey, id.ToString());
        return tableEntity.HasValue ? _mapper.Map<RedditLogEntity>(tableEntity) : null;
    }

    public async Task<IEnumerable<RedditLogEntity>> GetAsync(DateTime from, DateTime to)
    {
        var results = new List<RedditLogEntity>();

        var query = _table.QueryAsync<RedditLogTableEntity>(x => x.Created >= from && x.Created <= to, maxPerPage: 100);
        await foreach (var entities in query.AsPages())
        {
            foreach (var entity in entities.Values)
            {
                results.Add(entity);
            }
        }
        return results;
    }
}