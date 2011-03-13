namespace CraftyServer.Core
{
    public interface IBlockAccess
    {
        int getBlockId(int i, int j, int k);
        int getBlockMetadata(int i, int j, int k);
        Material getBlockMaterial(int i, int j, int k);
        bool isBlockOpaqueCube(int i, int j, int k);
    }
}