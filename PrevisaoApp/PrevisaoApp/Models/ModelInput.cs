using Microsoft.ML.Data;

public class ModelInput
{
    [LoadColumn(0)] public float Tamanho { get; set; }
    [LoadColumn(1)] public float Quartos { get; set; }
    [LoadColumn(2)] public float Idade { get; set; }

    [LoadColumn(3), ColumnName("Label")]
    public float Preco { get; set; }
}
