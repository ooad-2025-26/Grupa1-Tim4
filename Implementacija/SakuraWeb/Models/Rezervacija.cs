public class Rezervacija
{
    public int ID { get; set; }
    public DateTime DatumRezervacije { get; set; }
    public DateTime VrijemeTermina { get; set; }
    public bool Potvrđena { get; set; }

    public int KorisnikID { get; set; }
    public Korisnik Korisnik { get; set; }

    public int UslugaID { get; set; }
    public Usluga Usluga { get; set; }
}