using Garage_2._0.Common;
using System;
using Xunit;

namespace Garage2._0.Tests
{
    public class UtilTests
    {

        #region ParkingTimMin
        [Fact]
        public void ParkingTimeMin_OunHour()
        {
            //Arrange
            var inTime = new DateTime(2022, 01, 29, 16, 13, 00, 000);
            var outTime = new DateTime(2022, 01, 29, 17, 13, 00, 000);

            //Act
            var res = Util.ParkingTimeMin(inTime, outTime);

            //Assert
            Assert.Equal(60, Math.Round(res));

        }

        [Fact]
        public void ParkingTimeMin_OunHourMilleSek()
        {
            //Arrange
            var inTime = new DateTime(2022, 01, 29, 16, 13, 00, 000);
            var outTime = new DateTime(2022, 01, 29, 17, 13, 00, 600);

            //Act
            var res = Util.ParkingTimeMin(inTime, outTime);

            //Assert
            Assert.Equal(60, Math.Round(res));

        }

        [Fact]
        public void ParkingTimeMin_OneDay()
        {
            //Arrange
            var inTime = new DateTime(2022, 01, 28, 17, 13, 00, 000);
            var outTime = new DateTime(2022, 01, 29, 17, 13, 00, 000);

            var oneDayInMin = 24 * 60;

            //Act
            var res = Util.ParkingTimeMin(inTime, outTime);

            //Assert //One
            Assert.Equal(oneDayInMin, Math.Round(res));

        }

        [Fact]
        public void ParkingTimeMin_OneYear()
        {
            //Arrange
            var inTime = new DateTime(2021, 01, 29, 17, 13, 00, 000);
            var outTime = new DateTime(2022, 01, 29, 17, 13, 00, 000);

            var oneYearInMin = 365 * 24 * 60;

            //Act
            var res = Util.ParkingTimeMin(inTime, outTime);

            //Assert //One
            Assert.Equal(oneYearInMin, Math.Round(res));

        }

        [Fact]
        public void ParkingTimeMin_HalvannanTimme()
        {
            //Arrange
            var inTime = new DateTime(2022, 01, 29, 17, 00, 00, 000);
            var outTime = new DateTime(2022, 01, 29, 18, 30, 00, 000);

            var expectedResult = 1.5 * 60;

            //Act
            var res = Util.ParkingTimeMin(inTime, outTime);

            //Assert //One
            Assert.Equal(expectedResult, Math.Round(res));

        }

        [Fact]
        public void ParkingTimeMin_Null_CheckOut()
        {
            //Arrange
            var inTime = new DateTime(2022, 01, 29, 17, 00, 00, 000);
            //DateTime? outTime = null;// new DateTime(2022, 01, 29, 18, 30, 00, 000);
            //DateTime? outTime = "";
            // Null not allowed as parameter... 
            var expectedResult = 1.5 * 60;

            //Act
            //var res = Util.ParkingTimeMin(inTime, outTime);

            //Assert //One
            //Assert.Equal(expectedResult, Math.Round(res));

        }

        [Fact]
        public void ParkingTimeMin_CheckoutBeforCheckIn()
        {
            //Arrange
            DateTime inTime = new DateTime(2022, 01, 29, 17, 00, 00, 000);
            DateTime outTime = new DateTime(2022, 01, 28, 18, 30, 00, 000);

            //Act
            //var res = Util.ParkingTimeMin(inTime, outTime);
            Action action = () => Util.ParkingTimeMin(inTime, outTime);

            //Assert
            var caughtException = Assert.Throws<ArgumentException>(action);
            Assert.Equal("Can not check out before checking in.", caughtException.Message);

        }

        [Theory]
        [InlineData(0, 0, 0)]
        [InlineData(0, 30, 30)]
        [InlineData(0, 45, 45)]
        [InlineData(1, 0, 60)]
        [InlineData(1, 30, 90)]
        [InlineData(1, 45, 105)]
        [InlineData(1, 59, 119)]
        public void ParkingTimeMin_TimeCheckes(int hour, int min, double expectedResult)
        {
            //Arrange
            DateTime inTime = new DateTime(2022, 01, 01, 00, 00, 00, 000);
            DateTime outTime = new DateTime(2022, 01, 01, hour, min, 00, 000);

            //var expectedResult = 1.5 * 60;

            //Act
            var res = Util.ParkingTimeMin(inTime, outTime);

            //Assert //One
            Assert.Equal(expectedResult, Math.Round(res));

        }
        [Theory]
        [InlineData(10, 0, 10)]
        [InlineData(10, 15, 10)]
        [InlineData(10, 29, 10)]
        [InlineData(10, 30, 10)]
        [InlineData(10, 31, 11)]
        [InlineData(10, 45, 11)]
        [InlineData(10, 59, 11)]
        public void ParkingTimeMin_Timesecunds(int min, int sec, double expectedResult)
        {
            //Arrange
            DateTime inTime = new DateTime(2022, 01, 01, 00, 00, 00, 000);
            DateTime outTime = new DateTime(2022, 01, 01, 00, min, sec, 000);

            //var expectedResult = 1.5 * 60;

            //Act
            var res = Util.ParkingTimeMin(inTime, outTime);

            //Assert //One
            Assert.Equal(expectedResult, Math.Round(res));

        }

