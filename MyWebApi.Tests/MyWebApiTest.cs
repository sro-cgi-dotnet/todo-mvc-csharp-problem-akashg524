using System;
using Xunit;
using Moq;
using MyWebApi.Controllers;
using EFCoreDatabase;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace MyWebApi.Tests
{
    public class MyWebApiTest
    {
        [Fact]
        public void get_data_empty()
        {
            var mock_repo=new Mock<IEFCoreDatabaseContext>();
            var logger=new Mock<ILogger<NotesController>>();
            mock_repo.Setup(repo=>repo.GetData()).Returns(GetData());
            var controller=new NotesController(mock_repo.Object,logger.Object);
            var result = controller.Get();
            var viewresult = Assert.IsType<NoContentResult>(result);         
        }
        [Fact]
        
        public void get_data_something()
        {
            var mock_repo=new Mock<IEFCoreDatabaseContext>();
            var logger=new Mock<ILogger<NotesController>>();
            mock_repo.Setup(repo=>repo.GetData()).Returns(GetDataHere());
            var controller=new NotesController(mock_repo.Object,logger.Object);
            var result = controller.Get();
            var viewresult = Assert.IsType<OkObjectResult>(result);
            var model =Assert.IsAssignableFrom<List<Note>>(viewresult.Value);
            Assert.Equal(1,model.Count());
        }

        private List<Note> GetDataHere()
        {
            List<Note> notes=new List<Note>();
            notes.Add(new Note{NoteId=101,title="new_note",text="some text",pinned=false});
            return notes;
        }

        private List<Note> GetData()
        {
            List<Note> note1=new List<Note>();
            return note1;
        }
    }
}
