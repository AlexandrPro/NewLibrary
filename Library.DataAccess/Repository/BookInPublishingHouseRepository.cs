using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.DataAccess.EF;
using Library.Entities;
using System.Configuration;
using System.Data.SqlClient;

namespace Library.DataAccess.Repository
{
    public class BookInPublishingHouseRepository : BaseRepository<BookInPublishingHouse> 
    {
        private string ConnectionString;
        public BookInPublishingHouseRepository(ApplicationContext context) : base(context)
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["NewLibrary"].ConnectionString;//for clear ADO.NET
        }

        public List<PublishingHouse> GetBookPublishingHouses(string Id)
        {
            List<BookInPublishingHouse> bookInPublishingHouses = Context.BookInPublishingHouses.Where(b => b.Book.Id == Id).ToList();
            List<PublishingHouse> publishingHouses = new List<PublishingHouse>();
            foreach (var item in bookInPublishingHouses)
            {
                publishingHouses.Add(Context.PublishingHouses.Find(item.PublishingHouse.Id));
            }
            return publishingHouses;
        }
        public List<PublishingHouse> GetBookPublishingHousesADO(string BookId)
        {
            List<PublishingHouse> publishingHouses = new List<PublishingHouse>();

            string sqlExpression = @"SELECT [Id]
                                        ,[CreationDate]
                                        ,[Name]
                                        ,[Address]
                                    FROM[NewLibrary].[dbo].[PublishingHouses] as p
                                    JOIN(SELECT[PublishingHouse_Id]
                                        FROM[NewLibrary].[dbo].[BookInPublishingHouses]
                                        WHERE(Book_Id = '" + BookId + @"')
                                        ) as bp
                                    ON(p.[Id] = bp.[PublishingHouse_Id]); ";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows) // если есть данные
                {
                    while (reader.Read()) // построчно считываем данные
                    {
                        string id = (string)reader.GetValue(0);
                        DateTime creationDate = (DateTime)reader.GetValue(1);
                        string name = (string)reader.GetValue(2);
                        string address = (string)reader.GetValue(3);
                        
                        publishingHouses.Add(new PublishingHouse
                        {
                            Id = id,
                            CreationDate = creationDate,
                            Name = name,
                            Address = address,
                        });
                    }
                }
                reader.Close();
            }
            return publishingHouses;
        }


        public List<Book> GetPublishingHouseBooks(string Id)
        {
            List<BookInPublishingHouse> bookInPublishingHouses = Context.BookInPublishingHouses.Where(b => b.PublishingHouse.Id == Id).ToList();
            List<Book> books = new List<Book>();
            foreach (var item in bookInPublishingHouses)
            {
                books.Add(Context.Books.Find(item.Book.Id));
            }
            return books;
        }

        public List<Book> GetPublishingHouseBooksADO(string PublishingHouseId)
        {
            List<Book> books = new List<Book>();

            string sqlExpression = @"SELECT [Id]
                                        ,[CreationDate]
                                        ,[Name]
                                        ,[Author]
                                        ,[YearOfPublishing]
                                        ,[PublicationId]
                                    FROM[NewLibrary].[dbo].[Book] as p
                                    JOIN(SELECT[Book_Id]
                                        FROM[NewLibrary].[dbo].[BookInPublishingHouses]
                                        WHERE(PublishingHouse_Id = '" + PublishingHouseId + @"')
                                        ) as bp
                                    ON(p.[Id] = bp.[Book_Id]); ";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows) // если есть данные
                {
                    Context.Publications.Find();
                    while (reader.Read()) // построчно считываем данные
                    {
                        string id = (string)reader.GetValue(0);
                        DateTime creationDate = (DateTime)reader.GetValue(1);
                        string name = (string)reader.GetValue(2);
                        string author = (string)reader.GetValue(3);
                        DateTime yearOfPublishing = (DateTime)reader.GetValue(4);
                        Publication publication = Context.Publications.Find((int)reader.GetValue(5));

                        books.Add(new Book
                        {
                            Id = id,
                            CreationDate = creationDate,
                            Name = name,
                            Author = author,
                            Publication = publication,
                            YearOfPublishing = yearOfPublishing,
                        });
                    }
                }
                reader.Close();
            }
            return books;
        }
    }
}
