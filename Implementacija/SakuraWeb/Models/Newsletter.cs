using System.ComponentModel.DataAnnotations;

namespace SakuraWeb.Models
{
    public class Newsletter
    {
        [Key]
        int id { get; set; }
        //List<Poruka> poruke { get; set; } // Rijesen

        public Newsletter() 
        {
            
        }
        
        public void dodajPoruku(Poruka poruka)
        {
            //poruke.Add(poruka);
        }

        public void ukloniPoruku(Poruka poruka)
        {
            //poruke.Remove(poruka);
        }

        public void obrisiSvePoruke()
        {
            //poruke.Clear();
        }

        public void posaljiNewsletter()
        {

        }

    }
}
