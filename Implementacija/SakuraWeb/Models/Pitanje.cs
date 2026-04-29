public class Pitanje
{
	[Key]
	public int ID { get; set; }

	[Required]
	public string Naslov { get; set; }

	[Required]
	public string Sadrzaj { get; set; }

	public DateTime DatumObjave { get; set; }

	
	public int KorisnikID { get; set; }
	public Korisnik Korisnik { get; set; }
}
