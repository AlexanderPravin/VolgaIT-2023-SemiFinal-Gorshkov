using Domain.VolgaIT.Entities;

namespace InfrastructureTest
{
    public class UnitTest
    {
        [Fact]
        public async Task EntityCreationTest()
        {
            var testHelper = new TestHelper();
            var UserRepository = testHelper.UnitOfWork.UserRepository;

            User user = new User
            {
                Balance = 100,
                Id = Guid.NewGuid(),
                Login = "TestLogin",
                Password = "TestPassword",
                Role = "Admin"
            };

            user.RentHistory.Add(new RentInfo
            {
                Id = Guid.NewGuid(),
                FinalPrice = 100,
                Owner = user,
                PriceOfUnit = 100,
                PriceType = "Minutes",
                TimeEnd = null,
                TimeStart = $"{DateTime.Now}",
                CurrentUser = user,
                UserId = user.Id,
            });


            user.OwnedTransport.Add(new Transport
            {
                CanBeRented = true,
                Color = "Walter White",
                DayPrice = 100,
                Description = "Description",
                Id = Guid.NewGuid(),
                Identifier = "O004KO",
                IsRentedNow = false,
                Latitude = 100,
                Longitude = 100,
                MinutePrice = 100,
                Model = "Toyota Mark 2 Chaser",
                TransportType = "Car",
                Owner = user,
                RentHistory = user.RentHistory,
            });

            user.RentHistory.First().Transport = user.OwnedTransport.First();

            await UserRepository.AddEntityAsync(user);
            await testHelper.UnitOfWork.SaveChangesAsync();

            var userFromDB = await UserRepository.GetEntityByIdAsync(user.Id.ToString());
  
            Assert.NotNull(userFromDB);
            Assert.Equal(user.Balance, userFromDB.Balance);
        }

        [Fact]
        public async void NotCorrectCreationOfEntityTest()
        {
            var TestHelper = new TestHelper();

            User user = new User
            {
                Balance = 100,
                Id = Guid.NewGuid(),
                Password = "TestPassword",
                Role = "Admin"
            };

            catch (Exception ex)
            {
                Assert.NotNull(ex);
            }

            try
            {
                await TestHelper.UnitOfWork.UserRepository.AddEntityAsync(user);
                await TestHelper.UnitOfWork.SaveChangesAsync();
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.NotNull(() => ex.Message);
            }
        }
    }
}
