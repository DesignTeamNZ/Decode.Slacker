using Slacker.AutoModeller.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slacker.AutoModeller.Templates {
    public partial class DataModelTemplate {

        public List<InformationSchemaColumn> InfoSchema { get; protected set; }

        public DataModelTemplate(List<InformationSchemaColumn> infoSchema) {
            this.InfoSchema = infoSchema;
        }

    }
}
