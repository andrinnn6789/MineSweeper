using System.Runtime.CompilerServices;
using FluentAssertions;
using Microsoft.VisualBasic.CompilerServices;
using MineSweeper.Core;
using Moq;

namespace MineSweeper.Test
{
    public class FieldTest
    {
        private Field _testee;

        public FieldTest()
        {
            _testee = new Field();
        }

        [Fact]
        public void CheckIfMinesAreHidden()
        {
            var countOfHiddenMines = _testee.RevealedMineSweeperField.Cast<string?>().Count(field => field == "+");

            countOfHiddenMines.Should().Be(40);
        }

        [Fact]
        public void CheckIfAllFieldsAreSet()
        {
            foreach (var field in _testee.RevealedMineSweeperField) field.Should().NotBeNull();
        }

        [Fact]
        public void SetMines_CheckIfCountOfFieldsAroundAreCorrect()
        {
            var fakeRandom = new Mock<Random>();
            fakeRandom.Setup(random => random.Next(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(0);
            _testee = new Field(fakeRandom.Object);

            _testee.RevealedMineSweeperField[0, 1].Should().Be("1");
            _testee.RevealedMineSweeperField[1, 0].Should().Be("1");
            _testee.RevealedMineSweeperField[1, 1].Should().Be("1");
        }
    }
}