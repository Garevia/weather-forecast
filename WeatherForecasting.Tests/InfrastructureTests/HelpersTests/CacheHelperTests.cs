using Microsoft.Extensions.Logging;
using Moq;
using StackExchange.Redis;
using WeatherForecasting.Common;
using WeatherForecasting.Infrastructure.Common.Helpers;

namespace WeatherForecasting.Tests.InfrastructureTests.HelpersTests;

public class CacheHelperTests
{
    private readonly Mock<IDatabase> _cacheMock;
    private readonly Mock<ILogger> _loggerMock;

    public CacheHelperTests()
    {
        _cacheMock = new Mock<IDatabase>();
        _loggerMock = new Mock<ILogger>();
    }

    [Fact]
    public async Task GetOrSetAsync_ReturnsCachedValue_WhenCacheHit()
    {
        var key = "test-key";
        var expectedResult = Result<string>.Success("cached-data");
        var cachedString = System.Text.Json.JsonSerializer.Serialize(expectedResult);

        _cacheMock.Setup(c => c.StringGetAsync(key, It.IsAny<CommandFlags>()))
                  .ReturnsAsync(cachedString);

        var result = await CacheHelper.GetOrSetAsync(
            _cacheMock.Object,
            key,
            () => Task.FromResult(Result<string>.Success("should-not-be-called")),
            TimeSpan.FromMinutes(5),
            _loggerMock.Object);

        Assert.True(result.IsSuccess);
        Assert.Equal("cached-data", result.Value);

        // Verify getData function was never called
        _cacheMock.Verify(c => c.StringSetAsync(It.IsAny<RedisKey>(), It.IsAny<RedisValue>(), It.IsAny<TimeSpan?>(), 
                                                It.IsAny<When>(), It.IsAny<CommandFlags>()), Times.Never);
    }

    [Fact]
    public async Task GetOrSetAsync_CallsGetDataAndSetsCache_WhenCacheMissAndSuccess()
    {
        var key = "test-key";
        var dataToReturn = Result<string>.Success("fresh-data");

        _cacheMock.Setup(c => c.StringGetAsync(key, It.IsAny<CommandFlags>()))
                  .ReturnsAsync(RedisValue.Null);

        _cacheMock.Setup(c => c.StringSetAsync(
            It.IsAny<RedisKey>(), 
            It.IsAny<RedisValue>(), 
            It.IsAny<TimeSpan?>(), 
            It.IsAny<When>(), 
            It.IsAny<CommandFlags>()))
            .ReturnsAsync(true);

        var getDataCalled = false;

        var result = await CacheHelper.GetOrSetAsync(
            _cacheMock.Object,
            key,
            () =>
            {
                getDataCalled = true;
                return Task.FromResult(dataToReturn);
            },
            TimeSpan.FromMinutes(5),
            _loggerMock.Object);

        Assert.True(result.IsSuccess);
        Assert.Equal("fresh-data", result.Value);
        Assert.True(getDataCalled);
    }

    [Fact]
    public async Task GetOrSetAsync_DoesNotSetCache_WhenGetDataFails()
    {
        var key = "test-key";
        var failureResult = Result<string>.Failure("Error", System.Net.HttpStatusCode.BadRequest);

        _cacheMock.Setup(c => c.StringGetAsync(key, It.IsAny<CommandFlags>()))
                  .ReturnsAsync(RedisValue.Null);

        var result = await CacheHelper.GetOrSetAsync(
            _cacheMock.Object,
            key,
            () => Task.FromResult(failureResult),
            TimeSpan.FromMinutes(5),
            _loggerMock.Object);

        Assert.False(result.IsSuccess);
        Assert.Equal("Error", result.Error.Message);

        _cacheMock.Verify(c => c.StringSetAsync(It.IsAny<RedisKey>(), It.IsAny<RedisValue>(), 
                                                It.IsAny<TimeSpan?>(), It.IsAny<When>(), It.IsAny<CommandFlags>()), Times.Never);
    }
}