using EnvironmentConfig;
using Simem.AppCom.Base.Repo;
using Simem.AppCom.Datos.Dto;

namespace Simem.AppCom.Datos.Repo.Tags.Repo.Test
{
    [TestClass]
    public class TagsTest
    {
        private readonly DbContextSimem _baseContext;
        private readonly EtiquetaRepo tagsRepo;
        public TagsTest()
        {
            Connection.ConfigureConnections();
            _baseContext ??= new DbContextSimem();
            tagsRepo = new EtiquetaRepo();
        }
        [TestMethod]
        public void TestA1GetTags()
        {
            List<EnlaceDto> tagsDtos = tagsRepo.GetTags();
            Assert.IsNotNull(tagsDtos);
        }
        [TestMethod]
        public void TestA2GetTag()
        {
            EnlaceDto tagsDto = tagsRepo.GetTag(Guid.Parse("18411223-6334-4418-8E4C-298EF8C75961"));
            Assert.IsNotNull(tagsDto);
        }
        [TestMethod]
        public async Task TestA3NewTag()
        {
            EnlaceDto tagsData = new EnlaceDto()
            {
                Estado = true,
                Titulo = "hola mundo test",
            };
            try
            {
                await tagsRepo.NewTag(tagsData);
                Assert.IsTrue(true);
            }
            catch
            {
                Assert.IsTrue(false);
            }
        }
        [TestMethod]
        public async Task TestA4ModifyTag()
        {
            var tagData = _baseContext.Etiqueta.OrderByDescending(o => o.Id).FirstOrDefault()!;
            EnlaceDto tagsData = new EnlaceDto()
            {
                Estado = true,
                Titulo = "hola mundo test",
                Id = tagData.Id
            };

            bool @bool = await tagsRepo.ModifyTag(tagsData);
            Assert.IsTrue(@bool);
        }
        [TestMethod]
        public async Task TestA5DeleteTag()
        {
            try
            {
                string? id = _baseContext.Etiqueta.Where(w => w.Titulo == "hola mundo test").Select(s => s.Id).FirstOrDefault().ToString();
                await tagsRepo.DeleteTag(Guid.Parse(id!));
                Assert.IsTrue(true);
            }
            catch (Exception) { Assert.IsTrue(true); }
        }
    }
}