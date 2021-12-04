using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Diagnostics;
using Dapper;
using WebApplication2.Models.Interface;
using System.Web.Mvc;
using WebApplication2.Models;
using Microsoft.AspNet.Identity;

namespace WebApplication2.Models
{
    public class SqlDataProvider : IDataProvider
    {
        public static Func<string, IDbConnection> CreateConnection = connectionString => new SqlConnection(connectionString);

        private String _connectionString = "server=sql5052.site4now.net;initial catalog=DB_A56CDF_stpaul;uid=DB_A56CDF_stpaul_admin;password=Emad0425";

        #region
        /// <summary>
        /// Executes stored procedure by name and gets results. Mapps data to generic type TDdata.
        /// </summary>
        /// <typeparam name="TData">Type of the model</typeparam>
        /// <param name="storedProcedureName">SP name to be executed</param>
        /// <param name="parameters">Stored procedure's parameters object</param>
        /// <returns></returns>
        public IEnumerable<TData> ExecuteSpWithDapper<TData>(String storedProcedureName, Object parameters)
        {
            using (var connection = CreateConnection(ConnectionString))
            {
                return connection.Query<TData>(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public String ConnectionString
        {
            get
            {
                return _connectionString == "" ? "server=sql5052.site4now.net;initial catalog=DB_A56CDF_stpaul;uid=DB_A56CDF_stpaul_admin;password=Emad0425" : _connectionString;
            }
            set { _connectionString = value; }
        }
        /// <summary>
        /// Executes stored procedure by name and gets results. Mapps data to generic type TDdata.
        /// </summary>
        /// <typeparam name="TData">Type of the model</typeparam>
        /// <param name="storedProcedureName">SP name to be executed</param>
        /// <returns></returns>
        public IEnumerable<TData> ExecuteSpWithDapper<TData>(String storedProcedureName)
        {
            Object parameters = new { };
            return ExecuteSpWithDapper<TData>(storedProcedureName, parameters);
        }

        /// <summary>
        /// Executes stored procedure using dapper.
        /// </summary>
        /// <param name="storedProcedureName">Stored procedure name</param>
        /// <param name="parameters">Stored procedure's parameters</param>
        /// <param name="connection"></param>
        /// <param name="transaction"></param>
        /// <param name="cn"></param>
        public void ExecuteVoidSpWithDapper(string storedProcedureName, object parameters, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            if (connection == null)
            {
                using (var myConnection = CreateConnection(ConnectionString))
                {
                    myConnection.Execute(storedProcedureName, parameters, commandType: CommandType.StoredProcedure, transaction: transaction);
                }
            }
            else
            {
                connection.Execute(storedProcedureName, parameters, commandType: CommandType.StoredProcedure, transaction: transaction);
            }
        }

        /// <summary>
        /// Executes stored procedure using dapper.
        /// </summary>
        /// <param name="storedProcedureName">Stored procedure name</param>
        public void ExecuteVoidSpWithDapper(String storedProcedureName)
        {
            Object parameters = new { };
            ExecuteVoidSpWithDapper(storedProcedureName, parameters);
        }

        /// <summary>
        /// Executes stored procedure by name and gets results for a single enitty. Mapps data to generic type TDdata.
        /// </summary>
        /// <typeparam name="TData">Type of the model</typeparam>
        /// <param name="storedProcedureName">SP name to be executed</param>
        /// <param name="parameters">Stored procedure's parameters object</param>
        /// <returns></returns>
        public TData ExecuteScalarSpWithDapper<TData>(String storedProcedureName, Object parameters)
        {
            return ExecuteSpWithDapper<TData>(storedProcedureName, parameters).FirstOrDefault();
        }

        /// <summary>
        /// Executes stored procedure by name and gets results for a single enitty. Mapps data to generic type TDdata.
        /// </summary>
        /// <typeparam name="TData">Type of the model</typeparam>
        /// <param name="storedProcedureName">SP name to be executed</param>
        /// <returns></returns>
        public TData ExecuteScalarSpWithDapper<TData>(String storedProcedureName)
        {
            Object parameters = new { };
            return ExecuteScalarSpWithDapper<TData>(storedProcedureName, parameters);
        }
        #endregion

        public List<Users> GetAllUsers()
        {
            var list = ExecuteSpWithDapper<Users>("getUsers").ToList();
            return list;
        }
        public List<Events> GetAllEvents()
        {
            var list = ExecuteSpWithDapper<Events>("GetAllEvents").ToList();
            return list;
        }
        public Events GetSingleEvent(Guid eventID)
        {
            var list = ExecuteScalarSpWithDapper<Events>("GetSinglEvent", new { eventID });
            return list;
        }
        public int addGuest( string userName, Guid eventID, string firstName, string lastName, bool isDeacon)
        {

            var userID = GetUserIdByUserName(userName);

            var result = ExecuteScalarSpWithDapper<int>("AddGuestToList",
            new
            {
                eventID,
                userID,
                firstName,
                lastName,
                isDeacon
            });

            return result;
        }

        public string GetUserIdByUserName(string UserName)
        {
            var Result = ExecuteScalarSpWithDapper<string>("GetUserIDbyUserName", new { UserName });
            return Result;
        }
        public void CreateUserProfile(string userId, string FirstName, string LastName, string PhoneNumber)
        {
            ExecuteVoidSpWithDapper("CreateUser",
               new
               {
                   userId,
                   FirstName,
                   LastName,
                   PhoneNumber
               });
        }

        public List<Events> GetAllEventsForUser(string userName )
        {
            var userID = GetUserIdByUserName(userName);
            var list = ExecuteSpWithDapper<Events>("GetEventsForUser", new { userID }).ToList();
            return list;
        }

        public int DeleteSingleEvent(string userName, Guid eventID)
        {
            var userID = GetUserIdByUserName(userName);
            var Result = ExecuteScalarSpWithDapper<int>("DeleteUserEvent", new { userID, eventID });
            return Result;
        }

        public List<GuestList> GetGuestListForEvent(string userName, Guid eventID)
        {
            var userID = GetUserIdByUserName(userName);
            var list = ExecuteSpWithDapper<GuestList>("GetDetailsForUserEvent", new { userID, eventID }).ToList();
            return list;
        }

        public GuestList GetSingleGuestDetails(Guid guestID)
        {
            var result = ExecuteScalarSpWithDapper<GuestList>("GetSingleGuestDetails", new { guestID });
            return result;
        }

        public int UpdateSingleGuest(GuestList guest)
        {
            var Result = ExecuteScalarSpWithDapper<int>("UpdateSingleGuest", new { guest.GuestID, guest.FirstName, guest.LastName, guest.isDeacon });
            return Result;
        }
        public int DeleteSingleGuest(Guid guestID)
        {

            var result = ExecuteScalarSpWithDapper<int>("DeleteSingleGuest", new { guestID });
            return result;
        }
        public List<Events> GetCheckInEvents()
        {
            var list = ExecuteSpWithDapper<Events>("GetEventListForCheckin").ToList();
            return list;
        }
        public List<Events> GetAllEventsForAdmins()
        {
            var list = ExecuteSpWithDapper<Events>("GetallEventsforAdmins").ToList();
            return list;
        }
        public int CheckInGuest (Guid guestID)
        {
            var result = ExecuteScalarSpWithDapper<int>("CheckGuest", new { guestID });
            return result;
        }
        public int UnCheckInGuest(Guid guestID)
        {
            var result = ExecuteScalarSpWithDapper<int>("UnCheckGuest", new { guestID });
            return result;
        }
        public List<GuestList> GetGuestFullListForEvent( Guid eventID)
        {
            var list = ExecuteSpWithDapper<GuestList>("GetEventGuestList", new { eventID }).ToList();
            return list;
        }
        public int SaveImage(Image image)
        {
            try
            {
                var result = ExecuteScalarSpWithDapper<int>("SaveGalleryImages", new { image.ImageTitle, image.ImageData, ImageType = image.ImageType });
                return result;
            }
            catch (Exception e)
            {
                throw new Exception();
            }


        }
        public List<Image> GetGalleryImages()
        {
            var list = ExecuteSpWithDapper<Image>("GetGalleryImages").ToList();
            return list;
        }
        public Image GetHomeImage()
        {
            var image = ExecuteScalarSpWithDapper<Image>("GetHomePageImage");
            return image;
        }

        public List<Contacts> GetContacts()
        {
            var list = ExecuteSpWithDapper<Contacts>("GetContacts").ToList();
            return list;
        }
        public List<Family> GetFamilies(string UserID)
        {
            var list = ExecuteSpWithDapper<Family>("GetUserDetails", new { UserID }).ToList();
            return list;
        }
        public Address GetAddress(string UserID)
        {
            var address = ExecuteScalarSpWithDapper<Address>("GetUserAddress", new { UserID });
            return address;
        }
        public Address GetAddressById(string AddressID)
        {
            var address = ExecuteScalarSpWithDapper<Address>("GetAddressById", new { AddressID });
            return address;
        }
        public int CreateAddress(string UserID, string Street1, string Street2, string City, string State, string Zipcode)
        {
            var address = ExecuteScalarSpWithDapper<int>("AddUserAddress", new { UserID,Street1, Street2, City, State, Zipcode });
            return address;
        }

        public int UpdateAddress(string AddressID, string Street1, string Street2, string City, string State, string Zipcode)
        {
            var address = ExecuteScalarSpWithDapper<int>("UpdateUserAddress", new { AddressID, Street1, Street2, City, State, Zipcode });
            return address;
        }
        public Guid SaveNewsImage(Image image)
        {

            var result = ExecuteScalarSpWithDapper<Guid>("SaveNewsImages", new { image.ImageTitle, image.ImageData });

            return result;
        }
        public int SaveNews(News News)
        {
            var result = ExecuteScalarSpWithDapper<int>("SaveNews", new { News.NewsTitle, News.Link, News.EnglishText, News.ArabicText, News.ImageID, News.Created, News.isSpotlight });
            return result;
        }

        public List<News> GetAllNews()
        {
            var list = ExecuteSpWithDapper<News>("GetAllNews").ToList();
            return list;
        }
        public List<News> GetSpotNews()
        {
            var list = ExecuteSpWithDapper<News>("GetAllSpotNews").ToList();
            return list;
        }
        public News GetSingleNews(Guid NewsID)
        {
            var News = ExecuteScalarSpWithDapper<News>("GetSingleNews", new { NewsID });
            return News;
        }

        public int DeleteNews(Guid NewsID)
        {
            var News = ExecuteScalarSpWithDapper<int>("DeleteNews", new { NewsID });
            return News;
        }

        public int UpdateNews(News News)
        {
            News.Updated = DateTime.Now;
            var result = ExecuteScalarSpWithDapper<int>("UpdateNews", new { News.ID, News.NewsTitle, News.Link, News.isSpotlight, News.EnglishText, News.ArabicText, News.Updated, News.ImageID, News.ImageTitle, News.ImageData });
            return result;
        }
        public int MakeSpotlight(Guid NewsID)
        {
            var result = ExecuteScalarSpWithDapper<int>("MakeSpotlight", new { NewsID });
            return result;
        }
        public int RemoveMakeSpotlight(Guid NewsID)
        {
            var result = ExecuteScalarSpWithDapper<int>("RemoveMakeSpotlight", new { NewsID });
            return result;
        }

    }

}