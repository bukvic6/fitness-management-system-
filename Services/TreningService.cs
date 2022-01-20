using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SR22_2020_POP2021.Model;

namespace SR22_2020_POP2021.Services
{
    class TreningService: ITreningService
    {
        public void ReadTrening()
        {
            Util.Instance.Treninzi = new ObservableCollection<Trening>();
            

            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();
                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"select * from Trening";
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Util.Instance.Treninzi.Add(new Trening
                    {
                        Id = reader.GetInt32(0),
                        Datum = reader.GetDateTime(1),
                        VremeTreninga = reader.GetString(2),
                        TrajanjeTreninga = reader.GetInt32(3),
                        StatusTreninga = (EStatusTreninga)Enum.Parse(typeof(EStatusTreninga), reader.GetString(4)),
                        InstruktorJmbg = reader.GetString(5),
                        
                        Aktivan = reader.GetBoolean(7),

                        

                    }) ;
                }

            }
        }
        public int SaveTrening(Object obj) {
            Trening trening = obj as Trening;
            using(SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();
                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"insert into dbo.Trening(datumTreninga,vremePocetka,trajanjeTreninga,statusTreninga,jmbgInstruktora,aktivan)
output inserted.id VALUES(@datumTreninga,@vremePocetka,@trajanjeTreninga,@statusTreninga,@jmbgInstruktora,@aktivan)";
                command.Parameters.Add(new SqlParameter("datumTreninga", trening.Datum));
                command.Parameters.Add(new SqlParameter("vremePocetka", trening.VremeTreninga));
                command.Parameters.Add(new SqlParameter("trajanjeTreninga", trening.TrajanjeTreninga));
                command.Parameters.Add(new SqlParameter("statusTreninga", trening.StatusTreninga.ToString()));
                command.Parameters.Add(new SqlParameter("jmbgInstruktora", trening.InstruktorJmbg));
               
                command.Parameters.Add(new SqlParameter("aktivan", trening.Aktivan));



                return (int)command.ExecuteScalar();
            }
        }
    }
}
