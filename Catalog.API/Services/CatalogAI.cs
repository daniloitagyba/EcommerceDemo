using Catalog.API.Model;
using Microsoft.SemanticKernel.Embeddings;
using Pgvector;
using System.Diagnostics;

namespace Catalog.API.Services;

public sealed class CatalogAI : ICatalogAI
{
    private const int EmbeddingDimensions = 384;
#pragma warning disable SKEXP0001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
    private readonly ITextEmbeddingGenerationService _embeddingGenerator;
#pragma warning restore SKEXP0001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.

    /// <summary>The web host environment.</summary>
    private readonly IWebHostEnvironment _environment;
    /// <summary>Logger for use in AI operations.</summary>
    private readonly ILogger _logger;

#pragma warning disable SKEXP0001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
    public CatalogAI(IWebHostEnvironment environment, ILogger<CatalogAI> logger, ITextEmbeddingGenerationService embeddingGenerator = null)
#pragma warning restore SKEXP0001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
    {
        _embeddingGenerator = embeddingGenerator;
        _environment = environment;
        _logger = logger;
    }

    /// <inheritdoc/>
    public bool IsEnabled => _embeddingGenerator is not null;

    /// <inheritdoc/>
    public ValueTask<Vector> GetEmbeddingAsync(CatalogItem item) =>
        IsEnabled ?
            GetEmbeddingAsync(CatalogItemToString(item)) :
            ValueTask.FromResult<Vector>(null);

    /// <inheritdoc/>
    public async ValueTask<IReadOnlyList<Vector>> GetEmbeddingsAsync(IEnumerable<CatalogItem> items)
    {
        if (IsEnabled)
        {
            long timestamp = Stopwatch.GetTimestamp();

            IList<ReadOnlyMemory<float>> embeddings = await _embeddingGenerator.GenerateEmbeddingsAsync(items.Select(CatalogItemToString).ToList());
            var results = embeddings.Select(m => new Vector(m[0..EmbeddingDimensions])).ToList();

            if (_logger.IsEnabled(LogLevel.Trace))
            {
                _logger.LogTrace("Generated {EmbeddingsCount} embeddings in {ElapsedMilliseconds}s", results.Count, Stopwatch.GetElapsedTime(timestamp).TotalSeconds);
            }

            return results;
        }

        return null;
    }

    /// <inheritdoc/>
    public async ValueTask<Vector> GetEmbeddingAsync(string text)
    {
        if (IsEnabled)
        {
            long timestamp = Stopwatch.GetTimestamp();

#pragma warning disable SKEXP0001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
            ReadOnlyMemory<float> embedding = await _embeddingGenerator.GenerateEmbeddingAsync(text);
#pragma warning restore SKEXP0001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
            embedding = embedding[0..EmbeddingDimensions];

            if (_logger.IsEnabled(LogLevel.Trace))
            {
                _logger.LogTrace("Generated embedding in {ElapsedMilliseconds}s: '{Text}'", Stopwatch.GetElapsedTime(timestamp).TotalSeconds, text);
            }

            return new Vector(embedding);
        }

        return null;
    }

    private static string CatalogItemToString(CatalogItem item) => $"{item.Name} {item.Description}";
}
