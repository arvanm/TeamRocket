using NUnit.Framework;

using Game.Services;
using Game.Models;
using Game.Helpers;
using System;
using System.Linq;
using System.Collections.Generic;

namespace UnitTests.Helpers
{
    [TestFixture]
    public class MonsterTypeEnumHelperTests
    {

        [Test]
        public void MonsterTypeEnumHelper_MonsterTypeEnum_GetList_Should_Pass()
        {
            // Arrange

            // Act
            var myDataList = MonsterTypeEnumHelper.GetList;
            var myExpectedList = new List<string>()
            {
                "Fire",
                "Water",
                "Poison"
            };

            // Reset

            // Assert
            // Make sure each item is in the list
            foreach (var item in myDataList)
            {
                var found = false;
                foreach (var expected in myExpectedList)
                {
                    if (item == expected)
                    {
                        found = true;
                        break;
                    }
                }

                // Assert
                Assert.AreEqual(true, found, "item : " + item + TestContext.CurrentContext.Test.Name);
            }

            // reverse it, to make sure the list has each item
            // Make sure each item is in the list
            foreach (var expected in myExpectedList)
            {
                var found = false;
                {
                    foreach (var item in myDataList)
                        if (item == expected)
                        {
                            found = true;
                            break;
                        }
                }

                // Assert
                Assert.AreEqual(true, found, "expected : " + expected + TestContext.CurrentContext.Test.Name);
            }
        }

        [Test]
        public void MonsterTypeEnumHelper_MonsterTypeEnum_GetListMessage_Should_Pass()
        {
            // Arrange

            // Act
            var myDataList = MonsterTypeEnumHelper.GetListMessage;
            var myExpectedList = new List<string>()
            {
                "Fire",
                "Water",
                "Poison"
            };

            // Reset

            // Assert
            // Make sure each item is in the list
            foreach (var item in myDataList)
            {
                var found = false;
                foreach (var expected in myExpectedList)
                {
                    if (item == expected)
                    {
                        found = true;
                        break;
                    }
                }

                // Assert
                Assert.AreEqual(true, found, "item : " + item + TestContext.CurrentContext.Test.Name);
            }

            // reverse it, to make sure the list has each item
            // Make sure each item is in the list
            foreach (var expected in myExpectedList)
            {
                var found = false;
                {
                    foreach (var item in myDataList)
                        if (item == expected)
                        {
                            found = true;
                            break;
                        }
                }

                // Assert
                Assert.AreEqual(true, found, "expected : " + expected + TestContext.CurrentContext.Test.Name);
            }
        }

        [Test]
        public void MonsterTypeEnumHelper_ConvertStringToEnum_Should_Pass()
        {
            // Arrange
            var myList = Enum.GetNames(typeof(MonsterTypeEnum)).ToList();

            MonsterTypeEnum myActual;
            MonsterTypeEnum myExpected;

            // Act
            foreach (var item in myList)
            {
                myActual = MonsterTypeEnumHelper.ConvertStringToEnum(item);
                myExpected = (MonsterTypeEnum)Enum.Parse(typeof(MonsterTypeEnum), item);

                // Assert
                Assert.AreEqual(myExpected, myActual, "string: " + item + TestContext.CurrentContext.Test.Name);
            }
        }

        [Test]
        public void MonsterTypeEnumHelper_ConvertMessageToEnum_Should_Pass()
        {
            // Arrange
            var myList = new List<string>()
            {
                "Fire",
                "Water",
                "Poison",
                "Some Random String"
            };
            var myExpectedList = new List<MonsterTypeEnum>()
            {
                MonsterTypeEnum.Fire,
                MonsterTypeEnum.Water,
                MonsterTypeEnum.Poison,
                MonsterTypeEnum.Unknown
            };

            // Act
            for (int i = 0; i < myList.Count; i++)
            {
                MonsterTypeEnum myActual = MonsterTypeEnumHelper.ConvertMessageToEnum(myList[i]);

                // Assert
                Assert.AreEqual(myExpectedList[i], myActual);
            }
        }

        [Test]
        public void MonsterTypeEnumHelper_GetMonsterTypeEnumByPosition_1_Should_Pass()
        {
            // Arrange
            var value = 1;

            // Act
            var Actual = MonsterTypeEnumHelper.GetMonsterTypeByPosition(value);
            var Expected = MonsterTypeEnum.Fire;

            // Assert
            Assert.AreEqual(Expected, Actual, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void MonsterTypeEnumHelper_GetMonsterTypeEnumByPosition_2_Should_Pass()
        {
            // Arrange
            var value = 2;

            // Act
            var Actual = MonsterTypeEnumHelper.GetMonsterTypeByPosition(value);
            var Expected = MonsterTypeEnum.Water;

            // Assert
            Assert.AreEqual(Expected, Actual, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void MonsterTypeEnumHelper_GetMonsterTypeEnumByPosition_3_Should_Pass()
        {
            // Arrange
            var value = 3;

            // Act
            var Actual = MonsterTypeEnumHelper.GetMonsterTypeByPosition(value);
            var Expected = MonsterTypeEnum.Poison;

            // Assert
            Assert.AreEqual(Expected, Actual, TestContext.CurrentContext.Test.Name);
        }

    }
}