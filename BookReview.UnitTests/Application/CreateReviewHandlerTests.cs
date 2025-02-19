using Bogus;
using BookReview.Application.Commads.ReviewCommans.Create;
using BookReview.Core.Entity;
using BookReview.Core.Repositories;
using BookReview.UnitTests.Fakes;
using NSubstitute;

namespace BookReview.UnitTests.Application
{
    public class CreateReviewHandlerTests
    {
        [Fact]
        public async Task InputDataAreOk_Insert_Success()
        {
            // Arrange
            const int ID = 1;

            var repostitory = Substitute.For<IBookRepository>();
            repostitory.AddReview(Arg.Any<Review>()).Returns(Task.FromResult(ID));

            var fakeBook = FakeDataHelper.CreateFakeBook();

            repostitory.GetByIdAsync(Arg.Any<int>()).Returns(Task.FromResult(fakeBook));

            //var command = new CreateReviewCommand("Descrição teste", 1, 1, 5, DateTime.UtcNow);

            var command = FakeDataHelper.CreateFakeReviewCommand();

            var handler = new CreateReviewCommandHandler(repostitory);

            // Act
            var result = await handler.Handle(command, new CancellationToken());

            // Assert
            Assert.True(result.IsSuccess);

        }
    }
}
