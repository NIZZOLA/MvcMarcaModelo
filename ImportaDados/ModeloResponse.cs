namespace ImportaDados;

public class ModeloResponse
{
    public List<Modelo> Modelos { get; set; }
    public List<Ano> Anos { get; set; }
}
public class Ano
{
    public string Codigo { get; set; }
    public string Nome { get; set; }
}

public class Modelo
{
    public int Codigo { get; set; }
    public string Nome { get; set; }
}

