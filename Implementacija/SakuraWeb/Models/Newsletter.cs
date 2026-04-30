using System.ComponentModel.DataAnnotations;

namespace SakuraWeb.Models
{
    public class Newsletter
    {
        [Key]
        public int id { get; set; }

        public Newsletter()
        {
            id = -1;
        }

        public Newsletter(int id)
        {
            this.id = id;
        }

        public void dodajPoruku(Poruka poruka)
        {

        }

        public void ukloniPoruku(Poruka poruka)
        {

        }

        public void obrisiSvePoruke()
        {

        }

        public void posaljiNewsletter()
        {

        }

    }
}
