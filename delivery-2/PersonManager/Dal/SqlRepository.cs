using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using Zadatak.Models;
using Zadatak.Utils;

namespace Zadatak.Dal {
    class SqlRepository : IRepository {
        private const string FirstNameParam = "@firstname";
        private const string LastNameParam = "@lastname";
        private const string AgeParam = "@age";
        private const string EmailParam = "@email";
        private const string PictureParam = "@picture";
        private const string IdPersonParam = "@idPerson";

        private const string PetNameParam = "@name";
        private const string PetAgeParam = "@age";
        private const string PetOwnerParam = "@ownerId";
        private const string PetPictureParam = "@picture";
        private const string PetIDParam = "@idPet";

        // cannot be const
        //private static readonly string cs = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
        private static readonly string cs = EncryptionUtils.Decrypt(ConfigurationManager.ConnectionStrings["cs"].ConnectionString, "fru1tc@k3");

        #region people
        public void AddPerson(Person person) {
            using (SqlConnection con = new SqlConnection(cs)) {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand()) {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(FirstNameParam, person.FirstName);
                    cmd.Parameters.AddWithValue(LastNameParam, person.LastName);
                    cmd.Parameters.AddWithValue(AgeParam, person.Age);
                    cmd.Parameters.AddWithValue(EmailParam, person.Email);
                    cmd.Parameters.Add(new SqlParameter(PictureParam, SqlDbType.Binary, person.Picture.Length) {
                        Value = person.Picture
                    });
                    SqlParameter idPerson = new SqlParameter(IdPersonParam, SqlDbType.Int) {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(idPerson);
                    cmd.ExecuteNonQuery();
                    person.IDPerson = (int) idPerson.Value;
                }
            }
        }

        public void DeletePerson(Person person) {
            using (SqlConnection con = new SqlConnection(cs)) {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand()) {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(IdPersonParam, person.IDPerson);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public IList<Person> GetPeople() {
            IList<Person> people = new List<Person>();
            using (SqlConnection con = new SqlConnection(cs)) {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand()) {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader dr = cmd.ExecuteReader()) {
                        while (dr.Read()) {
                            people.Add(ReadPerson(dr));
                        }
                    }
                }
            }
            return people;
        }

        public Person GetPerson(int idPerson) {
            using (SqlConnection con = new SqlConnection(cs)) {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand()) {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(IdPersonParam, idPerson);
                    using (SqlDataReader dr = cmd.ExecuteReader()) {
                        if (dr.Read()) {
                            return ReadPerson(dr);
                        }
                    }
                }
            }
            throw new Exception("Person does not exist");
        }

        private Person ReadPerson(SqlDataReader dr) => new Person {
            IDPerson = (int) dr[nameof(Person.IDPerson)],
            FirstName = dr[nameof(Person.FirstName)].ToString(),
            LastName = dr[nameof(Person.LastName)].ToString(),
            Age = (int) dr[nameof(Person.Age)],
            Email = dr[nameof(Person.Email)].ToString(),
            Picture = ImageUtils.ByteArrayFromSqlDataReader(dr, 5)

        };

        public void UpdatePerson(Person person) {
            using (SqlConnection con = new SqlConnection(cs)) {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand()) {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(FirstNameParam, person.FirstName);
                    cmd.Parameters.AddWithValue(LastNameParam, person.LastName);
                    cmd.Parameters.AddWithValue(AgeParam, person.Age);
                    cmd.Parameters.AddWithValue(EmailParam, person.Email);
                    cmd.Parameters.AddWithValue(IdPersonParam, person.IDPerson);
                    cmd.Parameters.Add(new SqlParameter(PictureParam, SqlDbType.Binary, person.Picture.Length) {
                        Value = person.Picture
                    });
                    cmd.ExecuteNonQuery();
                }
            }
        }
        #endregion people
        #region pets

        public void AddPet(Pet pet) {
            using (SqlConnection con = new SqlConnection(cs)) {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand()) {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(PetNameParam, pet.Name);
                    cmd.Parameters.AddWithValue(PetAgeParam, pet.Age);
                    cmd.Parameters.AddWithValue(PetOwnerParam, pet.OwnerID);
                    cmd.Parameters.Add(new SqlParameter(PetPictureParam, SqlDbType.Binary, pet.Picture.Length) {
                        Value = pet.Picture
                    });
                    SqlParameter idPet = new SqlParameter(PetIDParam, SqlDbType.Int) {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(idPet);
                    cmd.ExecuteNonQuery();
                    pet.IDPet = (int) idPet.Value;
                }
            }
        }

        public void DeletePet(Pet pet) {
            using (SqlConnection con = new SqlConnection(cs)) {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand()) {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(PetIDParam, pet.IDPet);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public IList<Pet> GetPets() {
            IList<Pet> pets = new List<Pet>();
            using (SqlConnection con = new SqlConnection(cs)) {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand()) {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader dr = cmd.ExecuteReader()) {
                        while (dr.Read()) {
                            pets.Add(ReadPet(dr));
                        }
                    }
                }
            }
            return pets;
        }

        public Pet GetPet(int idPet) {
            using (SqlConnection con = new SqlConnection(cs)) {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand()) {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(PetIDParam, idPet);
                    using (SqlDataReader dr = cmd.ExecuteReader()) {
                        if (dr.Read()) {
                            return ReadPet(dr);
                        }
                    }
                }
            }
            throw new Exception("Person does not exist");
        }

        private Pet ReadPet(SqlDataReader dr) {
            Console.WriteLine(dr);
            var IDPet = (int) dr[nameof(Pet.IDPet)];
            var Name = dr[nameof(Pet.Name)].ToString();
            var Age = (int) dr[nameof(Pet.Age)];
            var OwnerID = (int) dr[nameof(Pet.OwnerID)];
            var Picture = ImageUtils.ByteArrayFromSqlDataReader(dr, 4);
            return new Pet() {
                IDPet = IDPet,
                Name = Name,
                Age = Age,
                OwnerID = OwnerID,
                Picture = Picture
            };
        }
        //=> new Pet {
        //    IDPet = (int) dr[nameof(Pet.IDPet)],
        //    Name = dr[nameof(Pet.Name)].ToString(),
        //    Age = (int) dr[nameof(Pet.Age)],
        //    OwnerID = (int) dr[nameof(Pet.OwnerID)],
        //    Picture = ImageUtils.ByteArrayFromSqlDataReader(dr, 5)
        //};

        public void UpdatePet(Pet pet) {
            using (SqlConnection con = new SqlConnection(cs)) {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand()) {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(PetNameParam, pet.Name);
                    cmd.Parameters.AddWithValue(PetAgeParam, pet.Age);
                    cmd.Parameters.AddWithValue(PetOwnerParam, pet.OwnerID);
                    cmd.Parameters.AddWithValue(PetIDParam, pet.IDPet);
                    cmd.Parameters.Add(new SqlParameter(PetPictureParam, SqlDbType.Binary, pet.Picture.Length) {
                        Value = pet.Picture
                    });
                    cmd.ExecuteNonQuery();
                }
            }
        }
        #endregion pets
    }
}
