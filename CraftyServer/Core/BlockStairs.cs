using java.util;

namespace CraftyServer.Core
{
    public class BlockStairs : Block
    {
        private readonly Block modelBlock;

        public BlockStairs(int i, Block block)
            : base(i, block.blockIndexInTexture, block.blockMaterial)
        {
            modelBlock = block;
            setHardness(block.blockHardness);
            setResistance(block.blockResistance/3F);
            setStepSound(block.stepSound);
        }

        public override void setBlockBoundsBasedOnState(IBlockAccess iblockaccess, int i, int j, int k)
        {
            setBlockBounds(0.0F, 0.0F, 0.0F, 1.0F, 1.0F, 1.0F);
        }

        public override AxisAlignedBB getCollisionBoundingBoxFromPool(World world, int i, int j, int k)
        {
            return base.getCollisionBoundingBoxFromPool(world, i, j, k);
        }

        public override bool isOpaqueCube()
        {
            return false;
        }

        public override bool shouldSideBeRendered(IBlockAccess iblockaccess, int i, int j, int k, int l)
        {
            return base.shouldSideBeRendered(iblockaccess, i, j, k, l);
        }

        public override void getCollidingBoundingBoxes(World world, int i, int j, int k, AxisAlignedBB axisalignedbb,
                                                       ArrayList arraylist)
        {
            int l = world.getBlockMetadata(i, j, k);
            if (l == 0)
            {
                setBlockBounds(0.0F, 0.0F, 0.0F, 0.5F, 0.5F, 1.0F);
                base.getCollidingBoundingBoxes(world, i, j, k, axisalignedbb, arraylist);
                setBlockBounds(0.5F, 0.0F, 0.0F, 1.0F, 1.0F, 1.0F);
                base.getCollidingBoundingBoxes(world, i, j, k, axisalignedbb, arraylist);
            }
            else if (l == 1)
            {
                setBlockBounds(0.0F, 0.0F, 0.0F, 0.5F, 1.0F, 1.0F);
                base.getCollidingBoundingBoxes(world, i, j, k, axisalignedbb, arraylist);
                setBlockBounds(0.5F, 0.0F, 0.0F, 1.0F, 0.5F, 1.0F);
                base.getCollidingBoundingBoxes(world, i, j, k, axisalignedbb, arraylist);
            }
            else if (l == 2)
            {
                setBlockBounds(0.0F, 0.0F, 0.0F, 1.0F, 0.5F, 0.5F);
                base.getCollidingBoundingBoxes(world, i, j, k, axisalignedbb, arraylist);
                setBlockBounds(0.0F, 0.0F, 0.5F, 1.0F, 1.0F, 1.0F);
                base.getCollidingBoundingBoxes(world, i, j, k, axisalignedbb, arraylist);
            }
            else if (l == 3)
            {
                setBlockBounds(0.0F, 0.0F, 0.0F, 1.0F, 1.0F, 0.5F);
                base.getCollidingBoundingBoxes(world, i, j, k, axisalignedbb, arraylist);
                setBlockBounds(0.0F, 0.0F, 0.5F, 1.0F, 0.5F, 1.0F);
                base.getCollidingBoundingBoxes(world, i, j, k, axisalignedbb, arraylist);
            }
            setBlockBounds(0.0F, 0.0F, 0.0F, 1.0F, 1.0F, 1.0F);
        }

        public override void onBlockClicked(World world, int i, int j, int k, EntityPlayer entityplayer)
        {
            modelBlock.onBlockClicked(world, i, j, k, entityplayer);
        }

        public override void onBlockDestroyedByPlayer(World world, int i, int j, int k, int l)
        {
            modelBlock.onBlockDestroyedByPlayer(world, i, j, k, l);
        }

        public override float getExplosionResistance(Entity entity)
        {
            return modelBlock.getExplosionResistance(entity);
        }

        public override int idDropped(int i, Random random)
        {
            return modelBlock.idDropped(i, random);
        }

        public override int quantityDropped(Random random)
        {
            return modelBlock.quantityDropped(random);
        }

        public override int func_22009_a(int i, int j)
        {
            return modelBlock.func_22009_a(i, j);
        }

        public override int getBlockTextureFromSide(int i)
        {
            return modelBlock.getBlockTextureFromSide(i);
        }

        public override int tickRate()
        {
            return modelBlock.tickRate();
        }

        public override void velocityToAddToEntity(World world, int i, int j, int k, Entity entity, Vec3D vec3d)
        {
            modelBlock.velocityToAddToEntity(world, i, j, k, entity, vec3d);
        }

        public override bool isCollidable()
        {
            return modelBlock.isCollidable();
        }

        public override bool canCollideCheck(int i, bool flag)
        {
            return modelBlock.canCollideCheck(i, flag);
        }

        public override bool canPlaceBlockAt(World world, int i, int j, int k)
        {
            return modelBlock.canPlaceBlockAt(world, i, j, k);
        }

        public override void onBlockAdded(World world, int i, int j, int k)
        {
            onNeighborBlockChange(world, i, j, k, 0);
            modelBlock.onBlockAdded(world, i, j, k);
        }

        public override void onBlockRemoval(World world, int i, int j, int k)
        {
            modelBlock.onBlockRemoval(world, i, j, k);
        }

        public override void dropBlockAsItemWithChance(World world, int i, int j, int k, int l, float f)
        {
            modelBlock.dropBlockAsItemWithChance(world, i, j, k, l, f);
        }

        public override void dropBlockAsItem(World world, int i, int j, int k, int l)
        {
            modelBlock.dropBlockAsItem(world, i, j, k, l);
        }

        public override void onEntityWalking(World world, int i, int j, int k, Entity entity)
        {
            modelBlock.onEntityWalking(world, i, j, k, entity);
        }

        public override void updateTick(World world, int i, int j, int k, Random random)
        {
            modelBlock.updateTick(world, i, j, k, random);
        }

        public override bool blockActivated(World world, int i, int j, int k, EntityPlayer entityplayer)
        {
            return modelBlock.blockActivated(world, i, j, k, entityplayer);
        }

        public override void onBlockDestroyedByExplosion(World world, int i, int j, int k)
        {
            modelBlock.onBlockDestroyedByExplosion(world, i, j, k);
        }

        public override void onBlockPlacedBy(World world, int i, int j, int k, EntityLiving entityliving)
        {
            int l = MathHelper.floor_double(((entityliving.rotationYaw*4F)/360F) + 0.5D) & 3;
            if (l == 0)
            {
                world.setBlockMetadataWithNotify(i, j, k, 2);
            }
            if (l == 1)
            {
                world.setBlockMetadataWithNotify(i, j, k, 1);
            }
            if (l == 2)
            {
                world.setBlockMetadataWithNotify(i, j, k, 3);
            }
            if (l == 3)
            {
                world.setBlockMetadataWithNotify(i, j, k, 0);
            }
        }
    }
}