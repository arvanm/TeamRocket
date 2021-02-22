using NUnit.Framework;

using Game.Services;
using Game.Models;
using Game.Helpers;
using System;
using System.Linq;

namespace UnitTests.Helpers
{
    [TestFixture]
    public class ItemLocationEnumConverterHelperTests
    {

        [Test]
        public void ItemLocationEnumConverterHelper_Convert_String_Primary_Hand_Should_Pass()
        {
            // Arrange
            var myConverter = new ItemLocationEnumConverter();
            var myObject = "Primary Hand";

            // Act
            var result = myConverter.Convert(myObject, null, null, null);

            // Reset

            // Assert
            Assert.AreEqual(result, "Primary Hand");
        }

        [Test]
        public void ItemLocationEnumConverterHelper_Convert_String_Unknown_Should_Pass()
        {
            // Arrange
            var myConverter = new ItemLocationEnumConverter();
            var myObject = "Some Random String";

            // Act
            var result = myConverter.Convert(myObject, null, null, null);

            // Reset

            // Assert
            Assert.AreEqual(result, "Unknown");
        }

        [Test]
        public void ItemLocationEnumConverterHelper_Convert_Enum_Primary_Hand_Should_Pass()
        {
            // Arrange
            var myConverter = new ItemLocationEnumConverter();
            var myObject = ItemLocationEnum.PrimaryHand;

            // Act
            var result = myConverter.Convert(myObject, null, null, null);

            // Reset

            // Assert
            Assert.AreEqual(result, "Primary Hand");
        }

        [Test]
        public void ItemLocationEnumConverterHelper_Convert_Other_Should_Skip()
        {
            // Arrange
            var myConverter = new ItemLocationEnumConverter();
            var myObject = new ItemModel();

            // Act
            var result = myConverter.Convert(myObject, null, null, null);

            // Reset

            // Assert
            Assert.AreEqual(result, 0);
        }

        [Test]
        public void ItemLocationEnumConverterHelper_ConvertBack_Int_Should_Pass()
        {
            // Arrange
            var myConverter = new ItemLocationEnumConverter();
            var myObject = 20;

            // Act
            var result = myConverter.ConvertBack(myObject, typeof(ItemLocationEnum), null, null);

            // Reset

            // Assert
            Assert.AreEqual(result, "Primary Hand");
        }

        [Test]
        public void ItemLocationEnumConverterHelper_ConvertBack_String_Should_Pass()
        {
            // Arrange
            var myConverter = new ItemLocationEnumConverter();
            var myObject = "Primary Hand";

            // Act
            var result = myConverter.ConvertBack(myObject, typeof(ItemLocationEnum), null, null);

            // Reset

            // Assert
            Assert.AreEqual(result, ItemLocationEnum.PrimaryHand);
        }

        [Test]
        public void ItemLocationEnumConverterHelper_ConvertBack_Enum_Should_Skip()
        {
            // Arrange
            var myConverter = new ItemLocationEnumConverter();
            var myObject = ItemLocationEnum.PrimaryHand;

            // Act
            var result = myConverter.ConvertBack(myObject, typeof(ItemLocationEnum), null, null);

            // Reset

            // Assert
            Assert.AreEqual(result, 0);
        }

        [Test]
        public void ItemLocationEnumConverterHelper_ConvertBack_Other_Should_Skip()
        {
            // Arrange
            var myConverter = new ItemLocationEnumConverter();
            var myObject = new ItemModel();

            // Act
            var result = myConverter.ConvertBack(myObject, typeof(ItemLocationEnum), null, null);

            // Reset

            // Assert
            Assert.AreEqual(result, 0);
        }
    }
}