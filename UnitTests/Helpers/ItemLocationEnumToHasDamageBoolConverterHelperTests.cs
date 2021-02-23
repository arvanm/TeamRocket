using NUnit.Framework;

using Game.Services;
using Game.Models;
using Game.Helpers;
using System;
using System.Linq;

namespace UnitTests.Helpers
{
    [TestFixture]
    public class ItemLocationEnumToHasDamageBoolConverterHelperTests
    {

        [Test]
        public void ItemLocationEnumToHasDamageBoolConverterHelper_Convert_ItemLocationEnum_PrimaryHand_Should_Retrun_True()
        {
            // Arrange
            var myConverter = new ItemLocationToHasDamageBoolConverterHelper();
            var myObject = ItemLocationEnum.PrimaryHand;

            // Act
            var result = myConverter.Convert(myObject, null, null, null);

            // Reset

            // Assert
            Assert.AreEqual(result, true);
        }

        [Test]
        public void ItemLocationEnumToHasDamageBoolConverterHelper_Convert_ItemLocationEnum_Pokeball_Should_Return_True()
        {
            // Arrange
            var myConverter = new ItemLocationToHasDamageBoolConverterHelper();
            var myObject = ItemLocationEnum.Pokeball;

            // Act
            var result = myConverter.Convert(myObject, null, null, null);

            // Reset

            // Assert
            Assert.AreEqual(result, true);
        }

        [Test]
        public void ItemLocationEnumToHasDamageBoolConverterHelper_Convert_ItemLocationEnum_Head_Should_Return_False()
        {
            // Arrange
            var myConverter = new ItemLocationToHasDamageBoolConverterHelper();
            var myObject = ItemLocationEnum.Head;

            // Act
            var result = myConverter.Convert(myObject, null, null, null);

            // Reset

            // Assert
            Assert.AreEqual(result, false);
        }

        [Test]
        public void ItemLocationEnumToHasDamageBoolConverterHelper_Convert_Other_Should_Skip()
        {
            // Arrange
            var myConverter = new ItemLocationToHasDamageBoolConverterHelper();
            var myObject = new ItemModel();

            // Act
            var result = myConverter.Convert(myObject, null, null, null);

            // Reset

            // Assert
            Assert.AreEqual(result, 0);
        }

        [Test]
        public void ItemLocationEnumToHasDamageBoolConverterHelper_ConvertBack_Should_Return_Null()
        {
            // Arrange
            var myConverter = new ItemLocationToHasDamageBoolConverterHelper();
            var myObject = true;

            // Act
            var result = myConverter.ConvertBack(myObject, null, null, null);

            // Reset

            // Assert
            Assert.IsNull(result);
        }
    }
}