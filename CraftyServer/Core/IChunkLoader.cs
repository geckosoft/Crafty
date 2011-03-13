namespace CraftyServer.Core
{
    public interface IChunkLoader
    {
        Chunk loadChunk(World world, int i, int j);
        void saveChunk(World world, Chunk chunk);
        void saveExtraChunkData(World world, Chunk chunk);
        void func_661_a();
        void saveExtraData();
    }
}