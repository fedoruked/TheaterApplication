using Npgsql;
using System.Collections.Generic;
using TheaterApplication.Dal.Builders.Interfaces;

namespace TheaterApplication.Dal.Builders
{
    public class NpgsqlParametersBuilder: IParametersBuilder
    {
        private readonly List<NpgsqlParameter> parameters;

        public NpgsqlParametersBuilder()
        {
            parameters = new List<NpgsqlParameter>();
        }

        public IParametersBuilder Add(string name, object value)
        {
            parameters.Add(new NpgsqlParameter(name, value));
            return this;
        }

        public object[] Build()
        {
            return parameters.ToArray();
        }
    }
}
