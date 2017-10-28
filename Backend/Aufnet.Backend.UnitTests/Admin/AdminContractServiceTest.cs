using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Aufnet.Backend.ApiServiceShared.Models.Merchant;
using Aufnet.Backend.Data.Context;
using Aufnet.Backend.Data.Models.Entities.Merchants;
using Aufnet.Backend.Data.Models.Entities.Shared;
using Aufnet.Backend.Data.Repository;
using Aufnet.Backend.Services.Admin.Merchants;
using Aufnet.Backend.Services.Shared;
using Aufnet.Backend.UnitTests.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;

namespace Aufnet.Backend.UnitTests.Admin
{
    [TestFixture]
    public class AdminContractServiceTest
    {
        [Test]
        public async Task CreateContractForMerchant()
        {
            MerchantCreateDto mc = new MerchantCreateDto()
            {
                Abn = "51824753556",
                Address = new Address()
                {
                    Country = "Australia",
                    State = "NSW",
                    PostCode = "2117",
                    Street = "15 Station St.",
                    Unit = "4"
                },
                BusinessName = "A dummy business name",
                CategoryId = 1234567,
                ContractStartDate = DateTime.Now,
                Email = "m.kamrani@hotmail.com",
                OwnerName = "gholi",
                Phone = "0452574520"
            };

            var dbConText = TestDbContext.GetContext();
            dbConText.Categories.Add(new Category()
            {
                Id = 1234567,
                DisplayName = "DummyCategory"
            });
            dbConText.SaveChanges();

            EfRepository<Merchant> merRepository =
                new EfRepository<Merchant>(dbConText);

            EfRepository<Category> catRepository =
                new EfRepository<Category>(dbConText);

            var merCatRepositoryMock = new Mock<IRepository<Category>>();
            merCatRepositoryMock.Setup(mrc => mrc.GetByIdAsync(mc.CategoryId)).ReturnsAsync(new Category()
            {
                Id = 1234567,
                DisplayName = "DummyCategory"
            });

            var service = new AdminContractService(merRepository, catRepository, null, null);

            var result = await service.CreateContractAsync(mc);
            foreach (var errorMessage in result.GetErrors())
            {
                TestContext.WriteLine(errorMessage.Message);
            }
            Assert.AreEqual(result.HasError(), false);
            
        }

        [Test]
        public async Task CreateContractShoudReturnErrorForDuplicateMerchant()
        {
            MerchantCreateDto mc = new MerchantCreateDto()
            {
                Abn = "51824753556",
                Address = new Address()
                {
                    Country = "Australia",
                    State = "NSW",
                    PostCode = "2117",
                    Street = "15 Station St.",
                    Unit = "4"
                },
                BusinessName = "A dummy business name",
                CategoryId = 1234567,
                ContractStartDate = DateTime.Now,
                Email = "m.kamrani@hotmail.com",
                OwnerName = "gholi",
                Phone = "0452574520"
            };


            var dbConText = TestDbContext.GetContext();
            dbConText.Merchants.Add(new Merchant()
            {
                Contract = new Contract()
                {
                    Abn = mc.Abn,
                    BusinessName = mc.BusinessName,
                }
            });
            dbConText.Categories.Add(new Category()
            {
                Id = 1234567,
                DisplayName = "DummyCategory"
            });
            dbConText.SaveChanges();

            EfRepository<Merchant> merRepository =
                new EfRepository<Merchant>(dbConText);

            EfRepository<Category> catRepository =
                new EfRepository<Category>(dbConText);

            var service = new AdminContractService(merRepository, catRepository, null, null);

            var result = await service.CreateContractAsync(mc);
            
            Assert.AreEqual(result.GetErrors().ToList()[0].Message, "Trying to repeat an action which is already done.");
            Assert.AreEqual(result.HasError(), true);
        }

        [Test]
        public async Task CreateContractShoudReturnErrorForNotExistingCategoryMerchant()
        {
            MerchantCreateDto mc = new MerchantCreateDto()
            {
                Abn = "51824753556",
                Address = new Address()
                {
                    Country = "Australia",
                    State = "NSW",
                    PostCode = "2117",
                    Street = "15 Station St.",
                    Unit = "4"
                },
                BusinessName = "A dummy business name",
                CategoryId = 1234567,
                ContractStartDate = DateTime.Now,
                Email = "m.kamrani@hotmail.com",
                OwnerName = "gholi",
                Phone = "0452574520"
            };

            var dbConText = TestDbContext.GetContext();
            EfRepository<Merchant> merRepository =
                new EfRepository<Merchant>(dbConText);
            EfRepository<Category> catRepository =
                new EfRepository<Category>(dbConText);

            var service = new AdminContractService(merRepository, catRepository, null, null);

            var result = await service.CreateContractAsync(mc);
            Assert.AreEqual(result.GetErrors().ToList()[0].Message.StartsWith("Invalid Argument"), true);
            Assert.AreEqual(result.HasError(), true);
        }


        [Test]
        public async Task SaveLogo()
        {
            MerchantCreateDto mc = new MerchantCreateDto()
            {
                Abn = "51824753556",
                Address = new Address()
                {
                    Country = "Australia",
                    State = "NSW",
                    PostCode = "2117",
                    Street = "15 Station St.",
                    Unit = "4"
                },
                BusinessName = "A dummy business name",
                CategoryId = 1234567,
                ContractStartDate = DateTime.Now,
                Email = "m.kamrani@hotmail.com",
                OwnerName = "gholi",
                Phone = "0452574520"
            };


            var dbConText = TestDbContext.GetContext();
            dbConText.Merchants.Add(new Merchant()
            {
                Id = 1,
                Contract = new Contract()
                {
                    Abn = mc.Abn,
                    BusinessName = mc.BusinessName,
                }
            });
            dbConText.Categories.Add(new Category()
            {
                Id = 1234567,
                DisplayName = "DummyCategory"
            });
            dbConText.SaveChanges();
            EfRepository<Merchant> merRepository =
                new EfRepository<Merchant>(dbConText);

            var fileMock = new Mock<IFormFile>();
            
            var content = "Hello World from a Fake File"; // not used
            var fileName = "test.pdf";

            fileMock.Setup(_ => _.FileName).Returns(fileName);
            fileMock.Setup(_ => _.Length).Returns(100);

            var fileManagerMock = new Mock<IFileManager>();


            var service = new AdminContractService(merRepository, null, fileManagerMock.Object, null);


            var result = await service.SaveLogoAsync(1, fileMock.Object);
            foreach (var errorMessage in result.GetErrors())
            {
                TestContext.WriteLine(errorMessage.Message);
            }
            Assert.AreEqual(result.HasError(), false);
        }
    }
}
