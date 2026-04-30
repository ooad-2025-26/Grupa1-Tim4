public class Pitanje
{
	[Key]
	public int ID { get; set; }

	public string Sadrzaj { get; set; }

    public List<string> Odgovori { get; set; }

	public List<int> Poeni { get; set; }


    public Pitanje()
    {
        Odgovori = new List<string>();
        Poeni = new List<int>();
    }

    public Pitanje(string sadrzaj, List<string> odgovori, List<int> poeni)
    {
        this.Sadrzaj = sadrzaj;
        this.Odgovori = odgovori;
        this.Poeni = poeni;
    }

}
