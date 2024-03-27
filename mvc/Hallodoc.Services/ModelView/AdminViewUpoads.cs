using HalloDoc.Repositories.DataModels;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HallodocServices.ModelView
{
    public class AdminViewUpoads
    {
        public string? filename { get; set; }

        public int requestId { get; set; }

        public int? Id { get; set; }

        [Column(TypeName = "timestamp without time zone")]
        public DateTime? CreatedDate { get; set; }
        public string? Patientname { get; set; }

        public int? count { get; set; }

        public List<RequestWiseFile> WiseFiles { get; set; }

        public IFormFile formFile { get; set; }

        List<string>? files { get; set; }
    }
}
