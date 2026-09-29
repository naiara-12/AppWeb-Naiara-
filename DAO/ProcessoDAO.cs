using AppWebNaiara.Configs;
using AppWebNaiara.Model;
using MySql.Data.MySqlClient;

namespace AppWebNaiara.DAO
{
    public class ProcessoDAO
    {
        private readonly Conexao _conexao;

        public ProcessoDAO(Conexao conexao)
        {
            _conexao = conexao;
        }

        // LISTAR PROCESSOS
        public List<Processo> Listar()
        {
            var lista = new List<Processo>();

            using var comando = _conexao.CreateCommand(
                "SELECT * FROM processos;");

            using var leitor =
                comando.ExecuteReader();

            while (leitor.Read())
            {
                lista.Add(MapearProcesso(leitor));
            }

            return lista;
        }

        // INSERIR PROCESSO
        public void Inserir(Processo processo)
        {
            using var con = _conexao.GetConnection();

            string sql = @"
                INSERT INTO processos
                (
                    numero_pro,
                    data_pro,
                    interessado_pro,
                    assunto_pro,
                    descricao_pro,
                    situacao_pro
                )
                VALUES
                (
                    @numero,
                    @data,
                    @interessado,
                    @assunto,
                    @descricao,
                    @situacao
                )";

            using var comando = con.CreateCommand();

            comando.CommandText = sql;

            comando.Parameters.AddWithValue(
                "@numero",
                processo.Numero);

            comando.Parameters.AddWithValue(
                "@data",
                processo.Data.HasValue
                    ? processo.Data.Value.ToDateTime(TimeOnly.MinValue)
                    : DBNull.Value);

            comando.Parameters.AddWithValue(
                "@interessado",
                processo.Interessado);

            comando.Parameters.AddWithValue(
                "@assunto",
                processo.Assunto);

            comando.Parameters.AddWithValue(
                "@descricao",
                processo.Descricao);

            comando.Parameters.AddWithValue(
                "@situacao",
                processo.Situacao);

            comando.ExecuteNonQuery();
        }

        // CONVERTE UMA LINHA DO BANCO EM PROCESSO
        private static Processo MapearProcesso(
            MySqlDataReader leitor)
        {
            DateOnly? data = null;

            if (!leitor.IsDBNull(
                leitor.GetOrdinal("data_pro")))
            {
                data = DateOnly.FromDateTime(
                    leitor.GetDateTime("data_pro"));
            }

            return new Processo
            {
                Id = leitor.GetInt32("id_pro"),

                Numero = DAOHelper.GetString(
                    leitor,
                    "numero_pro"),

                Data = data,

                Interessado = DAOHelper.GetString(
                    leitor,
                    "interessado_pro"),

                Assunto = DAOHelper.GetString(
                    leitor,
                    "assunto_pro"),

                Descricao = DAOHelper.GetString(
                    leitor,
                    "descricao_pro"),

                Situacao = DAOHelper.GetString(
                    leitor,
                    "situacao_pro")
            };
        }
    }
}