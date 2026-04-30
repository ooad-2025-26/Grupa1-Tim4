public class Usluga
{
    [Key]
    public int ID { get; set; }

    public string Naziv { get; set; }

    public string Opis { get; set; }

    public double Cijena { get; set; }

    public KategorijaUsluga Kategorija { get; set; }


    public Usluga() { } 

    public Usluga(string naziv, double cijena, KategorijaUsluga kategorija, int trajanje)
    {
        this.Naziv = naziv;
        this.Cijena = cijena;
        this.Kategorija = kategorija;
        this.Trajanje = trajanje;
    }


}