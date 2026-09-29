using MySql.Data.MySqlClient;

namespace AppWebNaiara.Configs
{
    public static class DAOHelper
    {
        public static string GetString(
            MySqlDataReader reader,
            string columnName)
        {
            string text = string.Empty;

            if (!reader.IsDBNull(reader.GetOrdinal(columnName)))
            {
                text = reader.GetString(columnName);
            }

            return text;
        }

        public static DateTime? GetDateTime(
            MySqlDataReader reader,
            string columnName)
        {
            DateTime? value = null;

            if (!reader.IsDBNull(reader.GetOrdinal(columnName)))
            {
                value = reader.GetDateTime(columnName);
            }

            return value;
        }

        public static bool IsNull(
            MySqlDataReader reader,
            string columnName)
        {
            return reader.IsDBNull(
                reader.GetOrdinal(columnName));
        }
    }
}