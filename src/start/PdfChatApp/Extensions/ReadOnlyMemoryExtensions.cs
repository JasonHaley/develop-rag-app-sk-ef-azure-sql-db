namespace PdfChatApp.Extensions;

public static class ReadOnlyMemoryExtensions
{
    public static float[] ToFloatArray(this IList<ReadOnlyMemory<float>> list)
    {
        // Step 1: Calculate total length
        int totalLength = 0;
        foreach (var memory in list)
        {
            totalLength += memory.Length;
        }

        // Step 2: Allocate new float[] with total length
        float[] result = new float[totalLength];

        // Step 3: Copy data from each ReadOnlyMemory<float> to the result array
        int offset = 0;
        foreach (var memory in list)
        {
            memory.Span.CopyTo(result.AsSpan(offset));
            offset += memory.Length;
        }

        return result;
    }
}