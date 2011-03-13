namespace CraftyServer.Core
{
    public interface IChunkProvider
    {
        bool chunkExists(int i, int j);
        Chunk provideChunk(int i, int j);
        void populate(IChunkProvider ichunkprovider, int i, int j);
        bool saveChunks(bool flag, IProgressUpdate iprogressupdate);
        bool func_361_a();
        bool func_364_b();
    }
}