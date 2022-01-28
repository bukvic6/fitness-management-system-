using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SR22_2020_POP2021.Model;
using SR22_2020_POP2021.MojiIzuzeci;

namespace SR22_2020_POP2021.Services
{
    class TreningService: ITreningService
    {
        public void DeleteTrening(int id)
        {
            Trening trening = Util.Instance.Treninzi.ToList().Find(tr => tr.Id.Equals(id));
            if (trening == null)
            {
                throw new UserNotFoundExeption($"Ne postoji");
            }
            trening.Aktivan = false;
            IzmeniTrening(trening);
        }
        public void DeleteTreningii(string jmbgInstruktora)
        {
            Trening trening = Util.Instance.Treninzi.ToList().Find(tr => tr.InstruktorJmbg.Equals(jmbgInstruktora));
            if (trening == null)
            {
                throw new UserNotFoundExeption($"Ne postoji");
            }
            trening.Aktivan = false;
            IzmeniTreningii(trening);
        }
        public void IzmeniTreningii(object obj)
        {
            Trening trening = obj as Trening;
            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();
                SqlCommand command = conn.CreateCommand();

                command.CommandText = @"update dbo.trening set Aktivan = @Aktivan  where jmbgInstruktora= @jmbgInstruktora";
                command.Parameters.Add(new SqlParameter("Aktivan", trening.Aktivan));
                command.Parameters.Add(new SqlParameter("jmbgInstruktora", trening.InstruktorJmbg));
                command.ExecuteNonQuery();

            }
        }
        public void IzmeniTrening(object obj)
        {
            Trening trening = obj as Trening;
            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();
                SqlCommand command = conn.CreateCommand();

                command.CommandText = @"update dbo.trening set Aktivan = @Aktivan  where id= @Id";
                command.Parameters.Add(new SqlParameter("Aktivan", trening.Aktivan));
                command.Parameters.Add(new SqlParameter("Id", trening.Id));
                command.ExecuteNonQuery();

            }
        }
        public void RezervisiTrening(int id)
        {
            Trening trening = Util.Instance.Treninzi.ToList().Find(tr => tr.Id.Equals(id));
            if (trening == null)
            {
                throw new UserNotFoundExeption($"Ne postoji");
            }
            trening.StatusTreninga = EStatusTreninga.REZERVISAN;
            trening.PolaznikJmbg = Util.Instance.jmbgPrijavljen7;
            IzmeniTreningZaRezervaciju(trening);
        }
        public void OtkaziTrening(int id)
        {
            Trening trening = Util.Instance.Treninzi.ToList().Find(tr => tr.Id.Equals(id));
            if (trening == null)
            {
                throw new UserNotFoundExeption($"Ne postoji");
            }
            trening.StatusTreninga = EStatusTreninga.SLOBODAN;
            trening.PolaznikJmbg = " ";
            IzmeniTreningZaRezervaciju(trening);
        }


        public void IzmeniTreningZaRezervaciju(object obj)
        {
            Trening trening = obj as Trening;
            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();
                SqlCommand command = conn.CreateCommand();

                command.CommandText = @"update dbo.trening set statusTreninga = @statusTreninga, jmbgPolaznika = @jmbgPolaznika  where id= @Id";
                command.Parameters.Add(new SqlParameter("statusTreninga", trening.StatusTreninga));
                command.Parameters.Add(new SqlParameter("jmbgPolaznika", trening.PolaznikJmbg));
                command.Parameters.Add(new SqlParameter("Id", trening.Id));
                command.ExecuteNonQuery();

            }
        }


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
                        PolaznikJmbg = reader.GetString(6),


                        Aktivan = reader.GetBoolean(7),

                        

                    }) ;
                }
                reader.Close();

            }
        }
        public int SaveTrening(Object obj) {
            Trening trening = obj as Trening;
            using(SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();
                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"insert into dbo.Trening(datumTreninga,vremePocetka,trajanjeTreninga,statusTreninga,jmbgInstruktora,jmbgPolaznika,aktivan)
output inserted.id VALUES(@datumTreninga,@vremePocetka,@trajanjeTreninga,@statusTreninga,@jmbgInstruktora,@jmbgPolaznika, @aktivan)";
                command.Parameters.Add(new SqlParameter("datumTreninga", trening.Datum));
                command.Parameters.Add(new SqlParameter("vremePocetka", trening.VremeTreninga));
                command.Parameters.Add(new SqlParameter("trajanjeTreninga", trening.TrajanjeTreninga));
                command.Parameters.Add(new SqlParameter("statusTreninga", trening.StatusTreninga.ToString()));
                command.Parameters.Add(new SqlParameter("jmbgInstruktora", trening.InstruktorJmbg));
                command.Parameters.Add(new SqlParameter("jmbgPolaznika", trening.PolaznikJmbg = " "));
                command.Parameters.Add(new SqlParameter("aktivan", trening.Aktivan));



                return (int)command.ExecuteScalar();
            }
        }
    }

}