        #endregion ParkingTimMin
        #region ParkingTimString
        [Fact]
        public void ParkingTimeString_45min()
        {
            //Arrange
            var inTime = new DateTime(2022, 01, 29, 17, 00, 00, 000);
            var outTime = new DateTime(2022, 01, 29, 17, 45, 00, 000);

            var expectedResult = "45 m ";

            //Act
            var res = Util.ParkingTimeString(inTime, outTime);

            //Assert //One
            Assert.Equal(expectedResult, res);

        }

        [Fact]
        public void ParkingTimeString_1h45min()
        {
            //Arrange
            var inTime = new DateTime(2022, 01, 29, 17, 00, 00, 000);
            var outTime = new DateTime(2022, 01, 29, 18, 45, 00, 000);

            var expectedResult = "1 t 45 m ";

            //Act
            var res = Util.ParkingTimeString(inTime, outTime);

            //Assert //One
            Assert.Equal(expectedResult, res);

        }

        [Fact]
        public void ParkingTimeString_1d1h45min()
        {
            //Arrange
            var inTime = new DateTime(2022, 01, 28, 17, 00, 00, 000);
            var outTime = new DateTime(2022, 01, 29, 18, 45, 00, 000);

            var expectedResult = "1 d 1 t 45 m ";

            //Act
            var res = Util.ParkingTimeString(inTime, outTime);

            //Assert //One
            Assert.Equal(expectedResult, res);

        }

        #endregion ParkingTimString

        #region ParkingTimCost

        [Fact]
        public void ParkingTimeCost_1h()
        {
            //Arrange
            var inTime = new DateTime(2022, 01, 29, 17, 00, 00, 000);
            var outTime = new DateTime(2022, 01, 29, 18, 00, 00, 000);
            var timeRate = 15;
            var expectedResult = timeRate; // en timme ";

            //Act
            var res = Util.ParkingTimeCost(inTime, outTime, timeRate);

            //Assert //One
            Assert.Equal(expectedResult, res);

        }

        [Fact]
        public void ParkingTimeCost_45min()
        {
            //Arrange
            var inTime = new DateTime(2022, 01, 29, 17, 00, 00, 000);
            var outTime = new DateTime(2022, 01, 29, 17, 45, 00, 000);
            var timeRate = 15.00;
            var expectedResult = 45 / 60.0 * timeRate; // 45 min ";

            //Act
            var res = Util.ParkingTimeCost(inTime, outTime, timeRate);

            //Assert //One
            Assert.Equal(expectedResult, res);

        }
        [Fact]
        public void ParkingTimeCost_1d1h45min()
        {
            //Arrange
            var inTime = new DateTime(2022, 01, 28, 17, 00, 00, 000);
            var outTime = new DateTime(2022, 01, 29, 18, 45, 00, 000);
            var timeRate = 15.00;
            var expectedResult = (24 * 60 + 60 + 45) / 60.0 * timeRate; //"1 d 1 t 45 m => timme";

            //Act
            var res = Util.ParkingTimeCost(inTime, outTime, timeRate);

            //Assert //One
            //Assert.Equal("", resString);
            Assert.Equal(expectedResult, res);

        }

        [Fact]
        public void ParkingTimeCost_1_5h()
        {
            //Arrange
            var inTime = new DateTime(2022, 01, 29, 17, 00, 00, 000);
            var outTime = new DateTime(2022, 01, 29, 18, 30, 00, 000);
            var timeRate = 15;
            var expectedResult = 1.5 * timeRate; // en timme ";

            //Act
            var res = Util.ParkingTimeCost(inTime, outTime, timeRate);

            //Assert //One
            Assert.Equal(expectedResult, res);

        }
        #endregion ParkingTimCost
    }
}