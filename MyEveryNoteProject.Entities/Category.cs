using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEveryNoteProject.Entities
{
    [Table("Categories")]
    public class Category:MyEntityBase
    {       
        [DisplayName("Category Title"),Required,StringLength(50),MaxLength(60),MinLength(5)]
        public string Title { get; set; }

        [DisplayName("Description"),Required,StringLength(200),MaxLength(60), MinLength(5)]
        public string Description { get; set; }
        public virtual List<Article> Articles { get; set; }

        //Category e MyInitializer de category e fakedata eklerken article kısımı ile ilgili (AAA ile gosterilmistir MyInitializer o kısım) Article null gelmesin diye asagidaki constructor yazılmıstır.
        public Category()
        {
            Articles = new List<Article>();
        }

    }
}
