using System.Threading.Tasks;
using Aufnet.Backend.Data.Context;
using Aufnet.Backend.Data.Models.Entities.Shared;
using Microsoft.EntityFrameworkCore;

namespace Aufnet.Backend.Services.Shared
{
    public interface IDataService
    {

        Task<int> CreateCategoryAsync( Category category );
        Task<Category> GetCategoryAsync( long id );
        Task<int> UpdateCategoryAsync( Category category );

        Task<int> CreateMerchantAsync( Data.Models.Entities.Merchants.Merchant merchant);

        Task<Data.Models.Entities.Merchants.Merchant> GetMerchantAsync(string abn, string userName);
        Task<Data.Models.Entities.Merchants.Merchant> GetMerchantByIdAsync( long id );

        Task<bool> MerchantExistsAsync(string abn, string userName);
        Task<int> UpdateMerchantAsync(Data.Models.Entities.Merchants.Merchant merchant);
        
    }

    public class DataService : IDataService
    {
        private readonly ApplicationDbContext _dbContext;

        public DataService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> CreateCategoryAsync(Category category)
        {
            await _dbContext.Categories.AddAsync(category);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<Category> GetCategoryAsync(long id)
        {
            return await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<int> UpdateCategoryAsync(Category category)
        {
            _dbContext.Categories.Update(category);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> CreateMerchantAsync(Data.Models.Entities.Merchants.Merchant merchant)
        {
            await _dbContext.Merchants.AddAsync(merchant);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<Data.Models.Entities.Merchants.Merchant> GetMerchantAsync(string abn, string businessName)
        {
            return await _dbContext.Merchants.FirstOrDefaultAsync(m => m.Contract.Abn == abn &&
                                                                       m.Contract.BusinessName.ToLower() ==
                                                                       businessName.ToLower());
        }

        public async Task<Data.Models.Entities.Merchants.Merchant> GetMerchantByIdAsync(long id)
        {
            return await _dbContext.Merchants.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<bool> MerchantExistsAsync( string abn, string businessName)
        {
            return await _dbContext.Merchants.AnyAsync(m => m.Contract.Abn == abn &&
                                                                       m.Contract.BusinessName.ToLower() ==
                                                                       businessName.ToLower());
        }

        public async Task<int> UpdateMerchantAsync(Data.Models.Entities.Merchants.Merchant merchant)
        {
            _dbContext.Merchants.Update(merchant);
            return await _dbContext.SaveChangesAsync();
        }
    }
}