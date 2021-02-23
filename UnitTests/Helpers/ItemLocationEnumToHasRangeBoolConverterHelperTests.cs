using NUnit.Framework;

using Game.Services;
using Game.Models;
using Game.Helpers;
using System;
using System.Linq;

namespace UnitTests.Helpers
{
    [TestFixture]
    public class ItemLocationEnumToHasRangeBoolConverterHelperTests
    {

        [Test]
        public void ItemLocationEnumToHasRangeBoolConverterHelper_Convert_ItemLocationEnum_PrimaryHand_Should_Retrun_True()
        {
            // Arrange
            var myConverter = new ItemLocationToHasRangeBoolConverterHelper();
            var myObject = ItemLocationEnum.PrimaryHand;

            // Act
            var result = myConverter.Convert(myObject, null, null, null);

            // Reset

            // Assert
            Assert.AreEqual(result, true);
        }

        [Test]
        public void ItemLocationEnumToHasRangeBoolConverterHelper_Convert_ItemLocationEnum_Head_Should_Return_False()
        {
            // Arrange
            var myConverter = new ItemLocationToHasRangeBoolConverterHelper();
            var myObject = ItemLocationEnum.Head;

            // Act
            var result = myConverter.Convert(myObject, null, null, null);

            // Reset

            // Assert
            Assert.AreEqual(result, false);
        }

        [Test]
        public void ItemLocationEnumToHasRangeBoolConverterHelper_Convert_Other_Should_Skip()
        {
            // Arrange
            var myConverter = new ItemLocationToHasRangeBoolConverterHelper();
            var myObject = new ItemModel();

            // Act
            var result = myConverter.Convert(myObject, null, null, null);

            // Reset

            // Assert
            Assert.AreEqual(result, 0);
        }

        [Test]
        public void ItemLocationEnumToHasRangeBoolConverterHelper_ConvertBack_Should_Return_Null()
        {
            // Arrange
            var myConverter = new ItemLocationToHasRangeBoolConverterHelper();
            var myObject = true;

            // Act
            var result = myConverter.ConvertBack(myObject, null, null, null);

            // Reset

            // Assert
            Assert.IsNull(result);
        }
    }
}