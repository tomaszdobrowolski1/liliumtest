using Data.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;

namespace Tests
{
    [TestClass]
    public class DataCoreTests
    {
        [TestInitialize]
        public void TestInit()
        {
            var path = System.Reflection.Assembly.GetAssembly(typeof(JsonMapper)).Location; ;
            var sourcePath = Path.Combine( Path.GetDirectoryName(path), "initialTestModelData.json");
            var targetPath = Path.Combine(Path.GetDirectoryName(path), "TestModel.json");
            var initialJson = File.ReadAllText(sourcePath); 
            System.IO.File.WriteAllText(targetPath, initialJson);
        }
        //test loading method
        [TestMethod]
        public void JsonMapperSerializesSuccessfully()
        {
            IJsonMapper jmap = new JsonMapper();
            var crew = jmap.GetCollectionFromJson<TestModel>();
            Assert.IsTrue(crew.Count == 8);
        }

        //test saving method functionality
        [TestMethod]
        public void JsonMapperSavesToJsonSuccessfully()
        {
            IJsonMapper jmap = new JsonMapper();
            var crew = jmap.GetCollectionFromJson<TestModel>();
            Assert.IsTrue(crew.Count == 8);
            var newCrewmember = new TestModel() { Base = "London", Name = "Tomasz Dobrowolski", WorkDays = new List<string>() { "Monday" },Id=-1 };
            crew.Add(newCrewmember);
            jmap.SaveCollectionToJson<TestModel>(crew);
            var crewAdded = jmap.GetCollectionFromJson<TestModel>();
            Assert.IsTrue(crewAdded.Count == 9);
            crewAdded.RemoveAll(x => x.Id == -1);
            Assert.IsTrue(crewAdded.Count == 8);
            jmap.SaveCollectionToJson<TestModel>(crewAdded);
            var crewManipulated = jmap.GetCollectionFromJson<TestModel>();
            Assert.IsTrue(crewManipulated.Count == 8);
        }
    }
}
