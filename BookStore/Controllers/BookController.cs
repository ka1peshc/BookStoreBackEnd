using BookStoreManager.Manager;
using BookStoreModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    public class BookController : ControllerBase
    {
        private readonly IBookManager manager;
        public BookController(IBookManager manager)
        {
            this.manager = manager;
        }
        [HttpPost]
        [Route("book/newBook")]
        public IActionResult NewBook([FromBody] BookModel book)
        {
            try
            {
                string result = this.manager.AddNewBook(book);
                if (result.Equals("Book Added Successful"))
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = result});
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = result });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        [HttpDelete]
        [Route("book/deleteBook")]
        public IActionResult DeleteBook(int bookId)
        {
            try
            {
                string result = this.manager.DeleteBook(bookId);
                if (result.Equals("Book Deleted Successful"))
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = result });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }
        [HttpPut]
        [Route("book/updateBook")]
        public IActionResult UpdateBook([FromBody] BookModel book)
        {
            try
            {
                string result = this.manager.UpdateBook(book);
                if (result.Equals("Update Book Successful"))
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = result });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        [HttpGet]
        [Route("book/getOneBook")]
        public IActionResult GetOneBook(int bookId)
        {
            try
            {
                IEnumerable<BookModel> result = this.manager.DisplayOneBook(bookId);
                if ((int)result.Count() != 0)
                {
                    return this.Ok(new { Status = true, Data = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "No notes found" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        [HttpGet]
        [Route("book/getAllBooks")]
        public IActionResult GetAllBooks()
        {
            try
            {
                IEnumerable<BookModel> result = this.manager.DisplayAllBooks();
                if ((int)result.Count() != 0)
                {
                    //this.logger.Info(result + Environment.NewLine + DateTime.Now);
                    return this.Ok(new { Status = true, Data = result });
                }
                else
                {
                    //this.logger.Warn(result + Environment.NewLine + DateTime.Now);
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "No notes found" });
                }
            }
            catch (Exception ex)
            {
                //this.logger.Error(ex.Message + Environment.NewLine + DateTime.Now);
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }
    }
}
