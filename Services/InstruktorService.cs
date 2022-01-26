using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using SR22_2020_POP2021.Model;

namespace SR22_2020_POP2021.Services
{
    class InstruktorService : IInstruktorService

    {



        public void ReadUsers()
        {
            Util.Instance.Instruktori = new ObservableCollection<Instruktor>();
            Util.Instance.Korisnici = new ObservableCollection<RegistrovaniKorisnik>();
            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();
                
                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"select * from korisnici";
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Adresa a = new Adresa()
                    {



                        Ulica = reader.GetString(8),
                        Broj = reader.GetString(9),
                        Drzava = reader.GetString(10),

                        Grad = reader.GetString(11),


                    };
                    Util.Instance.Korisnici.Add(new RegistrovaniKorisnik
                    {
                        

                        Id = reader.GetInt32(0),
                        Ime = reader.GetString(1),
                        Prezime = reader.GetString(2),
                        Email = reader.GetString(4),
                        TipKorisnika = (ETipKorisnika)Enum.Parse(typeof(ETipKorisnika), reader.GetString(3)),
                        Aktivan = reader.GetBoolean(5),
                        JMBG = reader.GetString(6),
                        Lozinka = reader.GetString(7),
                        Pol = (EPol)Enum.Parse(typeof(EPol), reader.GetString(12)),
                        Adresa = a

                }); ;

                    //ETipKorisnika tip = (ETipKorisnika)Enum.Parse(typeof(ETipKorisnika), reader.GetString(3));
                    //Adresa adresa = new Adresa(reader.GetString(8), reader.GetString(9), reader.GetString(10), reader.GetString(11));

                    //RegistrovaniKorisnik korisnik = new RegistrovaniKorisnik(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), tip, reader.GetString(3), reader.GetString(4), reader.GetBoolean(5), reader.GetString(6), adresa);
                    //Util.Instance.Korisnici.Add(korisnik);
                }
                reader.Close();
            }
        }


        public int SaveUser(Object obj)
        {
            Instruktor instruktor = obj as Instruktor;
            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();
                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"insert into dbo.Instruktori (Id) output inserted.id VALUES(@Id)";
                command.Parameters.Add(new SqlParameter("Id", instruktor.Id));

                return (int)command.ExecuteScalar();
            }
        }

    }
}


