using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SR22_2020_POP2021.Model;

namespace SR22_2020_POP2021.Services
{
    class AdresaService : IAdresaService
    {

        public int SaveAdresa(Object obj)
        {
            Adresa adresa = obj as Adresa;
            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();
                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"insert into dbo.Adrese(id,ulica,broj,drzava,grad) output inserted.id VALUES(@Id,@Ulica,@Broj,@Drzava,@Grad) ";
                command.Parameters.Add(new SqlParameter("Id", adresa.Id));
                command.Parameters.Add(new SqlParameter("Ulica", adresa.Ulica));
                command.Parameters.Add(new SqlParameter("Broj", adresa.Broj));
                command.Parameters.Add(new SqlParameter("Drzava", adresa.Drzava));
                command.Parameters.Add(new SqlParameter("Grad", adresa.Grad));

                return (int)command.ExecuteScalar();
            }
        }
        public void ReadAdresa()
        {
            Util.Instance.Adrese = new ObservableCollection<Adresa>();
            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();
                SqlCommand command = new SqlCommand("select * from adrese", conn);
                SqlDataReader sqlDataReader = command.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    int id = Convert.ToInt32(sqlDataReader.GetValue(0));
                    string ulica = sqlDataReader.GetValue(1).ToString();
                    string broj = sqlDataReader.GetValue(2).ToString();
                    string drzava = sqlDataReader.GetValue(3).ToString();
                    string grad = sqlDataReader.GetValue(4).ToString();

                    Adresa adresa = new Adresa(id, ulica, broj, drzava, grad);
                    Util.Instance.Adrese.Add(adresa);
                }
            }


        }
    }
}
