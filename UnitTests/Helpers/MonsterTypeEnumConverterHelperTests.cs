using NUnit.Framework;

using Game.Services;
using Game.Models;
using Game.Helpers;
using System;
using System.Linq;

namespace UnitTests.Helpers
{
    [TestFixture]
    public class MonsterTypeEnumConverterHelperTests
    {

        [Test]
        public void MonsterTypeEnumConverterHelper_Convert_String_Fire_Should_Pass()
        {
            // Arrange
            var myConverter = new MonsterTypeEnumConverterHelper();
            var myObject = "Fire";

            // Act
            var result = myConverter.Convert(myObject, null, null, null);

            // Reset

            // Assert
            Assert.AreEqual(result, "Fire");
        }

        [Test]
        public void MonsterTypeEnumConverterHelper_Convert_String_Unknown_Should_Pass()
        {
            // Arrange
            var myConverter = new MonsterTypeEnumConverterHelper();
            var myObject = "Some Random String";

            // Act
            var result = myConverter.Convert(myObject, null, null, null);

            // Reset

            // Assert
            Assert.AreEqual(result, "Unknown");
        }

        [Test]
        public void MonsterTypeEnumConverterHelper_Convert_Enum_Fire_Should_Pass()
        {
            // Arrange
            var myConverter = new MonsterTypeEnumConverterHelper();
            var myObject = MonsterTypeEnum.Fire;

            // Act
            var result = myConverter.Convert(myObject, null, null, null);

            // Reset

            // Assert
            Assert.AreEqual(result, "Fire");
        }

        [Test]
        public void MonsterTypeEnumConverterHelper_Convert_Other_Should_Skip()
        {
            // Arrange
            var myConverter = new MonsterTypeEnumConverterHelper();
            var myObject = new ItemModel();

            // Act
            var result = myConverter.Convert(myObject, null, null, null);

            // Reset

            // Assert
            Assert.AreEqual(result, 0);
        }

        [Test]
        public void MonsterTypeEnumConverterHelper_ConvertBack_Int_Should_Pass()
        {
            // Arrange
            var myConverter = new MonsterTypeEnumConverterHelper();
            var myObject = 10;

            // Act
            var result = myConverter.ConvertBack(myObject, typeof(MonsterTypeEnum), null, null);

            // Reset

            // Assert
            Assert.AreEqual(result, "Fire");
        }

        [Test]
        public void MonsterTypeEnumConverterHelper_ConvertBack_String_Should_Pass()
        {
            // Arrange
            var myConverter = new MonsterTypeEnumConverterHelper();
            var myObject = "Fire";

            // Act
            var result = myConverter.ConvertBack(myObject, typeof(MonsterTypeEnum), null, null);

            // Reset

            // Assert
            Assert.AreEqual(result, MonsterTypeEnum.Fire);
        }

        [Test]
        public void MonsterTypeEnumConverterHelper_ConvertBack_Enum_Should_Skip()
        {
            // Arrange
            var myConverter = new MonsterTypeEnumConverterHelper();
            var myObject = MonsterTypeEnum.Fire;

            // Act
            var result = myConverter.ConvertBack(myObject, typeof(MonsterTypeEnum), null, null);

            // Reset

            // Assert
            Assert.AreEqual(result, 0);
        }

        [Test]
        public void MonsterTypeEnumConverterHelper_ConvertBack_Other_Should_Skip()
        {
            // Arrange
            var myConverter = new MonsterTypeEnumConverterHelper();
            var myObject = new ItemModel();

            // Act
            var result = myConverter.ConvertBack(myObject, typeof(MonsterTypeEnum), null, null);

            // Reset

            // Assert
            Assert.AreEqual(result, 0);
        }
    }
}