using Microsoft.ML;

public class MLModelBuilder
{
    private readonly string _dataPath = Path.Combine("DATA", "casas.csv");
    private readonly MLContext _mlContext;
    private ITransformer _model;

    public MLModelBuilder()
    {
        _mlContext = new MLContext(seed: 0);
        TreinarModelo();
    }

    public void TreinarModelo()
    {
        var data = _mlContext.Data.LoadFromTextFile<ModelInput>(_dataPath, hasHeader: true, separatorChar: ',');

        var split = _mlContext.Data.TrainTestSplit(data, testFraction: 0.3);

        var pipeline = _mlContext.Transforms.Concatenate("Features", nameof(ModelInput.Tamanho), nameof(ModelInput.Quartos), nameof(ModelInput.Idade))
            .Append(_mlContext.Regression.Trainers.FastTree());

        _model = pipeline.Fit(split.TrainSet);

        var predictions = _model.Transform(split.TestSet);
        var metrics = _mlContext.Regression.Evaluate(predictions);
        Console.WriteLine($"R²: {metrics.RSquared:0.##} | RMSE: {metrics.RootMeanSquaredError:#.##}");
    }

    public float Prever(ModelInput input)
    {
        var predictionEngine = _mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(_model);
        return predictionEngine.Predict(input).Score;
    }
}
