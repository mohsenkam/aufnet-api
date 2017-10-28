using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aufnet.Backend.ApiServiceShared.Models.Customer;
using Aufnet.Backend.ApiServiceShared.Shared;
using Aufnet.Backend.Data.Models.Entities.Merchants;
using Aufnet.Backend.Data.Repository;
using Microsoft.EntityFrameworkCore;

namespace Aufnet.Backend.Services.Customers
{
    public class CustomerSearcheService : ICustomerSearcheService
    {
        private readonly IRepository<Product> _prRepository;
        private readonly IRepository<Merchant> _merRepository;
        private readonly IRepository<ItemBasedOffer> _iboRepository;
        private readonly IRepository<LoyaltyBasedOffer> _lboRepository;
        private readonly IRepository<QuantityBasedOffer> _qboRepository;

        public CustomerSearcheService(IRepository<Product> prRepository, IRepository<Merchant> merRepository, IRepository<LoyaltyBasedOffer> lboRepository, IRepository<ItemBasedOffer> iboRepository, IRepository<QuantityBasedOffer> qboRepository)
        {
            _prRepository = prRepository;
            _merRepository = merRepository;
            _lboRepository = lboRepository;
            _iboRepository = iboRepository;
            _qboRepository = qboRepository;
        }

        public async Task<IGetServiceResult<List<LoyaltyBasedOfferThumbnailDto>>> GetLoyaltyBasedOffersAsync(string order, SearchParams searchParams)
        {
            var serviceResult = new ServiceResult();
            var getResult = new GetServiceResult<List<LoyaltyBasedOfferThumbnailDto>>();
            try
            {
                var query = _lboRepository.Query();
                if (searchParams.CategoryId != null)
                    query = query.Where(lbo => lbo.Merchant.Contract.Category.Id == searchParams.CategoryId);
                if (!string.IsNullOrEmpty(searchParams.Postcode))
                    query = query.Where(lbo => lbo.Merchant.Contract.Address.PostCode == searchParams.Postcode);
                // todo: apply the distance
                //query = query.OrderByDescending(lbo => lbo.Transactions.Count); // Order by number of transactions
                // Assuming the first page starts at 0
                var products = await query.Skip(searchParams.Offset * searchParams.Count).Take(searchParams.Count).
                    Select(lbo => new LoyaltyBasedOfferThumbnailDto()
                    {
                        ProductsCount = lbo.OfferedProducts.Count,
                        Discount = lbo.DiscountPercentage,
                        MinTotalValue = lbo.MinTotalValue,
                        OfferId = lbo.Id,
                        TransactionTimes = lbo.TransactionTimes,
                    }).
                    ToListAsync();
                getResult.SetData(products);
                return getResult;
            }
            catch (Exception ex)
            {
                // todo: log ex
                serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.OperationFailed.Code,
                    ErrorCodesConstants.OperationFailed.Message));
                getResult.SetResult(serviceResult);
                return getResult;
            }
        }

        public async Task<IGetServiceResult<CustomerLoyaltyBasedOfferDto>> GetLoyaltyBasedOfferAsync(long id)
        {
            var serviceResult = new ServiceResult();
            var getResult = new GetServiceResult<CustomerLoyaltyBasedOfferDto>();

            try
            {
                var offer = await _lboRepository.GetByIdAsync(id);
                if (offer == null)
                {
                    serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.EntityNotFound.Code,
                        ErrorCodesConstants.EntityNotFound.Message));
                    getResult.SetResult(serviceResult);
                }
                else
                {
                    getResult.SetData(new CustomerLoyaltyBasedOfferDto()
                    {
                        MerchantName = offer.Merchant.Contract.BusinessName,
                        MerchantId = offer.Merchant.Id,
                        StartDate = offer.StartDate,
                        EndDate = offer.EndDate,
                        MinTotalValue = offer.MinTotalValue,
                        Products = offer.OfferedProducts.Select(p => new ProductSearchDto()
                        {
                            ProductName = p.Name,
                            Image = p.ImageUrl,
                            Price = p.Price,
                            MerchantName = p.Merchant.Contract.BusinessName,
                            MerchantId = p.Merchant.Id,
                            NewPrice = p.ItemBasedOffer?.NewPrice ?? p.Price
                        }).ToList(),
                        Discount = offer.DiscountPercentage,
                        TransactionTimes = offer.TransactionTimes,
                    });
                }
                return getResult;

            }
            catch (Exception ex)
            {
                // log ex
                serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.OperationFailed.Code,
                    ErrorCodesConstants.OperationFailed.Message));
                getResult.SetResult(serviceResult);
                return getResult;
            }
        }

        public async Task<IGetServiceResult<List<QuantityBasedOfferThumbnailDto>>> GetQuantityBasedOffersAsync(string order,
            SearchParams searchParams)
        {
            var serviceResult = new ServiceResult();
            var getResult = new GetServiceResult<List<QuantityBasedOfferThumbnailDto>>();
            try
            {
                var query = _qboRepository.Query();
                if (searchParams.CategoryId != null)
                    query = query.Where(qbo => qbo.Merchant.Contract.Category.Id == searchParams.CategoryId);
                if (!string.IsNullOrEmpty(searchParams.Postcode))
                    query = query.Where(qbo => qbo.Merchant.Contract.Address.PostCode == searchParams.Postcode);
                // todo: apply the distance
                //query = query.OrderByDescending(qbo => qbo.Transactions.Count); // Order by number of transactions
                // Assuming the first page starts at 0
                var products = await query.Skip(searchParams.Offset * searchParams.Count).Take(searchParams.Count).
                    Select(qbo => new QuantityBasedOfferThumbnailDto()
                    {
                        ProductsCount = qbo.OfferedProducts.Count,
                        Discount = qbo.DiscountPercentage,
                        MinTotalValue = qbo.MinTotalValue,
                        OfferId = qbo.Id
                    }).
                    ToListAsync();
                getResult.SetData(products);
                return getResult;
            }
            catch (Exception ex)
            {
                // todo: log ex
                serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.OperationFailed.Code,
                    ErrorCodesConstants.OperationFailed.Message));
                getResult.SetResult(serviceResult);
                return getResult;
            }

        }

        public async Task<IGetServiceResult<CustomerQuantityBasedOfferDto>> GetQuantityBasedOfferAsync(long id)
        {
            var serviceResult = new ServiceResult();
            var getResult = new GetServiceResult<CustomerQuantityBasedOfferDto>();

            try
            {
                var offer = await _qboRepository.GetByIdAsync(id);
                if (offer == null)
                {
                    serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.EntityNotFound.Code,
                        ErrorCodesConstants.EntityNotFound.Message));
                    getResult.SetResult(serviceResult);
                }
                else
                {
                    getResult.SetData(new CustomerQuantityBasedOfferDto()
                    {
                        MerchantName = offer.Merchant.Contract.BusinessName,
                        MerchantId = offer.Merchant.Id,
                        StartDate = offer.StartDate,
                        EndDate = offer.EndDate,
                        MinTotalValue = offer.MinTotalValue,
                        Products = offer.OfferedProducts.Select(p => new ProductSearchDto()
                        {
                            ProductName = p.Name,
                            Image = p.ImageUrl,
                            Price = p.Price,
                            MerchantName = p.Merchant.Contract.BusinessName,
                            MerchantId = p.Merchant.Id,
                            NewPrice = p.ItemBasedOffer?.NewPrice ?? p.Price
                        }).ToList(),
                        Discount = offer.DiscountPercentage
                    });
                }
                return getResult;

            }
            catch (Exception ex)
            {
                // log ex
                serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.OperationFailed.Code,
                    ErrorCodesConstants.OperationFailed.Message));
                getResult.SetResult(serviceResult);
                return getResult;
            }
        }

        public async Task<IGetServiceResult<List<ItemBasedOfferThumbnailDto>>> GetItemBasedOffersAsync(string order, SearchParams searchParams)
        {
            var serviceResult = new ServiceResult();
            var getResult = new GetServiceResult<List<ItemBasedOfferThumbnailDto>>();
            try
            {
                var query = _iboRepository.Query();
                if (searchParams.CategoryId != null)
                    query = query.Where(ibo => ibo.Merchant.Contract.Category.Id == searchParams.CategoryId);
                if (!string.IsNullOrEmpty(searchParams.Postcode))
                    query = query.Where(qbo => qbo.Merchant.Contract.Address.PostCode == searchParams.Postcode);
                // todo: apply the distance
                query = query.OrderByDescending(ibo => ibo.Product.ProductTransactionst.Count); // Order by number of transactions
                // Assuming the first page starts at 0
                var offers = await query.Skip(searchParams.Offset * searchParams.Count).Take(searchParams.Count).
                    Select(offer => new ItemBasedOfferThumbnailDto()
                    {
                        OfferId = offer.Id,
                        MerchantName = offer.Merchant.Contract.BusinessName,
                        ProductName = offer.Product.Name,
                        ProductId = offer.Product.Id,
                        ProductImage = offer.Product.ImageUrl,
                        OldPrice = offer.Product.Price,
                        NewPrice = offer.NewPrice,
                        ImageUrl = offer.Merchant.Contract.LogoUri // Here we show the merchant's logo
                    }).
                    ToListAsync();
                getResult.SetData(offers);
                return getResult;
            }
            catch (Exception ex)
            {
                // todo: log ex
                serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.OperationFailed.Code,
                    ErrorCodesConstants.OperationFailed.Message));
                getResult.SetResult(serviceResult);
                return getResult;
            }
        }

        public async Task<IGetServiceResult<CustomerItemBasedOfferDto>> GetItemBasedOfferAsync(long id)
        {
            var serviceResult = new ServiceResult();
            var getResult = new GetServiceResult<CustomerItemBasedOfferDto>();

            try
            {
                var offer = await _iboRepository.GetByIdAsync(id);
                if (offer == null)
                {
                    serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.EntityNotFound.Code,
                        ErrorCodesConstants.EntityNotFound.Message));
                    getResult.SetResult(serviceResult);
                }
                else
                {
                    getResult.SetData(new CustomerItemBasedOfferDto()
                    {
                        MerchantName = offer.Merchant.Contract.BusinessName,
                        CategryId = offer.Merchant.Contract.Category.Id,
                        CategoryName = offer.Merchant.Contract.Category.DisplayName,
                        ProductName = offer.Product.Name,
                        ProductId = offer.Product.Id,
                        ProductImage = offer.Product.ImageUrl
                    });
                }
                return getResult;
            }
            catch (Exception ex)
            {
                // todo: log ex
                serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.OperationFailed.Code,
                    ErrorCodesConstants.OperationFailed.Message));
                getResult.SetResult(serviceResult);
                return getResult;
            }
        }

        public async Task<IGetServiceResult<List<ProductSearchDto>>> SearchProductsAsync(string order, SearchParams searchParams)
        {
            var serviceResult = new ServiceResult();
            var getResult = new GetServiceResult<List<ProductSearchDto>>();

            try
            {
                var query = _prRepository.Query();
                if (searchParams.CategoryId != null)
                    query = query.Where(p => p.CategoryId == searchParams.CategoryId);
                if (!string.IsNullOrEmpty(searchParams.SearchTerm))
                {
                    query = query.Where(p => p.Name.Contains(searchParams.SearchTerm) ||
                                             p.Merchant.Contract.BusinessName.Contains(searchParams.SearchTerm));
                }
                if (searchParams.MinPrice != null)
                    query = query.Where(p => p.Price >= searchParams.MinPrice);
                if (searchParams.MaxPrice != null)
                    query = query.Where(p => p.Price <= searchParams.MaxPrice);
                if (!string.IsNullOrEmpty(searchParams.Postcode))
                    query = query.Where(p => p.Merchant.Contract.Address.PostCode == searchParams.Postcode);
                // todo: apply the distance
                switch (order)
                {
                    case "minprice":
                        query = query.OrderBy(p => p.Price);
                        break;
                    case "maxprice":
                        query = query.OrderByDescending(p => p.Price);
                        break;
                    case "popular": // Maximum rating
                        query = query.OrderByDescending(p => p.Ratings.Sum(r => r.Rate));
                        break;
                    case "bestseller": // Maximum number of transactions
                        query = query.OrderByDescending(p => p.ProductTransactionst.Count);
                        break;
                    case "newset":
                    default:
                        query = query.OrderByDescending(q => q.LastUpdatedAt);
                        break;
                }
                // Assuming the first page starts at 0
                var products = await query.Skip(searchParams.Offset * searchParams.Count).Take(searchParams.Count).
                    Select(p => new ProductSearchDto()
                    {
                        ProductName = p.Name,
                        Image = p.ImageUrl,
                        Price = p.Price,
                        MerchantName = p.Merchant.Contract.BusinessName,
                        MerchantId = p.Merchant.Id,
                        NewPrice = p.ItemBasedOffer != null ? p.ItemBasedOffer.NewPrice : p.Price
                    }).
                    ToListAsync();
                getResult.SetData(products);
                return getResult;
            }
            catch (Exception ex)
            {
                // todo: log ex
                serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.OperationFailed.Code,
                    ErrorCodesConstants.OperationFailed.Message));
                getResult.SetResult(serviceResult);
                return getResult;
            }
        }
        public async Task<IGetServiceResult<CustomerProductDto>> GetProductAsync( long id )
        {
            var serviceResult = new ServiceResult();
            var getResult = new GetServiceResult<CustomerProductDto>();

            try
            {
                var product = await _prRepository.GetByIdAsync(id);
                if (product == null)
                {
                    serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.EntityNotFound.Code,
                        ErrorCodesConstants.EntityNotFound.Message));
                    getResult.SetResult(serviceResult);
                }
                else
                {
                    getResult.SetData(new CustomerProductDto()
                    {
                        Name = product.Name,
                        OldPrice = product.Price,
                        NewPrice = product.ItemBasedOffer?.NewPrice ?? null,
                        ImageUrl = product.ImageUrl,
                        Rating = product.Ratings.Average(r => r.Rate),
                        Descripion = product.Description,
                        MerchantName = product.Merchant.Contract.BusinessName,
                        MerchantId = product.MerchantId,
                        CategoryName = product.Category.DisplayName,
                        CategoryId = product.CategoryId,
                    });
                }
                return getResult;

            }
            catch (Exception ex)
            {
                // log ex
                serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.OperationFailed.Code,
                    ErrorCodesConstants.OperationFailed.Message));
                getResult.SetResult(serviceResult);
                return getResult;
            }
        }

        public async Task<IGetServiceResult<List<MerchanSearchDto>>> SearchMerchantsAsync(SearchParams searchParams)
        {
            var serviceResult = new ServiceResult();
            var getResult = new GetServiceResult<List<MerchanSearchDto>>();

            try
            {
                var query = _merRepository.Query();
                if (!string.IsNullOrEmpty(searchParams.SearchTerm))
                {
                    query = query.Where(m => m.Contract.BusinessName.Contains(searchParams.SearchTerm));
                }
                if (searchParams.CategoryId != null)
                    query = query.Where(m => m.Contract.Category.Id == searchParams.CategoryId);
                if (!string.IsNullOrEmpty(searchParams.Postcode))
                    query = query.Where(m => m.Contract.Address.PostCode == searchParams.Postcode);
                // todo: apply the distance
                query = query.OrderByDescending(m => m.Products.Sum(p => p.Ratings.Sum(r => r.Rate))); // Order by popularity
                // Assuming the first page starts at 0
                var products = await query.Skip(searchParams.Offset * searchParams.Count).Take(searchParams.Count).
                    Select(m => new MerchanSearchDto()
                    {
                        MerchantName = m.Contract.BusinessName,
                        MerchantId = m.Id,
                        Logo = m.Contract.LogoUri,
                        Address = m.Contract.Address.Raw
                    }).
                    ToListAsync();
                getResult.SetData(products);
                return getResult;
            }
            catch (Exception ex)
            {
                // todo: log ex
                serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.OperationFailed.Code,
                    ErrorCodesConstants.OperationFailed.Message));
                getResult.SetResult(serviceResult);
                return getResult;
            }
        }

        public async Task<IGetServiceResult<CustomerMerchantDto>> GetMerchantAsync( long id )
        {
            var serviceResult = new ServiceResult();
            var getResult = new GetServiceResult<CustomerMerchantDto>();

            try
            {
                var merchant = await _merRepository.GetByIdAsync(id);
                if (merchant == null)
                {
                    serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.EntityNotFound.Code,
                        ErrorCodesConstants.EntityNotFound.Message));
                    getResult.SetResult(serviceResult);
                }
                else
                {
                    getResult.SetData(new CustomerMerchantDto()
                    {
                        MerchantName = merchant.Contract.BusinessName,
                        CategryId = merchant.Contract.Category.Id,
                        CategoryName = merchant.Contract.Category.DisplayName,
                        Logo = merchant.Contract.LogoUri,
                        Address = merchant.Contract.Address.Raw,
                        ItemBasedOffers = merchant.Products.Select(p => new ItemBasedOfferThumbnailDto()
                        {
                            OfferId = p.Id,
                            MerchantName = p.Merchant.Contract.BusinessName,
                            ProductName = p.Name,
                            ProductId = p.Id,
                            ProductImage = p.ImageUrl,
                            OldPrice = p.Price,
                            NewPrice = p.ItemBasedOffer.NewPrice,
                            ImageUrl = p.ImageUrl // Here we show the product's image
                        }).ToList(),
                        QuantityBasedOffers = merchant.QuantityBasedOffers.Select(qbo => new QuantityBasedOfferThumbnailDto()
                        {
                            OfferId = qbo.Id,
                            MinTotalValue = qbo.MinTotalValue,
                            ProductsCount = qbo.OfferedProducts.Count,
                            Discount = qbo.DiscountPercentage
                        }).ToList(),
                    });
                }
                return getResult;

            }
            catch (Exception ex)
            {
                // log ex
                serviceResult.AddError(new ErrorMessage(ErrorCodesConstants.OperationFailed.Code,
                    ErrorCodesConstants.OperationFailed.Message));
                getResult.SetResult(serviceResult);
                return getResult;
            }
        }

    }
}