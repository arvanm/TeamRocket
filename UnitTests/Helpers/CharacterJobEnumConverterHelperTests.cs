using NUnit.Framework;

using Game.Services;
using Game.Models;
using Game.Helpers;
using System;
using System.Linq;

namespace UnitTests.Helpers
{
    [TestFixture]
    public class CharacterJobEnumConverterHelperTests
    {

        [Test]
        public void CharacterJobEnumConverterHelper_Convert_String_Pet_Lover_Should_Pass()
        {
            // Arrange
            var myConverter = new CharacterJobEnumConverterHelper();
            var myObject = "Pet Lover";

            // Act
            var result = myConverter.Convert(myObject, null, null, null);

            // Reset

            // Assert
            Assert.AreEqual(result, "Pet Lover");
        }

        [Test]
        public void CharacterJobEnumConverterHelper_Convert_String_Unknown_Should_Pass()
        {
            // Arrange
            var myConverter = new CharacterJobEnumConverterHelper();
            var myObject = "Some Random String";

            // Act
            var result = myConverter.Convert(myObject, null, null, null);

            // Reset

            // Assert
            Assert.AreEqual(result, "Player");
        }

        [Test]
        public void CharacterJobEnumConverterHelper_Convert_Enum_PetLover_Should_Pass()
        {
            // Arrange
            var myConverter = new CharacterJobEnumConverterHelper();
            var myObject = CharacterJobEnum.PetLover;

            // Act
            var result = myConverter.Convert(myObject, null, null, null);

            // Reset

            // Assert
            Assert.AreEqual(result, "Pet Lover");
        }

        [Test]
        public void CharacterJobEnumConverterHelper_Convert_Other_Should_Skip()
        {
            // Arrange
            var myConverter = new CharacterJobEnumConverterHelper();
            var myObject = new ItemModel();

            // Act
            var result = myConverter.Convert(myObject, null, null, null);

            // Reset

            // Assert
            Assert.AreEqual(result, 0);
        }

        [Test]
        public void CharacterJobEnumConverterHelper_ConvertBack_Int_Should_Pass()
        {
            // Arrange
            var myConverter = new CharacterJobEnumConverterHelper();
            var myObject = 10;

            // Act
            var result = myConverter.ConvertBack(myObject, typeof(CharacterJobEnum), null, null);

            // Reset

            // Assert
            Assert.AreEqual(result, "Pet Lover");
        }

        [Test]
        public void CharacterJobEnumConverterHelper_ConvertBack_String_Should_Pass()
        {
            // Arrange
            var myConverter = new CharacterJobEnumConverterHelper();
            var myObject = "Pet Lover";

            // Act
            var result = myConverter.ConvertBack(myObject, typeof(CharacterJobEnum), null, null);

            // Reset

            // Assert
            Assert.AreEqual(result, CharacterJobEnum.PetLover);
        }

        [Test]
        public void CharacterJobEnumConverterHelper_ConvertBack_Enum_Should_Skip()
        {
            // Arrange
            var myConverter = new CharacterJobEnumConverterHelper();
            var myObject = CharacterJobEnum.PetLover;

            // Act
            var result = myConverter.ConvertBack(myObject, typeof(CharacterJobEnum), null, null);

            // Reset

            // Assert
            Assert.AreEqual(result, 0);
        }

        [Test]
        public void CharacterJobEnumConverterHelper_ConvertBack_Other_Should_Skip()
        {
            // Arrange
            var myConverter = new CharacterJobEnumConverterHelper();
            var myObject = new ItemModel();

            // Act
            var result = myConverter.ConvertBack(myObject, typeof(CharacterJobEnum), null, null);

            // Reset

            // Assert
            Assert.AreEqual(result, 0);
        }
    }
}