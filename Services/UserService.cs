using SR22_2020_POP2021.Model;
using SR22_2020_POP2021.MojiIzuzeci;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR22_2020_POP2021.Services
{
    public class UserService : IUserService
    {
        public void DeleteUser(string email)
        {
            RegistrovaniKorisnik registrovaniKorisnik = Util.Instance.Korisnici.ToList().Find(korisnik => korisnik.Email.Equals(email));
            if (registrovaniKorisnik == null)
            {
                throw new UserNotFoundExeption($"Ne postoji korisnik sa tim emailom");
            }
            registrovaniKorisnik.Aktivan = false;
            IzmeniKorisnika(registrovaniKorisnik);      
            
        }


        public int SaveUsers(Object obj)
        {
            RegistrovaniKorisnik korisnik = obj as RegistrovaniKorisnik;
            using(SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();
                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"insert into dbo.Korisnici(Ime, Prezime, TipKorisnika,Email,Aktivan,Lozinka,JMBG,Ulica,Broj,Drzava,Grad) 
                        output inserted.id VALUES(@ime,@prezime,@TipKorisnika,@Email,@Aktivan,@Lozinka,@JMBG,@Ulica,@Broj,@Drzava,@Grad)";
                command.Parameters.Add(new SqlParameter("Ime", korisnik.Ime));
                command.Parameters.Add(new SqlParameter("Prezime", korisnik.Prezime));
                command.Parameters.Add(new SqlParameter("TipKorisnika", korisnik.TipKorisnika.ToString()));
                command.Parameters.Add(new SqlParameter("Email", korisnik.Email));
                command.Parameters.Add(new SqlParameter("Aktivan", korisnik.Aktivan));
                command.Parameters.Add(new SqlParameter("Lozinka", korisnik.Lozinka));
                command.Parameters.Add(new SqlParameter("Jmbg", korisnik.JMBG));
                command.Parameters.Add(new SqlParameter("Ulica", korisnik.Adresa.Ulica));
                command.Parameters.Add(new SqlParameter("Broj", korisnik.Adresa.Broj));
                command.Parameters.Add(new SqlParameter("Drzava", korisnik.Adresa.Drzava));
                command.Parameters.Add(new SqlParameter("Grad", korisnik.Adresa.Grad));



                return (int)command.ExecuteScalar();

            }

            }
        
        public void ReadUsers()
        {
        }

        public void IzmeniKorisnika(object obj)
        {
            RegistrovaniKorisnik korisnik = obj as RegistrovaniKorisnik;

            using(SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();
                SqlCommand command = conn.CreateCommand();

                command.CommandText = @"update dbo.Korisnici set Aktivan = @Aktivan  where email= @email";
                command.Parameters.Add(new SqlParameter("Aktivan", korisnik.Aktivan));
                command.Parameters.Add(new SqlParameter("Email", korisnik.Email));
                command.ExecuteNonQuery();

            }
        }


    }

    
}
