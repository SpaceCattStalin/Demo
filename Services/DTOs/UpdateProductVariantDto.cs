using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTOs;
public class UpdateProductVariantDto
{
    public int Id { get; set; }
    public string VariantCode { get; set; }
    public string Color { get; set; }
    public string Size { get; set; }
}
