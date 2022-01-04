using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using Zadatak.Models;
using Zadatak.Utils;

namespace Zadatak.Dal
{
    class SqlRepository : IRepository
    {
        private const string FirstNameParam = "@firstname";
        private const string LastNameParam = "@lastname";
        private const string AgeParam = "@age";
        private const string EmailParam = "@email";
        private const string PictureParam = "@picture";
        private const string IdPersonParam = "@idPerson";

        // cannot be const
        //private static readonly string cs = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
        private static readonly string cs = EncryptionUtils.Decrypt(ConfigurationManager.ConnectionStrings["cs"].ConnectionString, "fru1tc@k3");

        public void AddPerson(Person person)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(FirstNameParam, person.FirstName);
                    cmd.Parameters.AddWithValue(LastNameParam, person.LastName);
                    cmd.Parameters.AddWithValue(AgeParam, person.Age);
                    cmd.Parameters.AddWithValue(EmailParam, person.Email);
                    cmd.Parameters.Add(new SqlParameter(PictureParam, SqlDbType.Binary, person.Picture.Length)
                    {
                        Value = person.Picture
                    });
                    SqlParameter idPerson = new SqlParameter(IdPersonParam, SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(idPerson);
                    cmd.ExecuteNonQuery();
                    person.IDPerson = (int)idPerson.Value;
                }
            }
        }

        public void DeletePerson(Person person)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(IdPersonParam, person.IDPerson);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public IList<Person> GetPeople()
        {
            IList<Person> people = new List<Person>();
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            people.Add(ReadPerson(dr));
                        }
                    }
                }
            }
            return people;
        }

        public Person GetPerson(int idPerson)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(IdPersonParam, idPerson);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            return ReadPerson(dr);
                        }
                    }
                }
            }
            throw new Exception("Person does not exist");
        }

        private Person ReadPerson(SqlDataReader dr) => new Person
            {
                IDPerson = (int)dr[nameof(Person.IDPerson)],
                FirstName = dr[nameof(Person.FirstName)].ToString(),
                LastName = dr[nameof(Person.LastName)].ToString(),
                Age = (int)dr[nameof(Person.Age)],
                Email = dr[nameof(Person.Email)].ToString(),
                Picture = ImageUtils.ByteArrayFromSqlDataReader(dr, 5)

        };

        public void UpdatePerson(Person person)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(FirstNameParam, person.FirstName);
                    cmd.Parameters.AddWithValue(LastNameParam, person.LastName);
                    cmd.Parameters.AddWithValue(AgeParam, person.Age);
                    cmd.Parameters.AddWithValue(EmailParam, person.Email);
                    cmd.Parameters.AddWithValue(IdPersonParam, person.IDPerson);
                    cmd.Parameters.Add(new SqlParameter(PictureParam, SqlDbType.Binary, person.Picture.Length)
                    {
                        Value = person.Picture
                    });
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
