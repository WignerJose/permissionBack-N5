using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PermissionsN5.Infrastructure.Config
{
    public class ElasticSearchConfig
    {
        public string Url { get; set; } = String.Empty;
        public string IndexName { get; set; } = String.Empty;
        public string UserName { get; set; } = String.Empty;
        public string Password { get; set; } = String.Empty;
    }
}
