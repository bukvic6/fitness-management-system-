using SR22_2020_POP2021.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR22_2020_POP2021.Services
{
    class InstruktorService : IInstruktorService
    {

        public void ReadUsers()
        {
            Util.Instance.Instruktori = new ObservableCollection<Instruktor>();
            Util.Instance.Korisnici = new ObservableCollection<RegistrovaniKorisnik>();
            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING) )
            {
                conn.Open();
                string selectedUsers = @"select * from korisnici where TipKorisnika like 'INSTRUKTOR'";

                SqlDataAdapter adapter = new SqlDataAdapter(selectedUsers, conn);

                DataSet ds = new DataSet();
                adapter.Fill(ds, "Instruktori");

                foreach (DataRow row in ds.Tables["Instruktori"].Rows)
                {
                    Util.Instance.Korisnici.Add(new RegistrovaniKorisnik
                    {
                        Id = Convert.ToInt32(row["Id"]),
                        Ime = (string) row["Ime"],
                        Prezime = (string)row["Prezime"],
                        Email = (string)row["Email"],
                        TipKorisnika= ETipKorisnika.INSTRUKTOR,
                        Aktivan = (bool)row["Aktivan"]
                    });
                }
            }


        }

        public int SaveUser(Object obj)
        {
            Instruktor instruktor = obj as Instruktor;
            using(SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
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
